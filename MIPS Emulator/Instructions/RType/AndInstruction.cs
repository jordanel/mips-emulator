using System;

namespace MIPS_Emulator.Instructions.RType {
	public class AndInstruction : RTypeInstruction {
		protected override string Name => "AND";
		
		public AndInstruction(uint d, uint s, uint t) : base(d, s, t) {
			
		}

		public override void Execute(ref uint pc, MemoryUnit mem, Registers reg) {
			reg[D] = reg[S] & reg[T];
			pc += 4;
		}
	}
}