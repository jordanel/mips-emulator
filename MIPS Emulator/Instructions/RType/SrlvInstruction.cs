
using System;

namespace MIPS_Emulator.Instructions.RType {
	public class SrlvInstruction : RTypeInstruction {
		protected override string Name => "SRLV";

		public SrlvInstruction(uint d, uint t, uint s) : base(d, s, t) {
			
		}

		public override void Execute(ref uint pc, MemoryMapper mem, Registers reg) {
			reg[D] = reg[T] >> (int) reg[S];
			pc += 4;
		}
		
		public override string ToString() {
			return $"{Name} {Registers.RegisterToName(D)}, {Registers.RegisterToName(T)}, {Registers.RegisterToName(S)}";
		}
	}
}