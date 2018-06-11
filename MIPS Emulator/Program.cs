using System;
using System.IO;
using System.Threading;

namespace MIPS_Emulator {
	internal class Program {
		public static void Main(string[] args) {

			ProgramLoader loader;
			string token;

			//determine config file location
			if (args.Length > 0) {
				token = args[0];
			} else {
				Console.WriteLine("Config file path required. Please enter its location.");
				token = Console.ReadLine();
			}

			//loads the emulator configuration from the config file
			try
			{
				loader = new ProgramLoader(new FileInfo(token));
			}
			catch (Exception e)
			{
				Console.WriteLine("Encountered error on initialization. Press any key to exit.");
				Console.ReadKey();
				return;
			}

			Console.WriteLine("Emulator successfully configured. Ready to run.");

			while(true)
			{
				token = Console.ReadLine();
				if (token == "next" || token == "n")
				{
					loader.Mips.ExecuteNext();
					Console.WriteLine($"PC: {loader.Mips.Pc}\t Instr: {loader.Mips.InstrMem.GetInstruction(loader.Mips.Pc).ToString()}");
				}
				else if (token == "registers" || token == "r")
				{
					Console.WriteLine("Register Contents:");
					for (uint i = 0; i < 32; i++)
					{
						Console.WriteLine($"{Registers.RegisterToName(i)}\t{loader.Mips.Reg[i]}");
					}
				}
			}
		}
	}
}