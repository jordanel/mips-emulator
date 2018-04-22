using MIPS_Emulator.Instructions.JType;
using NUnit.Framework;

namespace MIPS_Emulator.Test.Instructions.JType {
	public class JalInstructionTest {
		private uint pc;
		private Registers reg;
		private MemoryUnit mem;
		private JalInstruction target;

		[SetUp]
		public void SetUp() {
			pc = 0x00000000;
			reg = new Registers();
			mem = new MemoryUnit(8);
		}

		[Test]
		public void Execute_PCChangedToAddressTimesFour() {
			target = new JalInstruction(0x00000003);
			
			target.Execute(ref pc, mem, reg);
			
			Assert.AreEqual(0x0000000C, pc);
			Assert.AreEqual(0x00000004, reg[31]);
		}

		[Test]
		public void Execute_UpperFourPcBitsMaintained() {
			pc = 0x20000000;
			target = new JalInstruction(0x00000003);
			
			target.Execute(ref pc, mem, reg);
			
			Assert.AreEqual(0x2000000C, pc);
			Assert.AreEqual(0x20000004, reg[31]);
		}

		[Test]
		public void ToString_Formatted() {
			target = new JalInstruction(0x00000003);
			
			Assert.AreEqual("JAL 0x00000003", target.ToString());
		}
	}
}