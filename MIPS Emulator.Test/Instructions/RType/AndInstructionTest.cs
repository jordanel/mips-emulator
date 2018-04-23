using MIPS_Emulator.Instructions.RType;
using NUnit.Framework;

namespace MIPS_Emulator.Test.Instructions.RType {
	public class AndInstructionTest {
		private uint pc;
		private Registers reg;
		private MemoryUnit mem;
		private AndInstruction target;

		[SetUp]
		public void SetUp() {
			pc = 0x0;
			reg = new Registers();
			mem = new MemoryUnit(8);
		}

		[Test]
		public void Execute() {
			reg[9] = 0x0000FFFF;
			reg[10] = 0x00FFFF00;
			target = new AndInstruction(8, 9, 10);
			
			target.Execute(ref pc, mem, reg);
			
			Assert.AreEqual(0x0000FF00, reg[8]);
			Assert.AreEqual(0x4, pc);
		}

		[Test]
		public void ToString_Formatted() {
			target = new AndInstruction(8, 9, 10);
			
			Assert.AreEqual("AND $t0, $t1, $t2", target.ToString());
		}
	}
}