using System;
using System.IO;
using System.Threading;

namespace MIPS_Emulator {
	internal class Program {
		public static void Main(string[] args) {
			ProgramLoader loader;
			string filename;

			//determine config file location
			if (args.Length > 0) {
				filename = args[0];
			}
			else {
				Console.WriteLine("Config file path required. Please enter its location.");
				filename = Console.ReadLine();
			}

			//loads the emulator configuration from the config file
			try {
				loader = new ProgramLoader(new FileInfo(filename));
			}
			catch (Exception) {
				Console.WriteLine("Encountered error on initialization. Press any key to exit.");
				Console.ReadKey();
				return;
			}

			Console.WriteLine("Emulator successfully configured. Ready to run.");

			while (true) {
				char token = (char) Console.ReadKey().KeyChar;
				switch (token) {
					case 'n':
						loader.Mips.ExecuteNext();
						Console.WriteLine(
							$"PC: {loader.Mips.Pc}\t Instr: {loader.Mips.InstrMem.GetInstruction(loader.Mips.Pc)}"
						);
						break;
					case 'r':
						Console.WriteLine("Register Contents:");
						for (uint i = 0; i < 32; i++) {
							Console.WriteLine($"{Registers.RegisterToName(i)}\t{loader.Mips.Reg[i]}");
						}

						break;
					case 'm':
						Console.WriteLine("Memory Contents:");
						break;
				}
			}
		}
	}
}