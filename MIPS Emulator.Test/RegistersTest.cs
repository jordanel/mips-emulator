using System;
using NUnit.Framework;

namespace MIPS_Emulator.Test {
	public class RegistersTest {

		private Registers r;

		[SetUp]
		public void SetUp() {
			r = new Registers();
		}
		
		[Test]
		public void SetZeroRegister_RemainsZero() {
			r[0] = 5;
			Assert.AreEqual(0, r[0]);
		}

		[Test]
		public void TestSettingNonZeroRegister() {
			r[1] = 3;
			Assert.AreEqual(3, r[1]);
		}

		[Test]
		public void TestPrint() {
			for (int i = 0; i < 32; i++) {
				Console.WriteLine(Registers.RegisterToName(i));
			}
		}

	}
}