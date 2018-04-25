using MIPS_Emulator.Instructions.RType;
using NUnit.Framework;

namespace MIPS_Emulator.Test.Instructions.RType {
	public class AddInstructionTest {
		private uint pc;
		private Registers reg;
		private MemoryMapper mem;
		private AddInstruction target;

		[SetUp]
		public void SetUp() {
			pc = 0x0;
			reg = new Registers();
			mem = new MemoryMapper(8);
		}

		[Test]
		public void Execute_PositiveNumbers() {
			reg[9] = 0x1;
			reg[10] = 0x3;
			target = new AddInstruction(8, 9, 10);
			
			target.Execute(ref pc, mem, reg);
			
			Assert.AreEqual(0x4, reg[8]);
			Assert.AreEqual(0x4, pc);
		}
		
		[Test]
		public void Execute_NegativeNumber() {
			reg[9] = 0x3;
			reg[10] = 0xFFFFFFFF;
			target = new AddInstruction(8, 9, 10);
			
			target.Execute(ref pc, mem, reg);
			
			Assert.AreEqual(0x2, reg[8]);
			Assert.AreEqual(0x4, pc);
		}

		[Test]
		public void Execute_Overflow() {
			reg[9] = 0xFFFFFFFF;
			reg[10] = 0x1;
			target = new AddInstruction(8, 9, 10);
			
			target.Execute(ref pc, mem, reg);
			
			Assert.AreEqual(0x0, reg[8]);
			Assert.AreEqual(0x4, pc);
		}

		[Test]
		public void ToString_Formatted() {
			target = new AddInstruction(8, 9, 10);
			
			Assert.AreEqual("ADD $t0, $t1, $t2", target.ToString());
		}
	}
}