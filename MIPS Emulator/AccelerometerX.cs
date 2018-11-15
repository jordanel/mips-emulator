using System;

namespace MIPS_Emulator {
	public class AccelerometerX : MemoryUnit {
		public uint Size => WordSize;
		public uint WordSize { get; }
		public uint XValue { get; set; } = 255;
		
		public uint this[uint index] {
			get { return XValue; }
			set { }
		}
		
		public AccelerometerX() {
			this.WordSize = 4;
		}

		public AccelerometerX(uint size = 1, uint wordSize = 4) {
			this.WordSize = wordSize;
		}
	}
}