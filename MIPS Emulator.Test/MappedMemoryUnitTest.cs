using System;
using NUnit.Framework;

namespace MIPS_Emulator.Test {
	public class MappedMemoryUnitTest {
		private MemoryUnit memUnit;

		[SetUp]
		public void SetUp() {
			memUnit = new DataMemory(new uint[] {1, 2, 3, 4});
		}
		
		[Test]
		public void Construct_WithStartAndEndAddresses() {
			MappedMemoryUnit target = new MappedMemoryUnit(memUnit, 12, 44);
			
			Assert.AreEqual(12, target.StartAddr);
			Assert.AreEqual(44, target.EndAddr);
			Assert.AreEqual(memUnit, target.MemUnit);
		}

		[Test]
		public void Construct_WithStartAddress_ImplicitEndAddress() {
			MappedMemoryUnit target = new MappedMemoryUnit(memUnit, 12);
			
			Assert.AreEqual(12, target.StartAddr);
			Assert.AreEqual(27, target.EndAddr);
			Assert.AreEqual(memUnit, target.MemUnit);
		}

		[Test]
		public void Construct_Bitmask() {
			MappedMemoryUnit target = new MappedMemoryUnit(memUnit, " _1_0_xXxx_\r\n");
			
			Assert.AreEqual(0b100000, target.StartAddr);
			Assert.AreEqual(0b101111, target.EndAddr);
			Assert.AreEqual(memUnit, target.MemUnit);
		}

		[Test]
		public void Construct_BitmaskBeginsWithX_ThrowsArgumentException() {
			TestThrows("X10XXXX");
		}

		[Test]
		public void Construct_BitmaskIsAllX_ThrowsArgumentException() {
			TestThrows("XXXX");
		}

		[Test]
		public void Construct_BitmaskXBetweenBits_ThrowsArgumentException() {
			TestThrows("1X0X");
		}

		[Test]
		public void Construct_BitmaskInvalidCharacters_ThrowsArgumentException() {
			TestThrows("1021");
		}

		[Test]
		public void Construct_BitmaskEmpty_ThrowsArgumentException() {
			TestThrows("");
		}

		private void TestThrows(string bitmask) {
			Assert.Throws<ArgumentException>(
				() => new MappedMemoryUnit(memUnit, bitmask)
			);
		}
	}
}