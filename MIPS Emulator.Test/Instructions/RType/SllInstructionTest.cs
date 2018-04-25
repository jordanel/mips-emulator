using MIPS_Emulator.Instructions.RType;
using NUnit.Framework;

namespace MIPS_Emulator.Test.Instructions.RType {
	public class SllInstructionTest {
		private uint pc;
		private Registers reg;
		private MemoryMapper mem;
		private SllInstruction target;

		[SetUp]
		public void SetUp() {
			pc = 0x0;
			reg = new Registers();
			mem = new MemoryMapper(8);
		}
		
		[Test]
		public void Execute_ShiftsInZeroes() {
			reg[17] = 0x12345678;
			target = new SllInstruction(16, 17, 8);
			
			target.Execute(ref pc, mem, reg);
			
			Assert.AreEqual(0x34567800, reg[16]);
			Assert.AreEqual(0x4, pc);
		}

		[Test]
		public void ToString_Formatted() {
			target = new SllInstruction(16, 17, 8);
			
			Assert.AreEqual("SLL $s0, $s1, 8", target.ToString());
		}
	}
}