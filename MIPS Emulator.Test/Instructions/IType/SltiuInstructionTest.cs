using MIPS_Emulator.Instructions.IType;
using NUnit.Framework;

namespace MIPS_Emulator.Test.Instructions.IType {
	public class SltiuInstructionTest {
		private uint pc;
		private Registers reg;
		private MemoryMapper mem;
		private SltiuInstruction target;

		[SetUp]
		public void SetUp() {
			pc = 0x00000000;
			reg = new Registers();
			mem = new MemoryMapper(8);
		}

		[Test]
		public void Execute_LessThanImmediate() {
			reg[8] = 0x00000000;
			target = new SltiuInstruction(9, 8, 0x0001);
			
			target.Execute(ref pc, mem, reg);
			
			Assert.AreEqual(0x00000001, reg[9]);
			Assert.AreEqual(0x00000004, pc);
		}
		
		[Test]
		public void Execute_LessThanImmediate_UnsignedComparison() {
			reg[8] = 0xFFFFFFFF;
			target = new SltiuInstruction(9, 8, 0x0000);
			
			target.Execute(ref pc, mem, reg);
			
			Assert.AreEqual(0x00000000, reg[9]);
			Assert.AreEqual(0x00000004, pc);
		}
		
		[Test]
		public void Execute_NotLessThanImmediate() {
			reg[8] = 0x00000001;
			target = new SltiuInstruction(9, 8, 0x0000);
			
			target.Execute(ref pc, mem, reg);
			
			Assert.AreEqual(0x00000000, reg[9]);
			Assert.AreEqual(0x00000004, pc);
		}

		[Test]
		public void ToString_Formatted() {
			target = new SltiuInstruction(9, 8, 0x0000);
			
			Assert.AreEqual("SLTIU $t1, $t0, 0x0000", target.ToString());
		}
	}
}