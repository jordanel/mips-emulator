namespace MIPS_Emulator {
	public class Registers {

		private readonly uint[] registers;

		public Registers() {
			registers = new uint[32];
		}

		public uint getRegister(int regNumber) {
			return (regNumber == 0) ? 0 : registers[regNumber];
		}

		public void setRegister(int regNumber, uint value) {
			registers[regNumber] = value;
		}

		public static string registerToName(int regNumber) {

			if (regNumber == 0) {
				return "$zero";
			} else if (regNumber == 1) {
				return "$at";
			} else if (regNumber <= 3) {
				return "$v" + (regNumber - 2);
			} else if (regNumber <= 7) {
				return "$a" + (regNumber - 4);
			} else if (regNumber <= 15) {
				return "$t" + (regNumber - 8);
			} else if (regNumber <= 23) {
				return "$s" + (regNumber - 16);
			} else if (regNumber <= 25) {
				return "$t" + (regNumber - 16);
			} else if (regNumber <= 27) {
				return "$k" + (regNumber - 26);
			} else if (regNumber == 28) {
				return "$gp";
			} else if (regNumber == 29) {
				return "$sp";
			} else if (regNumber == 30) {
				return "$fp";
			} else if (regNumber == 31) {
				return "$ra";
			}
			
			return "$" + regNumber;
		}
	}
}