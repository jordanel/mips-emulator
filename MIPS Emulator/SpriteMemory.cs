namespace MIPS_Emulator {
	public class SpriteMemory : MemoryUnit {
		private uint spriteX;
		private uint spriteY;
		
		public uint Size => 4;
		public uint WordSize => 4;

		public uint this[uint index] {
			get => (index & 0xF) == 0x8 ? spriteX : spriteY;
			set {
				if ((index & 0xF) == 0x8) {
					spriteX = value;
				} else {
					spriteY = value;
				}
			}
		}

		public SpriteMemory(uint size, uint wordSize = 4) {
			
		}
	}
}