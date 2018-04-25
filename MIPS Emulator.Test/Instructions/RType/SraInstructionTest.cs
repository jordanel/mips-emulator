using MIPS_Emulator.Instructions.RType;
using NUnit.Framework;

namespace MIPS_Emulator.Test.Instructions.RType {
	public class SraInstructionTest {
		private uint pc;
		private Registers reg;
		private MemoryMapper mem;
		private SraInstruction target;

		[SetUp]
		public void SetUp() {
			pc = 0x0;
			reg = new Registers();
			mem = new MemoryMapper(8);
		}
		
		[Test]
		public void Execute_SignBitZero() {
			reg[17] = 0x12345678;
			target = new SraInstruction(16, 17, 8);
			
			target.Execute(ref pc, mem, reg);
			
			Assert.AreEqual(0x00123456, reg[16]);
			Assert.AreEqual(0x4, pc);
		}
		
		[Test]
		public void Execute_SignBitOne() {
			reg[17] = 0x82345678;
			target = new SraInstruction(16, 17, 8);
			
			target.Execute(ref pc, mem, reg);
			
			Assert.AreEqual(0xFF823456, reg[16]);
			Assert.AreEqual(0x4, pc);
		}

		[Test]
		public void ToString_Formatted() {
			target = new SraInstruction(16, 17, 8);
			
			Assert.AreEqual("SRA $s0, $s1, 8", target.ToString());
		}
	}
}