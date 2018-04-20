
using System;

namespace MIPS_Emulator.Instructions.IType {
	public class AddiInstruction : ITypeInstruction{
		public AddiInstruction(uint immediate, uint s, uint t) : base(immediate, s, t) {
			
		}

		protected override string name => "ADDI";
		

		
		public override void execute(ref uint pc, ref MemoryUnit mem, ref Registers reg) {
			pc += 4;
			Console.Error.Write("NOT IMPLEMENTED!");
		}
	}
}
