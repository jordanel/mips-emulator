
using System;

namespace MIPS_Emulator.Instructions.IType {
	public class OriInstruction : ITypeInstruction{
		protected override string Name => "ORI";
		
		public OriInstruction(int immediate, uint s, uint t) : base(t, s, immediate) {
			
		}
		
		public override void execute(ref uint pc, MemoryUnit mem, Registers reg) {
			pc += 4;
			Console.Error.Write("NOT IMPLEMENTED!");
		}
	}
}
