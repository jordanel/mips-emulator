using System;
using System.Collections.Generic;
using System.Windows.Input;
using IniParser;
using IniParser.Model;

namespace MIPS_Emulator.GUI {
	public static class ScanCodeMapper {
		private static IniData data;

		static ScanCodeMapper() {
			FileIniDataParser parser = new FileIniDataParser();
			data = parser.ReadFile("keyboard.ini");
		}

		public static uint GetScanCode(Key key) {
			string scanCode = data["Nexus"][((uint) key).ToString()] ?? "0";
			return UInt32.Parse(scanCode);
			// TODO: nonexistent mapping
		}
	}
}