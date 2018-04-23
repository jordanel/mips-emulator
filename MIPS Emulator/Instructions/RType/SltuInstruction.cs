
using System;

namespace MIPS_Emulator.Instructions.RType {
	public class SltuInstruction : RTypeInstruction {
		protected override string Name => "SLTU";

		public SltuInstruction(uint d, uint s, uint t) : base(d, s, t) {
			
		}

		public override void Execute(ref uint pc, MemoryUnit mem, Registers reg) {
			bool isLessThan = reg[S] < reg[T];
			reg[D] = (uint) (isLessThan ? 1 : 0);
			pc += 4;
		}
	}
}