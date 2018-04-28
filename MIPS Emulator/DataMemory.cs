using System;

namespace MIPS_Emulator {
	public class DataMemory : MemoryUnit {
		public uint Size => (uint) memory.Length * 4;
		private readonly uint[] memory;
		
		public DataMemory(uint size) {
			memory = new uint[size];
		}
		
		public DataMemory(uint[] memory) {
			this.memory = memory;
		}
		
		public uint this[uint index] {
			get {
				if (index % 4 == 0) {
					return memory[index / 4];
				} else {
					throw new ArgumentException($"Index ({index}) into data memory is not a multiple of 4");
				}
			}
			
			set {
				if (index % 4 == 0) {
					memory[index / 4] = value;
				} else {
					throw new ArgumentException($"Index ({index}) into data memory is not a multiple of 4");
				}
			}
		}
	}
}