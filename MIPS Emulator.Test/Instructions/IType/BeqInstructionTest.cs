using MIPS_Emulator.Instructions.IType;
using NUnit.Framework;

namespace MIPS_Emulator.Test.Instructions.IType {
	public class BeqInstructionTest {
		private uint pc;
		private Registers reg;
		private MemoryMapper mem;
		private BeqInstruction target;
		
		[SetUp]
		public void SetUp() {
			pc = 0x00000000;
			reg = new Registers();
			mem = new MemoryMapper(8);
		}
		
		[Test]
		public void Execute_NoBranch() {
			reg[1] = 1;
			target = new BeqInstruction(1, 0, 0xFFFF);
			
			target.Execute(ref pc, mem, reg);
			
			Assert.AreEqual(0x00000004, pc);
		}

		[Test]
		public void Execute_WithBranch() {
			reg[1] = 0;
			target = new BeqInstruction(1, 0, 0x0002);
			
			target.Execute(ref pc, mem, reg);
			
			Assert.AreEqual(0x0000000C, pc);
		}

		[Test]
		public void ToString_Formatted() {
			target = new BeqInstruction(5, 6, 0x1234);
			
			Assert.AreEqual("BEQ $a1, $a2, 0x1234", target.ToString());
		}
	}
}