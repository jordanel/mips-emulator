using System.Collections.Generic;
using NUnit.Framework;

namespace MIPS_Emulator.Test {
	public class MemoryMapperTest {

		[Test]
		public void AccessMemoryUnit_AddressResolved() {
			uint[] data = {1, 2, 3, 4};
			DataMemory dataMem = new DataMemory(data);
			MappedMemoryUnit mappedMem = new MappedMemoryUnit(dataMem, 0);
			MappedMemoryUnit mappedMem2 = new MappedMemoryUnit(dataMem, 4*4);
			MappedMemoryUnit mappedMem3 = new MappedMemoryUnit(dataMem, 8*4);
			var memUnitList = new List<MappedMemoryUnit> {mappedMem2, mappedMem3, mappedMem};
			MemoryMapper mem = new MemoryMapper(memUnitList);
			
			Assert.AreEqual(3, mem[8 + 16]);
		}
	}
}