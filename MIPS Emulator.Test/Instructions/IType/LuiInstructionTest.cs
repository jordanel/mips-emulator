using MIPS_Emulator.Instructions.IType;
using NUnit.Framework;

namespace MIPS_Emulator.Test.Instructions.IType {
	public class LuiInstructionTest {
		private uint pc;
		private Registers reg;
		private MemoryMapper mem;
		private LuiInstruction target;

		[SetUp]
		public void SetUp() {
			pc = 0x00000000;
			reg = new Registers();
			mem = new MemoryMapper(8);
		}

		[Test]
		public void Execute_ImmediateInUpperBits() {
			target = new LuiInstruction(8, 0xABCD);
			
			target.Execute(ref pc, mem, reg);
			
			Assert.AreEqual(0xABCD0000, reg[8]);
			Assert.AreEqual(0x00000004, pc);
		}

		[Test]
		public void ToString_Formatted() {
			target = new LuiInstruction(8, 0xABCD);
			
			Assert.AreEqual("LUI $t0, 0xABCD", target.ToString());
		}
	}
}