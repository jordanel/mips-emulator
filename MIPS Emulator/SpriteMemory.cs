namespace MIPS_Emulator {
	public class SpriteMemory : MemoryUnit {
		public uint SpriteX { get; private set; }
		public uint SpriteY { get; private set; }

		public uint Size => 8;
		public uint WordSize => 4;

		public uint this[uint index] {
			get => index == 0 ? SpriteY : SpriteX;
			set {
				if (index == 0) {
					SpriteY = value;
				} else {
					SpriteX = value;
				}
			}
		}

		public SpriteMemory(uint size, uint wordSize = 4) {
			
		}
	}
}