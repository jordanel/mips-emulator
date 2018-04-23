using MIPS_Emulator.Instructions.RType;
using NUnit.Framework;

namespace MIPS_Emulator.Test.Instructions.RType {
	public class SubInstructionTest {
		private uint pc;
		private Registers reg;
		private MemoryUnit mem;
		private SubInstruction target;

		[SetUp]
		public void SetUp() {
			pc = 0x0;
			reg = new Registers();
			mem = new MemoryUnit(8);
		}

		[Test]
		public void Execute_PositiveNumbers() {
			reg[17] = 0x3;
			reg[18] = 0x1;
			target = new SubInstruction(16, 17, 18);
			
			target.Execute(ref pc, mem, reg);
			
			Assert.AreEqual(0x2, reg[16]);
			Assert.AreEqual(0x4, pc);
		}
		
		[Test]
		public void Execute_NegativeNumber() {
			reg[17] = 0x3;
			reg[18] = 0xFFFFFFFF;
			target = new SubInstruction(16, 17, 18);
			
			target.Execute(ref pc, mem, reg);
			
			Assert.AreEqual(0x4, reg[16]);
			Assert.AreEqual(0x4, pc);
		}

		[Test]
		public void Execute_Underflow() {
			reg[17] = 0x0;
			reg[18] = 0x1;
			target = new SubInstruction(16, 17, 18);
			
			target.Execute(ref pc, mem, reg);
			
			Assert.AreEqual(0xFFFFFFFF, reg[16]);
			Assert.AreEqual(0x4, pc);
		}

		[Test]
		public void ToString_Formatted() {
			target = new SubInstruction(16, 17, 18);
			
			Assert.AreEqual("SUB $s0, $s1, $s2", target.ToString());
		}
	}
}