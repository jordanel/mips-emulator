using MIPS_Emulator.Instructions.IType;
using NUnit.Framework;

namespace MIPS_Emulator.Test.Instructions.IType {
	public class AddiuInstructionTest {
		private uint pc;
		private Registers reg;
		private MemoryUnit mem;
		private AddiuInstruction target;

		[SetUp]
		public void SetUp() {
			pc = 0;
			reg = new Registers();
			mem = new MemoryUnit(8);
		}
		
		[Test]
		public void Test() {
			reg[2] = 2;
			target = new AddiuInstruction(1, 1, 2);
			target.execute(ref pc, mem, reg);
			Assert.AreEqual(3, reg[1]);
		}
	}
}