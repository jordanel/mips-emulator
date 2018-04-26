using System;
using System.Collections.Generic;
using System.Linq;

namespace MIPS_Emulator {
	public class MemoryMapper {
		private List<MappedMemoryUnit> memUnits;

		public MemoryMapper(List<MappedMemoryUnit> memUnits) {
			this.memUnits = memUnits;
			this.memUnits.Sort((x, y) => x.StartAddr.CompareTo(y.StartAddr));
		}
		
		[Obsolete]
		public MemoryMapper(uint size) {
			uint[] data = new uint[size];
			DataMemory dataMem = new DataMemory(data);
			MappedMemoryUnit mappedMem = new MappedMemoryUnit(dataMem, 0);
			memUnits = new List<MappedMemoryUnit> {mappedMem};
		}
		
		public uint this[uint i] {
			get => ResolveAddress(ref i)[i];
			set => ResolveAddress(ref i)[i] = value;
		}

		private MappedMemoryUnit ResolveAddress(ref uint addr) {
			var a = addr;
			var m = (from memUnit in memUnits
				where memUnit.StartAddr <= a && a <= memUnit.EndAddr
				select memUnit).ToList()[0];
			addr -= m.StartAddr;
			return m;
		}
	}
}