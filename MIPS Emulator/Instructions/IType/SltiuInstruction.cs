
using System;

namespace MIPS_Emulator.Instructions.IType {
	public class SltiuInstruction : ITypeInstruction{
		protected override string Name => "SLTIU";
		
		public SltiuInstruction(uint t, uint s, uint immediate) : base(t, s, immediate) {
			
		}
		
		public override void Execute(ref uint pc, MemoryUnit mem, Registers reg) {
			bool isLessThanImmediate = reg[S] < SignExtend(Immediate);
			reg[T] = (uint) (isLessThanImmediate ? 1 : 0);
			pc += 4;
			Console.Error.Write("NOT IMPLEMENTED!");
		}
	}
}
