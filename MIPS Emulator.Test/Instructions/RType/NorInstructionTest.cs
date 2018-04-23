using MIPS_Emulator.Instructions.RType;
using NUnit.Framework;

namespace MIPS_Emulator.Test.Instructions.RType {
	public class NorInstructionTest {
		private uint pc;
		private Registers reg;
		private MemoryUnit mem;
		private NorInstruction target;

		[SetUp]
		public void SetUp() {
			pc = 0x0;
			reg = new Registers();
			mem = new MemoryUnit(8);
		}

		[Test]
		public void Execute() {
			reg[17] = 0x00FFFF00;
			reg[18] = 0x0000FFFF;
			target = new NorInstruction(16, 17, 18);
			
			target.Execute(ref pc, mem, reg);
			
			Assert.AreEqual(0xFF000000, reg[16]);
			Assert.AreEqual(0x4, pc);
		}

		[Test]
		public void ToString_Formatted() {
			target = new NorInstruction(16, 17, 18);
			
			Assert.AreEqual("NOR $s0, $s1, $s2", target.ToString());
		}
	}
}