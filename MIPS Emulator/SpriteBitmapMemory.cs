namespace MIPS_Emulator {
	public class SpriteBitmapMemory : BitmapMemory{
		public SpriteBitmapMemory(uint size, uint wordSize = 4) : base(size, wordSize) {
		}

		public SpriteBitmapMemory(uint[] memory, uint wordSize = 4) : base(memory, wordSize) {
		}
	}
}