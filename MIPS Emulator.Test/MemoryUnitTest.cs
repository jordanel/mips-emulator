using NUnit.Framework;

namespace MIPS_Emulator.Test {
	public class MemoryUnitTest {

		[Test]
		public void TestSetter() {
			MemoryUnit mem = new MemoryUnit(8);
			mem[2] = 12;
			
			Assert.AreEqual(12, mem[2]);
		}
		
	}
}