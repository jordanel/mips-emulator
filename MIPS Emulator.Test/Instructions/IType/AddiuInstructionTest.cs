using MIPS_Emulator.Instructions.IType;
using NUnit.Framework;

namespace MIPS_Emulator.Test.Instructions.IType {
	public class AddiuInstructionTest {
		private uint pc;
		private Registers reg;
		private MemoryMapper mem;
		private AddiuInstruction target;

		[SetUp]
		public void SetUp() {
			pc = 0x00000000;
			reg = new Registers();
			mem = new MemoryMapper(8);
		}
		
		[Test]
		public void Execute_AddPositiveNumbers() {
			reg[1] = 0x00000002;
			target = new AddiuInstruction(1, 1, 0x0001);
			
			target.Execute(ref pc, mem, reg);
			
			Assert.AreEqual(0x00000003, reg[1]);
			Assert.AreEqual(0x00000004, pc);
		}

		[Test]
		public void Execute_NegativeImmediate() {
			reg[1] = 0x00000003;
			target = new AddiuInstruction(2, 1, 0xFFFF);
			
			target.Execute(ref pc, mem, reg);
			
			Assert.AreEqual(0x00000002, reg[2]);
			Assert.AreEqual(0x00000004, pc);
		}

		[Test]
		public void Execute_Overflow() {
			reg[1] = 0xFFFFFFFF;
			target = new AddiuInstruction(1, 1, 0x0001);
			
			target.Execute(ref pc, mem, reg);
			
			Assert.AreEqual(0x00000000, reg[1]);
			Assert.AreEqual(0x00000004, pc);
		}

		[Test]
		public void ToString_Formatted() {
			target = new AddiuInstruction(1, 2, 0xFFFF);
			
			Assert.AreEqual("ADDIU $at, $v0, 0xFFFF", target.ToString());
		}
	}
}