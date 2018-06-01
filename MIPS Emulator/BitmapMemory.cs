using System;

namespace MIPS_Emulator {
	public class BitmapMemory : MemoryUnit {
		public uint Size => (uint) memory.Length * 4;
		private readonly uint[] memory;
		
		public BitmapMemory(uint size) {
			memory = new uint[size];
		}
		
		public BitmapMemory(uint[] memory) {
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
			
			set { }
		}
	}
}