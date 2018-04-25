
using System;

namespace MIPS_Emulator.Instructions.IType {
	public class SltiInstruction : ITypeInstruction{
		protected override string Name => "SLTI";
		
		public SltiInstruction(uint t, uint s, uint immediate) : base(t, s, immediate) {
			
		}
		
		public override void Execute(ref uint pc, MemoryMapper mem, Registers reg) {
			bool isLessThanImmediate = (int) reg[S] < (int) SignExtend(Immediate);
			reg[T] = (uint) (isLessThanImmediate ? 1 : 0);
			pc += 4;
		}
	}
}
