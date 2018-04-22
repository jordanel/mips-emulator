
using System;

namespace MIPS_Emulator.Instructions.IType {
	public class AddiuInstruction : ITypeInstruction{
		protected override string Name => "ADDIU";
		
		public AddiuInstruction(uint t, uint s, uint immediate) : base(t, s, immediate) {
			
		}

		public override void Execute(ref uint pc, MemoryUnit mem, Registers reg) {
			reg[T] = reg[S] + SignExtend(Immediate);
			pc += 4;
		}
	}
}
