using System.Collections.Generic;
using NUnit.Framework;

namespace MIPS_Emulator.Test {
	public class MemoryMapperTest {
		private MemoryMapper target;
		
		[SetUp]
		public void SetUp() {
			MappedMemoryUnit mappedMem = BuildMappedMemUnit(new uint[] {1, 2, 3, 4}, 0);
			MappedMemoryUnit mappedMem2 = BuildMappedMemUnit(new uint[] {5, 6, 7, 8}, 16);
			MappedMemoryUnit mappedMem3 = BuildMappedMemUnit(new uint[] {9, 10, 11, 12}, 100);
			var memUnitList = new List<MappedMemoryUnit> {mappedMem2, mappedMem3, mappedMem};
						
			target = new MemoryMapper(memUnitList);
		}
		
		[Test]
		public void AccessMemoryUnit_LowerBound_AddressResolved() {
			Assert.AreEqual(5, target[16]);
		}

		[Test]
		public void AccessMemoryUnit_UpperBound_AddressResolved() {
			Assert.AreEqual(8, target[16 + 12]);
		}

		[Test]
		public void MutateMemoryUnit_AddressResolved() {
			target[104] = 100000;
			
			Assert.AreEqual(100000, target[104]);
		}

		[Test]
		public void AccessUnmappedAddress_ThrowsUnmappedAddressException() {
			uint i;
			
			Assert.Throws<MemoryMapper.UnmappedAddressException>(
				() => i = target[48]
			);
		}

		private MappedMemoryUnit BuildMappedMemUnit(uint[] data, uint startAddr) {
			DataMemory dataMem = new DataMemory(data);
			MappedMemoryUnit mappedMem = new MappedMemoryUnit(dataMem, startAddr);
			return mappedMem;
		}
	}
}