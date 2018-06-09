using System;
using System.IO;
using MIPS_Emulator.Instructions;
using NUnit.Framework;

namespace MIPS_Emulator.Test {
	public class ProgramLoaderTest {
		private ProgramLoader target;
		
		[SetUp]
		public void SetUp() {
			FileInfo file = GetFileInfoFromPath("TestProjects/Project1/no_errors.json");

			target = new ProgramLoader(file);
		}

		private FileInfo GetFileInfoFromPath(String path) {
			string dir = Path.GetDirectoryName(typeof(ProgramLoaderTest).Assembly.Location);
			FileInfo file = new FileInfo(Path.Combine(dir, path));
			return file;
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
			
			Assert.AreEqual(expected.ToString(), target.Mips.InstrMem.GetInstruction(4).ToString());
		}

		[Test]
		public void InstructionsInitialized_WithComment() {
			InstructionFactory instrFact = new InstructionFactory();
			Instruction expected = instrFact.CreateInstruction(0x201d003c);
			
			Assert.AreEqual(expected.ToString(), target.Mips.InstrMem.GetInstruction(0).ToString());
		}

		[Test]
		public void MemoryInitialized_WithStartAddr() {
			Assert.AreEqual(3, target.Mips.Memory[4]);
		}
		
		[Test]
		public void MemoryInitialized_WithStartAndEndAddr() {
			Assert.AreEqual(0xf00, target.Mips.Memory[400]);
			Assert.AreEqual(0x0f0, target.Mips.Memory[2000]);
		}

		[Test]
		public void MemoryInitialized_WithStartAddrAndSize() {
			Assert.AreEqual(0, target.Mips.Memory[200]);
			Assert.AreEqual(1, target.Mips.Memory[396]);
		}
		
		[Test]
		public void MemoryInitialized_WithBitmask() {
			Assert.AreEqual(0xf00, target.Mips.Memory[2048]);
		}

		[Test]
		public void NonexistentMemoryUnitType() {
			FileInfo file = GetFileInfoFromPath("TestProjects/Project1/nonexistent_memory_type.json");
			Assert.Throws<ArgumentException>(
				() => new ProgramLoader(file)
			);
		}
		
		[Test]
		public void InvalidMemoryUnitType() {
			FileInfo file = GetFileInfoFromPath("TestProjects/Project1/invalid_memory_type.json");
			Assert.Throws<ArgumentException>(
				() => new ProgramLoader(file)
			);
		}
	}
}