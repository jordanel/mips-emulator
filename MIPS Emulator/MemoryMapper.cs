using System.Collections.Generic;

namespace MIPS_Emulator {
	public class MemoryMapper {
		private readonly uint[] dataMem;

		public MemoryMapper(List<MemoryUnit> memUnits) {
			
		}
		
		public MemoryMapper(uint size) {
			dataMem = new uint[size];
		}
		
		public uint this[uint i] {
			get => dataMem[i >> 2];
			set => dataMem[i >> 2] = value;
		}

		public override string ToString() {
			return dataMem.ToString();
		}
	}
}