using MIPS_Emulator.Instructions;
using MIPS_Emulator.Instructions.IType;
using MIPS_Emulator.Instructions.JType;
using MIPS_Emulator.Instructions.RType;
using NUnit.Framework;

namespace MIPS_Emulator.Test {
	public class InstructionFactoryTest {
		private InstructionFactory target;
		
		[SetUp]
		public void SetUp() {
			target = new InstructionFactory();
		}
		
		[Test]
		public void CreateInstruction_ADD() {
			Instruction i = target.CreateInstruction(0x00000020);
			
			Assert.AreEqual(typeof(AddInstruction), i.GetType());
		}

		[Test]
		public void CreateInstruction_SUB() {
			Instruction i = target.CreateInstruction(0x00000022);
			
			Assert.AreEqual(typeof(SubInstruction), i.GetType());
		}
		
		[Test]
		public void CreateInstruction_AND() {
			Instruction i = target.CreateInstruction(0x00000024);
			
			Assert.AreEqual(typeof(AndInstruction), i.GetType());
		}
		
		[Test]
		public void CreateInstruction_OR() {
			Instruction i = target.CreateInstruction(0x00000025);
			
			Assert.AreEqual(typeof(OrInstruction), i.GetType());
		}
		
		[Test]
		public void CreateInstruction_XOR() {
			Instruction i = target.CreateInstruction(0x00000026);
			
			Assert.AreEqual(typeof(XorInstruction), i.GetType());
		}
		
		[Test]
		public void CreateInstruction_NOR() {
			Instruction i = target.CreateInstruction(0x00000027);
			
			Assert.AreEqual(typeof(NorInstruction), i.GetType());
		}
		
		[Test]
		public void CreateInstruction_SLT() {
			Instruction i = target.CreateInstruction(0x0000002A);
			
			Assert.AreEqual(typeof(SltInstruction), i.GetType());
		}
		
		[Test]
		public void CreateInstruction_SLTU() {
			Instruction i = target.CreateInstruction(0x0000002B);
			
			Assert.AreEqual(typeof(SltuInstruction), i.GetType());
		}
		
		[Test]
		public void CreateInstruction_SLL() {
			Instruction i = target.CreateInstruction(0x00000000);
			
			Assert.AreEqual(typeof(SllInstruction), i.GetType());
		}
		
		[Test]
		public void CreateInstruction_SLLV() {
			Instruction i = target.CreateInstruction(0x00000004);
			
			Assert.AreEqual(typeof(SllvInstruction), i.GetType());
		}
		
		[Test]
		public void CreateInstruction_SRL() {
			Instruction i = target.CreateInstruction(0x00000002);
			
			Assert.AreEqual(typeof(SrlInstruction), i.GetType());
		}
		
		[Test]
		public void CreateInstruction_SRA() {
			Instruction i = target.CreateInstruction(0x00000003);
			
			Assert.AreEqual(typeof(SraInstruction), i.GetType());
		}
		
		[Test]
		public void CreateInstruction_JR() {
			Instruction i = target.CreateInstruction(0x00000008);
			
			Assert.AreEqual(typeof(JrInstruction), i.GetType());
		}
		
		[Test]
		public void CreateInstruction_LW() {
			Instruction i = target.CreateInstruction(0x8C000000);
			
			Assert.AreEqual(typeof(LwInstruction), i.GetType());
		}
		
		[Test]
		public void CreateInstruction_SW() {
			Instruction i = target.CreateInstruction(0xAC000000);
			
			Assert.AreEqual(typeof(SwInstruction), i.GetType());
		}

		[Test]
		public void CreateInstruction_ADDI() {
			Instruction i = target.CreateInstruction(0x20000000);
			
			Assert.AreEqual(typeof(AddiInstruction), i.GetType());
		}

		[Test]
		public void CreateInstruction_ADDIU() {
			Instruction i = target.CreateInstruction(0x24000000);
			
			Assert.AreEqual(typeof(AddiuInstruction), i.GetType());
		}
		
		[Test]
		public void CreateInstruction_SLTI() {
			Instruction i = target.CreateInstruction(0x28000000);
			
			Assert.AreEqual(typeof(SltiInstruction), i.GetType());
		}
		
		[Test]
		public void CreateInstruction_SLTIU() {
			Instruction i = target.CreateInstruction(0x2C000000);
			
			Assert.AreEqual(typeof(SltiuInstruction), i.GetType());
		}
		
		[Test]
		public void CreateInstruction_ORI() {
			Instruction i = target.CreateInstruction(0x34000000);
			
			Assert.AreEqual(typeof(OriInstruction), i.GetType());
		}
		
		[Test]
		public void CreateInstruction_LUI() {
			Instruction i = target.CreateInstruction(0x3C000000);
			
			Assert.AreEqual(typeof(LuiInstruction), i.GetType());
		}
		
		[Test]
		public void CreateInstruction_BEQ() {
			Instruction i = target.CreateInstruction(0x10000000);
			
			Assert.AreEqual(typeof(BeqInstruction), i.GetType());
		}
		
		[Test]
		public void CreateInstruction_BNE() {
			Instruction i = target.CreateInstruction(0x14000000);
			
			Assert.AreEqual(typeof(BneInstruction), i.GetType());
		}
		
		[Test]
		public void CreateInstruction_J() {
			Instruction i = target.CreateInstruction(0x08000000);
			
			Assert.AreEqual(typeof(JInstruction), i.GetType());
		}
		
		[Test]
		public void CreateInstruction_JAL() {
			Instruction i = target.CreateInstruction(0x0C000000);
			
			Assert.AreEqual(typeof(JalInstruction), i.GetType());
		}

		[Test]
		public void UnknownOpcode_ThrowsException() {
			Assert.Throws<InstructionFactory.UnknownInstructionException>(
				() => target.CreateInstruction(0xFFFFFFFF)
			);
		}
		
		[Test]
		public void UnknownFunc_ThrowsException() {
			Assert.Throws<InstructionFactory.UnknownInstructionException>(
				() => target.CreateInstruction(0x00FFFFFF)
			);
		}

		[Test]
		public void ValidInstruction_DoesNotThrow() {
			Assert.DoesNotThrow(() => target.CreateInstruction(0x0));
		}
	}
}