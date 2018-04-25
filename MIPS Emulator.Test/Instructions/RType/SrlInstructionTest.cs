using MIPS_Emulator.Instructions.RType;
using NUnit.Framework;

namespace MIPS_Emulator.Test.Instructions.RType {
	public class SrlInstructionTest {
		private uint pc;
		private Registers reg;
		private MemoryMapper mem;
		private SrlInstruction target;

		[SetUp]
		public void SetUp() {
			pc = 0x0;
			reg = new Registers();
			mem = new MemoryMapper(8);
		}
		
		[Test]
		public void Execute_ShiftsInZeroes() {
			reg[17] = 0x82345678;
			target = new SrlInstruction(16, 17, 8);
			
			target.Execute(ref pc, mem, reg);
			
			Assert.AreEqual(0x00823456, reg[16]);
			Assert.AreEqual(0x4, pc);
		}

		[Test]
		public void ToString_Formatted() {
			target = new SrlInstruction(16, 17, 8);
			
			Assert.AreEqual("SRL $s0, $s1, 8", target.ToString());
		}
	}
}