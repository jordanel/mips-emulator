using System;
using MIPS_Emulator.Instructions;
using NUnit.Framework;

namespace MIPS_Emulator.Test {
	public class InstructionFactoryTest {
		private InstructionFactory target;
		
		[Test]
		public void Test() {
			Instruction i = InstructionFactory.createInstruction(0x0);
			uint pc = 5;
			MemoryUnit m = new MemoryUnit(5);
			Registers r = new Registers();
			i.execute(ref pc, ref m, ref r);
			Console.WriteLine(pc);
			Console.WriteLine(i);

		}

		[Test]
		public void UnknownInstruction_ThrowsException() {
			Assert.Throws<InstructionFactory.UnknownInstructionException>(
				() => InstructionFactory.createInstruction(0xFFFFFFFF)
			);
			
		}

		[Test]
		public void ValidInstruction_DoesNotThrow() {
			Assert.DoesNotThrow(() => InstructionFactory.createInstruction(0x0));
		}
	}
}