using System;

namespace MIPS_Emulator {
	public class Accelerometer : MemoryUnit {
		public uint Size => 2 * WordSize;
		public uint WordSize { get; }
		public (uint x, uint y) Coordinates { get; set; }
		
		public uint this[uint index] {
			get {
				if (index == 0) {
					return Coordinates.x;
				} else if (index == WordSize) {
					return Coordinates.y;
				} else {
					throw new ArgumentException($"Index ({index}) into data memory is invalid");
				}
			}
			set { }
		}
		
		public Accelerometer() {
			this.WordSize = 4;
		}

		public Accelerometer(uint size = 2, uint wordSize = 4) {
			this.WordSize = wordSize;
		}
	}
}