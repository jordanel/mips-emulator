using System;
using MIPS_Emulator.Instructions;

namespace MIPS_Emulator {
	public class InstructionMemory : MemoryUnit {
		private readonly Instruction[] iMem;
		public uint Size => (uint) iMem.Length * WordSize;
		public uint WordSize { get; }
		public InstructionFactory InstrFact { get; set; }
		
		public InstructionMemory(uint size, uint wordSize = 4) {
			iMem = new Instruction[size];
			InstrFact = new InstructionFactory();
			WordSize = wordSize;
		}

		public InstructionMemory(Instruction[] instructions, uint wordSize = 4) {
			iMem = instructions;
			InstrFact = new InstructionFactory();
			WordSize = wordSize;
		}
		
		public uint this[uint pc] {
			get {
				throw new NotImplementedException();
			}
			
			set {
				if (pc % WordSize == 0) {
					iMem[pc / WordSize] = InstrFact.CreateInstruction(value);
				} else {
					throw new ArgumentException($"Index ({pc}) into instruction memory is not a multiple of word size ({WordSize})");
				}
			}
		}

		public Instruction GetInstruction(uint pc) {
			if (pc % WordSize == 0) {
				return iMem[(pc & 0xffff) / WordSize];
			} else {
				throw new ArgumentException($"Index ({pc}) into instruction memory is not a multiple of word size ({WordSize})");
			}
		}
	}
}