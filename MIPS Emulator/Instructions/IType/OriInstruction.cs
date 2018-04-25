
using System;

namespace MIPS_Emulator.Instructions.IType {
	public class OriInstruction : ITypeInstruction{
		protected override string Name => "ORI";
		
		public OriInstruction(uint t, uint s, uint immediate) : base(t, s, immediate) {
			
		}
		
		public override void Execute(ref uint pc, MemoryMapper mem, Registers reg) {
			reg[T] = reg[S] | Immediate;
			pc += 4;
		}
	}
}
