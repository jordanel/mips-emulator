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
		private string basePath;

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
			InstructionMemory imem = BuildInstructionMemory(project["imem"]);
			MemoryMapper mappedMem = BuildMemoryMapper(project["mappedMemory"]);
			
			return new Mips(pc, imem, mappedMem);
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

		private InstructionMemory BuildInstructionMemory(JToken token) {
			uint[] init = ReadInitFile(token["initFile"]);
			List<Instruction> instructions = new List<Instruction>();
			InstructionFactory instrFact = new InstructionFactory(); // TODO: Use instructionSet to determine impl
			
			foreach (uint instruction in init) {
				instructions.Add(instrFact.CreateInstruction(instruction));
			}
			
			return new InstructionMemory(instructions.ToArray());
		}

		private MemoryMapper BuildMemoryMapper(JToken token) {
			JArray memories = (JArray) token;
			List<MappedMemoryUnit> memUnits = new List<MappedMemoryUnit>();
			
			for (int i = 0; i < memories.Count; i++) {
				MappedMemoryUnit mem = BuildMemoryUnit(memories[i]);
				memUnits.Add(mem);
			}
			
			return new MemoryMapper(memUnits);
		}

		private MappedMemoryUnit BuildMemoryUnit(JToken token) {
			string type = (string) token["type"];
			uint? length = ParseNumber(token["length"]);
			uint[] init = token["initFile"] != null ? ReadInitFile(token["initFile"]) : null;

			Type t = Type.GetType($"MIPS_Emulator.{type}");
			Object[] args = { length ?? (uint) init.Length };

			MemoryUnit mem = null;
			try {
				mem = (MemoryUnit) Activator.CreateInstance(t, args);
			} catch (TypeLoadException e) {
				throw new ArgumentException($"MemoryUnit type {type} does not exist");
			}

			if (init != null) {
				for (uint i = 0; i < init.Length; i++) {
					mem[i * 4] = init[i];
				}
			}

			uint? startAddr = ParseNumber(token["startAddr"]);
			uint? endAddr = ParseNumber(token["endAddr"]);
			uint? size = ParseNumber(token["size"]);
			string bitmask = (string) token["bitmask"];

			MappedMemoryUnit mappedMem = null;
			if (startAddr != null) {
				if (endAddr != null) {
					mappedMem = new MappedMemoryUnit(mem, (uint) startAddr, (uint) endAddr);
				} else if (size != null) {
					mappedMem = new MappedMemoryUnit(mem, (uint) startAddr, (uint) (startAddr + size));
				} else {
					mappedMem = new MappedMemoryUnit(mem, (uint) startAddr);
				}
			} else if (bitmask != null) {
				mappedMem = new MappedMemoryUnit(mem, bitmask);
			} else {
				throw new ArgumentException("MappedMemoryUnit requires either startAddr or bitmask");
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
	}
}