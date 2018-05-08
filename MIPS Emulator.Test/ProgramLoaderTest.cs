using System.IO;
using NUnit.Framework;

namespace MIPS_Emulator.Test {
	public class ProgramLoaderTest {
		private ProgramLoader target;
		
		[SetUp]
		public void SetUp() {
			string dir = Path.GetDirectoryName(typeof(ProgramLoaderTest).Assembly.Location);
			FileInfo file = new FileInfo($"{dir}/TestProjects/Test1/project.json");
			
			target = new ProgramLoader(file);
		}

		[Test]
		public void Test() {
			
		}
	}
}