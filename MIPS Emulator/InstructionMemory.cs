using System;
using MIPS_Emulator.Instructions;

namespace MIPS_Emulator {
	public class InstructionMemory : MemoryUnit {
		private readonly Instruction[] iMem;
		public uint Size => (uint) iMem.Length * 4;
		public InstructionFactory InstrFact { get; set; }
		
		public InstructionMemory(uint size) {
			iMem = new Instruction[size];
			InstrFact = new InstructionFactory();
		}

		public InstructionMemory(Instruction[] instructions) {
			iMem = instructions;
			InstrFact = new InstructionFactory();
		}
		
		public uint this[uint pc] {
			get {
				throw new NotImplementedException();
			}
			
			set {
				if (pc % 4 == 0) {
					iMem[pc / 4] = InstrFact.CreateInstruction(value);
				} else {
					throw new ArgumentException($"Index ({pc}) into instruction memory is not a multiple of 4");
				}
			}
		}

		public Instruction GetInstruction(uint pc) {
			if (pc % 4 == 0) {
				return iMem[pc / 4];
			} else {
				throw new ArgumentException($"Index ({pc}) into instruction memory is not a multiple of 4");
			}
		}
	}
}