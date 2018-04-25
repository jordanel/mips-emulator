using MIPS_Emulator.Instructions.RType;
using NUnit.Framework;

namespace MIPS_Emulator.Test.Instructions.RType {
	public class JrInstructionTest {
		private uint pc;
		private Registers reg;
		private MemoryMapper mem;
		private JrInstruction target;

		[SetUp]
		public void SetUp() {
			pc = 0x0;
			reg = new Registers();
			mem = new MemoryMapper(8);
		}

		[Test]
		public void Execute_PcSetToRegisterValue() {
			reg[16] = 0xFFFFFFFF;
			target = new JrInstruction(16);
			
			target.Execute(ref pc, mem, reg);
			
			Assert.AreEqual(0xFFFFFFFF, pc);
		}

		[Test]
		public void ToString_Formatted() {
			target = new JrInstruction(16);
			
			Assert.AreEqual("JR $s0", target.ToString());
		}
	}
}