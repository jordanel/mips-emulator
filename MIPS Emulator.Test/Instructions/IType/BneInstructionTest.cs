using MIPS_Emulator.Instructions.IType;
using NUnit.Framework;

namespace MIPS_Emulator.Test.Instructions.IType {
	public class BneInstructionTest {
		private uint pc;
		private Registers reg;
		private MemoryUnit mem;
		private BneInstruction target;
		
		[SetUp]
		public void SetUp() {
			pc = 0x00000000;
			reg = new Registers();
			mem = new MemoryUnit(8);
		}
		
		[Test]
		public void Execute_NoBranch() {
			reg[1] = 0;
			target = new BneInstruction(1, 0, 0x00000002);
			
			target.Execute(ref pc, mem, reg);
			
			Assert.AreEqual(0x00000004, pc);
		}
		
		[Test]
		public void Execute_WithBranch() {
			reg[1] = 1;
			target = new BneInstruction(1, 0, 0x00000004);
			
			target.Execute(ref pc, mem, reg);
			
			Assert.AreEqual(0x00000010, pc);
		}

		[Test]
		public void ToString_Formatted() {
			target = new BneInstruction(5, 6, 0x1234);
			
			Assert.AreEqual("BNE $a1, $a2, 0x1234", target.ToString());
		}
	}
}