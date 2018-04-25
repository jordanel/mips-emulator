using NUnit.Framework;

namespace MIPS_Emulator.Test {
	public class MemoryMapperTest {

		[Test]
		public void TestSetter() {
			MemoryMapper mem = new MemoryMapper(8);
			mem[2] = 12;
			
			Assert.AreEqual(12, mem[2]);
		}
		
	}
}