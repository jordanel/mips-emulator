
using System;

namespace MIPS_Emulator.Instructions.IType {
	public class AddiuInstruction : ITypeInstruction{
		protected override string Name => "ADDIU";
		
		public AddiuInstruction(int immediate, uint s, uint t) : base(t, s, immediate) {
			
		}

		public override void execute(ref uint pc, MemoryUnit mem, Registers reg) {
			reg[T] = reg[S] + Immediate;
			pc += 4;
			Console.Error.WriteLine("NOT IMPLEMENTED!");
		}
	}
}
