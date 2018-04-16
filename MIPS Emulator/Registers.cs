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
			return regNumber.ToString();
		}
	}
}