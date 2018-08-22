namespace MIPS_Emulator {
	public class ScreenMemory : DataMemory {
		public ScreenMemory(uint size, uint wordSize = 4) : base(size, wordSize) {
		}

		public ScreenMemory(uint[] memory, uint wordSize) : base(memory, wordSize) {
		}
	}
}