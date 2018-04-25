using MIPS_Emulator.Instructions.RType;
using NUnit.Framework;

namespace MIPS_Emulator.Test.Instructions.RType {
	public class SllvInstructionTest {
		private uint pc;
		private Registers reg;
		private MemoryMapper mem;
		private SllvInstruction target;

		[SetUp]
		public void SetUp() {
			pc = 0x0;
			reg = new Registers();
			mem = new MemoryMapper(8);
		}
		
		[Test]
		public void Execute_ShiftsInZeroes() {
			reg[17] = 0x12345678;
			reg[18] = 0x8;
			target = new SllvInstruction(16, 17, 18);
			
			target.Execute(ref pc, mem, reg);
			
			Assert.AreEqual(0x34567800, reg[16]);
			Assert.AreEqual(0x4, pc);
		}

		[Test]
		public void ToString_Formatted() {
			target = new SllvInstruction(16, 17, 18);
			
			Assert.AreEqual("SLLV $s0, $s1, $s2", target.ToString());
		}
	}
}