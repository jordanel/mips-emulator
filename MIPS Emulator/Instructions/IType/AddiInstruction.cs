
using System;

namespace MIPS_Emulator.Instructions.IType {
	public class AddiInstruction : ITypeInstruction{
		protected override string Name => "ADDI";
		
		public AddiInstruction(int immediate, uint s, uint t) : base(t, s, immediate) {
			
		}
		
		public override void execute(ref uint pc, MemoryUnit mem, Registers reg) {
			pc += 4;
			Console.Error.WriteLine("NOT IMPLEMENTED!");
		}
	}
}
