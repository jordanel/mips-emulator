using System;
using MIPS_Emulator.Instructions;
using MIPS_Emulator.Instructions.IType;
using MIPS_Emulator.Instructions.RType;
using NUnit.Framework;

namespace MIPS_Emulator.Test {
	public class DataMemoryTest {
		private DataMemory target;

		[SetUp]
		public void SetUp() {
			uint[] data = { 1, 2 };
			target = new DataMemory(data);
		}
		
		[Test]
		public void SetInstruction_ValidIndex() {
			target = new DataMemory(8) {[4] = 5};

			Assert.AreEqual(5, target[4]);
		}

		[Test]
		public void SetInstruction_InvalidIndex_ThrowsArgumentException() {
			Assert.Throws<ArgumentException>(
				() => target[3] = 10
			);
		}

		[Test]
		public void GetInstruction_InvalidIndex_ThrowsArgumentException() {
			uint dataElement;
			
			Assert.Throws<ArgumentException>(
				() => dataElement = target[3]
			);
		}
	}
}