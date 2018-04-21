
using System;

namespace MIPS_Emulator.Instructions.IType {
	public class SltiuInstruction : ITypeInstruction{
		protected override string Name => "SLTIU";
		
		public SltiuInstruction(int immediate, uint s, uint t) : base(t, s, immediate) {
			
		}
		
		public override void execute(ref uint pc, MemoryUnit mem, Registers reg) {
			pc += 4;
			Console.Error.Write("NOT IMPLEMENTED!");
		}
	}
}
