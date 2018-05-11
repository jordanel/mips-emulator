using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Microsoft.CSharp;
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
			return null;
		}

		private uint[] ReadInitFile(JToken token) {
			string path = (string) token["filepath"];
			string format = (string) token["format"] ?? "hex";

			using (StreamReader r = new StreamReader(Path.Combine(basePath, path))) {
				
			}

			return null;
		}
	}
}