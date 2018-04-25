using MIPS_Emulator.Instructions.IType;
using NUnit.Framework;

namespace MIPS_Emulator.Test.Instructions.IType {
	public class OriInstructionTest {
		private uint pc;
		private Registers reg;
		private MemoryMapper mem;
		private OriInstruction target;

		[SetUp]
		public void SetUp() {
			pc = 0x00000000;
			reg = new Registers();
			mem = new MemoryMapper(8);
		}

		[Test]
		public void Execute() {
			reg[8] = 0xFFFF0000;
			target = new OriInstruction(9, 8, 0xFFFF);
			
			target.Execute(ref pc, mem, reg);
			
			Assert.AreEqual(0xFFFFFFFF, reg[9]);
			Assert.AreEqual(0x00000004, pc);
		}

		[Test]
		public void ToString_Formatted() {
			target = new OriInstruction(9, 8, 0xFFFF);
			
			Assert.AreEqual("ORI $t1, $t0, 0xFFFF", target.ToString());
		}
	}
}