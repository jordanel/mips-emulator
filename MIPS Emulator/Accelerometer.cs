using System;

namespace MIPS_Emulator {
	public class Accelerometer : MemoryUnit {
		public uint Size => WordSize;
		public uint WordSize { get; }
		public AccelerometerX X { get; set; }
		public AccelerometerY Y { get; set; }
		
		public uint this[uint index] {
			get { return (X.XValue << 16) | (Y.YValue); }
			set { }
		}
		
		public Accelerometer() : this(1, 4) { }

		public Accelerometer(uint size = 1, uint wordSize = 4) {
			this.X = new AccelerometerX();
			this.Y = new AccelerometerY();
			this.WordSize = wordSize;
		}
	}
}