using MIPS_Emulator.Instructions.JType;
using NUnit.Framework;

namespace MIPS_Emulator.Test.Instructions.JType {
	public class JInstructionTest {
		private uint pc;
		private Registers reg;
		private MemoryMapper mem;
		private JInstruction target;

		[SetUp]
		public void SetUp() {
			pc = 0x00000000;
			reg = new Registers();
			mem = new MemoryMapper(8);
		}

		[Test]
		public void Execute_PCChangedToAddressTimesFour() {
			target = new JInstruction(0x00000003);
			
			target.Execute(ref pc, mem, reg);
			
			Assert.AreEqual(0x0000000C, pc);
		}

		[Test]
		public void Execute_UpperFourPcBitsMaintained() {
			pc = 0x20000000;
			target = new JInstruction(0x00000003);
			
			target.Execute(ref pc, mem, reg);
			
			Assert.AreEqual(0x2000000C, pc);
		}

		[Test]
		public void ToString_Formatted() {
			target = new JInstruction(0x00000003);
			
			Assert.AreEqual("J 0x00000003", target.ToString());
		}
	}
}