using MIPS_Emulator.Instructions.RType;
using NUnit.Framework;

namespace MIPS_Emulator.Test.Instructions.RType {
	public class SltuInstructionTest {
		private uint pc;
		private Registers reg;
		private MemoryUnit mem;
		private SltuInstruction target;

		[SetUp]
		public void SetUp() {
			pc = 0x0;
			reg = new Registers();
			mem = new MemoryUnit(8);
		}
		
		[Test]
		public void Execute_IsLessThan() {
			reg[17] = 0x0;
			reg[18] = 0x1;
			target = new SltuInstruction(16, 17, 18);
			
			target.Execute(ref pc, mem, reg);
			
			Assert.AreEqual(0x1, reg[16]);
			Assert.AreEqual(0x4, pc);
		}

		[Test]
		public void Execute_IsNotLessThan() {
			reg[17] = 0x1;
			reg[18] = 0x0;
			target = new SltuInstruction(16, 17, 18);
			
			target.Execute(ref pc, mem, reg);
			
			Assert.AreEqual(0x0, reg[16]);
			Assert.AreEqual(0x4, pc);
		}
		
		[Test]
		public void Execute_IsNotLessThan_UnSignedComparison() {
			reg[17] = 0xFFFFFFFF;
			reg[18] = 0x00000000;
			target = new SltuInstruction(16, 17, 18);
			
			target.Execute(ref pc, mem, reg);
			
			Assert.AreEqual(0x0, reg[16]);
			Assert.AreEqual(0x4, pc);
		}

		[Test]
		public void ToString_Formatted() {
			target = new SltuInstruction(16, 17, 18);
			
			Assert.AreEqual("SLTU $s0, $s1, $s2", target.ToString());
		}
	}
}