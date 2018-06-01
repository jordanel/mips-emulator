namespace MIPS_Emulator {
	public class ScreenMemory : DataMemory {
		public ScreenMemory(uint size) : base(size) {
		}

		public ScreenMemory(uint[] memory) : base(memory) {
		}
	}
}