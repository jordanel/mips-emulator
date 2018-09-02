using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Remoting.Messaging;
using MIPS_Emulator.Instructions;
using Newtonsoft.Json.Linq;

namespace MIPS_Emulator {
	public class ProgramLoader {
		public Mips Mips { get; }
		private readonly string basePath;

		public ProgramLoader(FileInfo file) {
			this.basePath = file.DirectoryName;
			this.Mips = LoadMipsFromFile(file);
		}

		private Mips LoadMipsFromFile(FileInfo file) {
			string json;
			using (StreamReader r = file.OpenText()) {
				json = r.ReadToEnd();
			}
			
			JObject project = JObject.Parse(json);
			
			uint pc = ParseRequiredNumber(project["programCounter"]);
			var memDict = BuildMemoryUnits(project["memories"]);
						
			return new Mips(pc, memDict);
		}

		private uint ParseRequiredNumber(JToken token) {
			var number = ParseNumber(token);
			if (number == null) {
				throw new ArgumentException("Expected field not found in project JSON");
			}
			return (uint) number;
		}
		
		private uint? ParseNumber(JToken token) {
			return token != null ? ParseNumber((string) token) : null;
		}

		private uint? ParseNumber(string token) {
			return Convert.ToUInt32(token, 10);
		}
		
		private IDictionary<Type, List<MemoryUnit>> BuildMemoryUnits(JToken token) {
			JArray memories = (JArray) token;
			var memoryDict = new Dictionary<Type, List<MemoryUnit>>();
			
			List<MappedMemoryUnit> memUnits = new List<MappedMemoryUnit>();
			
			for (int i = 0; i < memories.Count; i++) {
				MemoryUnit mem = BuildMemoryUnit(memories[i]);
				try {
					MappedMemoryUnit mappedMem = MapMemoryToAddresses(memories[i], mem);
					memUnits.Add(mappedMem);
				}
				catch (MappingException) {
					//nop
				}

				if (!memoryDict.ContainsKey(mem.GetType())) {
						memoryDict.Add(mem.GetType(), new List<MemoryUnit>());
					}
					memoryDict[mem.GetType()].Add(mem);
				}
			
			MemoryMapper mapper = new MemoryMapper(memUnits);
			memoryDict.Add(mapper.GetType(), new List<MemoryUnit>());
			memoryDict[mapper.GetType()].Add(mapper);

			return memoryDict;
		}
		
		private MemoryUnit BuildMemoryUnit(JToken token) {
			string type = (string) token["type"];
			uint? length = ParseNumber(token["length"]);
			uint? wordSize = ParseNumber(token["wordSize"]);
			uint[] init = token["initFile"] != null ? ReadInitFile(token["initFile"]) : null;

			MemoryUnit mem = null;
			try {
				Type t = Type.GetType($"MIPS_Emulator.{type}");
				object[] args;
				if (length != null || init != null) {
					args = new object[] {length ?? (uint) init.Length, wordSize ?? 4};
				} else {
					args = new object[0];
				}

				mem = (MemoryUnit) Activator.CreateInstance(t, args);
			} catch (ArgumentNullException) {
				throw new ArgumentException($"MemoryUnit type {type} does not exist");
			} catch (InvalidCastException) {
				throw new ArgumentException($"MemoryUnit type {type} is not a MemoryUnit");
			} catch (MissingMethodException) {
				throw new ArgumentException($"MemoryUnit type {type} is not a MemoryUnit");
			}

			if (init != null) {
				for (uint i = 0; i < init.Length; i++) {
					mem[i * mem.WordSize] = init[i];
				}
			}
			return mem;
		}

		private MappedMemoryUnit MapMemoryToAddresses(JToken token, MemoryUnit mem) {
			uint? startAddr = ParseNumber(token["startAddr"]);
			uint? endAddr = ParseNumber(token["endAddr"]);
			uint? size = ParseNumber(token["size"]);
			string bitmask = (string) token["bitmask"];
			string name = (string) token["name"];

			MappedMemoryUnit mappedMem = null;
			if (startAddr != null) {
				if (endAddr != null) {
					mappedMem = new MappedMemoryUnit(mem, (uint) startAddr, (uint) endAddr, name);
				} else if (size != null) {
					mappedMem = new MappedMemoryUnit(mem, (uint) startAddr, (uint) (startAddr + size - 1), name);
				} else {
					mappedMem = new MappedMemoryUnit(mem, (uint) startAddr, name);
				}
			} else if (bitmask != null) {
				mappedMem = new MappedMemoryUnit(mem, bitmask, name);
			} else {
				throw new MappingException("MappedMemoryUnit requires either startAddr or bitmask");
			}
			return mappedMem;
		}

		private uint[] ReadInitFile(JToken token) {
			string path = (string) token["filepath"];
			string format = (string) token["format"] ?? "hex";
			int baseNum = ParseFormat(format);

			return ParseInitData(path, baseNum);
		}

		private static int ParseFormat(string format) {
			var baseDict = new Dictionary<string, int> {
				{"hex", 16},
				{"dec", 10},
				{"bin", 2}
			};
			if (!baseDict.TryGetValue(format, out int baseNum)) {
				baseNum = 16;
			}
			return baseNum;
		}

		private uint[] ParseInitData(string path, int baseNum) {
			List<uint> data = new List<uint>();
			using (StreamReader r = new StreamReader(Path.Combine(basePath, path))) {
				while (!r.EndOfStream) {
					string line = CleanLine(r.ReadLine());
					if (line.Length > 0) {
						data.Add(Convert.ToUInt32(line, baseNum));
					}
				}
			}
			return data.ToArray();
		}

		private static string CleanLine(string line) {
			int index = line.IndexOf("//", StringComparison.Ordinal);
			if (index >= 0) {
				line = line.Substring(0, index);
			}
			line = line.Trim().Replace("_", "");
			
			return line;
		}

		private class MappingException : ArgumentException {
			public MappingException(string message) 
				: base (message) {
			}
		}
	}
}