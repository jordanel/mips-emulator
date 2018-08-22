using System;

namespace MIPS_Emulator {
	public class BitmapMemory : MemoryUnit {
		public uint Size => (uint) memory.Length * WordSize;
		public uint WordSize { get; }
		private readonly uint[] memory;
		
		public BitmapMemory(uint size, uint wordSize = 4) {
			memory = new uint[size];
			WordSize = wordSize;
		}
		
		public BitmapMemory(uint[] memory, uint wordSize = 4) {
			this.memory = memory;
			WordSize = wordSize;
		}
		
		public uint this[uint index] {
			get {
				if (index % WordSize == 0) {
					return memory[index / WordSize];
				} else {
					throw new ArgumentException($"Index ({index}) into bitmap memory is not a multiple of word size ({WordSize})");
				}
			}

			// TODO: consider making read only
			set {
				if (index % WordSize == 0) {
					memory[index / WordSize] = value;
				} else {
					throw new ArgumentException($"Index ({index}) into bitmap memory is not a multiple of word size ({WordSize})");
				}
			}
		}
	}
}