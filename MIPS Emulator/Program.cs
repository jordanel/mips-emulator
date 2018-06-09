using System;
using System.IO;
using System.Threading;

namespace MIPS_Emulator {
	internal class Program {
		public static void Main(string[] args) {
			if (args.Length > 0) {
				ProgramLoader loader = new ProgramLoader(new FileInfo(args[0]));
				Console.WriteLine("Running");
				loader.Mips.ExecuteAll();
				Console.WriteLine("Finished executing");
			} else {
				Console.WriteLine("Config file path required");
			}
			Thread.Sleep(1000);
		}
	}
}