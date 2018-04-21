
using System;

namespace MIPS_Emulator.Instructions.IType {
	public class OriInstruction : ITypeInstruction{
		public OriInstruction(uint immediate, uint s, uint t) : base(immediate, s, t) {
			
		}

		protected override string name => "ORI";
		

		
		public override void execute(ref uint pc, ref MemoryUnit mem, ref Registers reg) {
			pc += 4;
			Console.Error.Write("NOT IMPLEMENTED!");
		}
	}
}
