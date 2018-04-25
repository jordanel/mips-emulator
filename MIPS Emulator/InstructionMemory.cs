using System;
using MIPS_Emulator.Instructions;

namespace MIPS_Emulator {
	public class InstructionMemory {
		private readonly Instruction[] iMem;
		
		public InstructionMemory(uint size) {
			iMem = new Instruction[size];
		}

		public InstructionMemory(Instruction[] instructions) {
			iMem = instructions;
		}
		
		public Instruction this[uint pc] {
			get {
				if (pc % 4 == 0) {
					return iMem[pc / 4];
				} else {
					throw new ArgumentException($"Index ({pc}) into instruction memory is not a multiple of 4");
				}
			}
			
			set {
				if (pc % 4 == 0) {
					iMem[pc / 4] = value;
				} else {
					throw new ArgumentException($"Index ({pc}) into instruction memory is not a multiple of 4");
				}
			}
		}
	}
}