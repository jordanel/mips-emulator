using System.IO;
using MIPS_Emulator.Instructions;
using NUnit.Framework;

namespace MIPS_Emulator.Test {
	public class ProgramLoaderTest {
		private ProgramLoader target;
		
		[SetUp]
		public void SetUp() {
			string dir = Path.GetDirectoryName(typeof(ProgramLoaderTest).Assembly.Location);
			FileInfo file = new FileInfo(Path.Combine(dir, "TestProjects/Test1/project.json"));
			
			target = new ProgramLoader(file);
		}

		[Test]
		public void PcSetFromFile() {
			Mips mips = target.Mips;
			
			Assert.AreEqual(4, mips.Pc);
		}

		[Test]
		public void InstructionsInitialized_BlanksIgnored() {
			Assert.AreEqual(49 * 4, target.Mips.InstrMem.Size);
		}

		[Test]
		public void InstructionsInitialized_NoComment() {
			InstructionFactory instrFact = new InstructionFactory();
			Instruction expected = instrFact.CreateInstruction(0x3c08ffff);
			
			Assert.AreEqual(expected.ToString(), target.Mips.InstrMem[4].ToString());
		}

		[Test]
		public void InstructionsInitialized_WithComment() {
			InstructionFactory instrFact = new InstructionFactory();
			Instruction expected = instrFact.CreateInstruction(0x201d003c);
			
			Assert.AreEqual(expected.ToString(), target.Mips.InstrMem[0].ToString());
		}
	}
}