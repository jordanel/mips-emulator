using System;

namespace MIPS_Emulator {
	public class AccelerometerY : MemoryUnit {
		public uint Size => WordSize;
		public uint WordSize { get; }
		public uint yValue { get; set; }
		
		public uint this[uint index] {
			get { return yValue; }
			set { }
		}
		
		public AccelerometerY() {
			this.WordSize = 4;
		}

		public AccelerometerY(uint size = 1, uint wordSize = 4) {
			this.WordSize = wordSize;
		}
	}
}