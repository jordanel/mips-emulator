using System.Collections.Generic;
using NUnit.Framework;

namespace MIPS_Emulator.Test {
	public class MemoryMapperTest {

		[Test]
		public void AccessMemoryUnit_AddressResolved() {
			uint[] data = {1, 2, 3, 4};
			DataMemory dataMem = new DataMemory(data);
			List<MemoryUnit> memUnitList = new List<MemoryUnit> {dataMem};
			MemoryMapper mem = new MemoryMapper(memUnitList);
		}
		
	}
}