
using System;

namespace MIPS_Emulator.Instructions.RType {
	public class SltInstruction : RTypeInstruction {
		protected override string Name => "SLT";

		public SltInstruction(uint d, uint s, uint t) : base(d, s, t) {
			
		}

		public override void Execute(ref uint pc, MemoryUnit mem, Registers reg) {
			bool isLessThan = (int) reg[S] < (int) reg[T];
			reg[D] = (uint) (isLessThan ? 1 : 0);
			pc += 4;
		}
	}
}