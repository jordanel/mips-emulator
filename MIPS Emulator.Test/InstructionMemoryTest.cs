using System;
using NUnit.Framework;

namespace MIPS_Emulator.Test {
	public class InstructionMemoryTest {
		private InstructionMemory target;

		[Test]
		//[Ignore("Ignoring while instructions are implemented")]
		public void TestInitInstructionMemory() {

			uint[] instructions = new uint[] {
				0x201d003c, 0x3c08ffff, 0x3508ffff, 0x2009ffff, 0x1509001b, 0x00084600, 0x3508f000, 0x00084203, 0x00084102,
				0x340a0003, 0x01495022, 0x01484004, 0x010a582a, 0x010a582b, 0x20080005, 0x2d0b000a, 0x2d0b0004, 0x2008fffb,
				0x2d0b0005, 0x3c0b1010, 0x356b1010, 0x3c0c0101, 0x218c1010, 0x016c6824, 0x016c6825, 0x016c6826, 0x016c6827,
				0x8c040004, 0x20840002, 0x2484fffe, 0x0c000021, 0xac020000, 0x08000020, 0x23bdfff8, 0xafbf0004, 0xafa40000,
				0x28880002, 0x11000002, 0x00041020, 0x0800002e, 0x2084ffff, 0x0c000021, 0x8fa40000, 0x00441020, 0x00441020,
				0x2042ffff, 0x8fbf0004, 0x23bd0008, 0x03e00008
			};

			target = new InstructionMemory(instructions);

			uint pc = 0;
			var dataMemory = new MemoryUnit(1000);
			var registers = new Registers();

			while (pc < instructions.Length * 4) {
				Console.WriteLine($"{pc:X8}: {target[pc]}");
				target[pc].Execute(ref pc, dataMemory, registers);
				pc += 4;
			}			
		}
	
	}
}