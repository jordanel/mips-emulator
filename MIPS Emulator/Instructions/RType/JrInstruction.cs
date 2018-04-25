
using System;

namespace MIPS_Emulator.Instructions.RType {
	public class JrInstruction : RTypeInstruction {
		protected override string Name => "JR";
		
		public JrInstruction(uint s) : base(0, s, 0) {
			
		}

		public override void Execute(ref uint pc, MemoryMapper mem, Registers reg) {
			pc = reg[S];
		}

		public override string ToString() {
			return $"{Name} {Registers.RegisterToName(S)}";
		}
	}
}