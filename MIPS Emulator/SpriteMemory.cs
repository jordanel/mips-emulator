namespace MIPS_Emulator {
	public class SpriteMemory : MemoryUnit {
		public uint SpriteX { get; private set; }
		public uint SpriteY { get; private set; }

		public uint Size => 4;
		public uint WordSize => 4;

		public uint this[uint index] {
			get => (index & 0xF) == 0x8 ? SpriteX : SpriteY;
			set {
				if ((index & 0xF) == 0x8) {
					SpriteX = value;
				} else {
					SpriteY = value;
				}
			}
		}

		public SpriteMemory(uint size, uint wordSize = 4) {
			
		}
	}
}