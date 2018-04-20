using System;
using MIPS_Emulator.Instructions;
using MIPS_Emulator.Instructions.IType;
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
			i = new LwInstruction(123, 4, 5);
			Console.WriteLine(i);
			}
		
	}
}