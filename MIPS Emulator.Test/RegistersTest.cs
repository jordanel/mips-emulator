using System;
using NUnit.Framework;

namespace MIPS_Emulator.Test {
	public class RegistersTest {

		private Registers r;

		[SetUp]
		public void setUp() {
			r = new Registers();
		}
		
		[Test]
		public void test() {
			r.setRegister(0, 50);
			Assert.AreEqual(0, r.getRegister(0));
		}

		[Test]
		public void test2() {
			r.setRegister(1, 3);
			Assert.AreEqual(3, r.getRegister(1));
		}

		[Test]
		public void testPrint() {
			for (int i = 0; i < 32; i++) {
				Console.WriteLine(Registers.registerToName(i));
			}
		}

	}
}