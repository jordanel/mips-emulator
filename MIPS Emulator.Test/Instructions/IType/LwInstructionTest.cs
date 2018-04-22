using MIPS_Emulator.Instructions.IType;
using NUnit.Framework;

namespace MIPS_Emulator.Test.Instructions.IType {
	public class LwInstructionTest {
		private uint pc;
		private Registers reg;
		private MemoryUnit mem;
		private LwInstruction target;

		[SetUp]
		public void SetUp() {
			pc = 0x00000000;
			reg = new Registers();
			mem = new MemoryUnit(8);
		}

		[Test]
		public void Execute_DataLoadedToReg() {
			mem[4] = 0xDEADBEEF;
			target = new LwInstruction(8, 9, 0x0004);
			
			target.Execute(ref pc, mem, reg);
			
			Assert.AreEqual(0xDEADBEEF, reg[8]);
			Assert.AreEqual(0x00000004, pc);
		}

		[Test]
		public void ToString_Formatted() {
			target = new LwInstruction(8, 9, 0x0004);
			
			Assert.AreEqual("LW $t0, 0x0004($t1)", target.ToString());
		}
	}
}