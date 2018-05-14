using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
			
			uint pc = ParseNumber(project["programCounter"]);
			InstructionMemory imem = BuildInstructionMemory(project["imem"]);
			
			return new Mips(pc, imem, null);
		}

		private uint ParseNumber(JToken token) {
			return ParseNumber((string) token);
		}

		private uint ParseNumber(string token) {
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