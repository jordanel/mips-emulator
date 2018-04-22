using System;
using MIPS_Emulator.Instructions.IType;
using NUnit.Framework;

namespace MIPS_Emulator.Test.Instructions.IType {
	public class AddiInstructionTest {
		private uint pc;
		private Registers reg;
		private MemoryUnit mem;
		private AddiInstruction target;

		[SetUp]
		public void SetUp() {
			pc = 0x00000000;
			reg = new Registers();
			mem = new MemoryUnit(8);
		}
		
		[Test]
		public void Execute_AddPositiveNumbers() {
			reg[1] = 0x00000002;
			target = new AddiInstruction(1, 1, 0x0001);
			
			target.Execute(ref pc, mem, reg);
			
			Assert.AreEqual(0x00000003, reg[1]);
			Assert.AreEqual(0x00000004, pc);
		}

		[Test]
		public void Execute_NegativeImmediate() {
			reg[1] = 0x00000003;
			target = new AddiInstruction(2, 1, 0xFFFF);
			
			target.Execute(ref pc, mem, reg);
			
			Assert.AreEqual(0x00000002, reg[2]);
			Assert.AreEqual(0x00000004, pc);
		}

		[Test]
		public void Execute_Overflow() {
			reg[1] = 0xFFFFFFFF;
			target = new AddiInstruction(1, 1, 0x0001);
			
			target.Execute(ref pc, mem, reg);
			
			Assert.AreEqual(0x00000000, reg[1]);
			Assert.AreEqual(0x00000004, pc);
		}

		[Test]
		public void ToString_Formatted() {
			target = new AddiInstruction(1, 2, 0xFFFF);
			
			Assert.AreEqual("ADDI $at, $v0, 0xFFFF", target.ToString());
		}
	}
}