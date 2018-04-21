using System;
using MIPS_Emulator.Instructions;

namespace MIPS_Emulator {
	public class InstructionMemory {
		private readonly Instruction[] iMem;

		public Instruction this[uint i] {

			get => i % 4 == 0 ? iMem[i / 4] : invalidIndex(i);
			set {
				if (i % 4 == 0) {
					iMem[i / 4] = value;
				}
				else {
					invalidIndex(i);
				}
			}
		}

		private static Instruction invalidIndex(uint index) {
			throw new ArgumentException($"Index ({index}) into instruction memory is not a multiple of 4");
		}
		
		public InstructionMemory(uint size) {
			iMem = new Instruction[size];
		}

		public InstructionMemory(uint[] instructions) {
			iMem = new Instruction[instructions.Length];

			for (var i = 0; i < instructions.Length; i++) {
				iMem[i] = InstructionFactory.createInstruction(instructions[i]);
			}
			
		}
	}
}