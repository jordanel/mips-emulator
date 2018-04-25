using System;
using MIPS_Emulator.Instructions;
using MIPS_Emulator.Instructions.IType;
using MIPS_Emulator.Instructions.RType;
using NUnit.Framework;

namespace MIPS_Emulator.Test {
	public class InstructionMemoryTest {
		private InstructionMemory target;

		[SetUp]
		public void SetUp() {
			Instruction instr1 = new AddInstruction(2, 3, 4);
			Instruction instr2 = new LuiInstruction(5, 0xFFFF);
			Instruction[] instructions = { instr1, instr2 };
			target = new InstructionMemory(instructions);
		}
		
		[Test]
		public void SetInstruction_ValidIndex() {
			target = new InstructionMemory(8);
			Instruction instr = new AddInstruction(1, 1, 2);

			target[4] = instr;
			
			Assert.AreEqual(instr, target[4]);
		}

		[Test]
		public void SetInstruction_InvalidIndex_ThrowsArgumentException() {
			Instruction instr = new AddInstruction(1, 1, 2);

			Assert.Throws<ArgumentException>(
				() => target[3] = instr
			);
		}

		[Test]
		public void GetInstruction_InvalidIndex_ThrowsArgumentException() {
			Instruction instr;
			
			Assert.Throws<ArgumentException>(
				() => instr = target[3]
			);
		}
	}
}