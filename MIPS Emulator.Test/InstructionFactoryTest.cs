using System;
using MIPS_Emulator.Instructions;
using MIPS_Emulator.Instructions.RType;
using NUnit.Framework;

namespace MIPS_Emulator.Test {
	public class InstructionFactoryTest {
		
		[Test]
		public void Test() {
			Instruction i = InstructionFactory.CreateInstruction(0x00000000);
			
			Assert.AreEqual(typeof(SllInstruction), i.GetType());
			Assert.AreEqual("SLL $zero $zero $zero", i.ToString());
		}

		[Test]
		public void UnknownInstruction_ThrowsException() {
			Assert.Throws<InstructionFactory.UnknownInstructionException>(
				() => InstructionFactory.CreateInstruction(0xFFFFFFFF)
			);
		}

		[Test]
		public void ValidInstruction_DoesNotThrow() {
			Assert.DoesNotThrow(() => InstructionFactory.CreateInstruction(0x0));
		}
	}
}