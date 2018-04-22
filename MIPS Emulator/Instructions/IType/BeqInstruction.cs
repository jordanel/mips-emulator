
using System;

namespace MIPS_Emulator.Instructions.IType {
	public class BeqInstruction : ITypeInstruction{
		protected override string Name => "BEQ";
		
		public BeqInstruction(int immediate, uint s, uint t) : base(t, s, immediate) {
			
		}
		
		public override void execute(ref uint pc, MemoryUnit mem, Registers reg) {
			pc += 4;
			Console.Error.WriteLine("NOT IMPLEMENTED!");
		}
		
		public override string ToString() {
			return $"{Name} {Registers.RegisterToName(S)} {Registers.RegisterToName(T)} {Immediate}";
		}
	}
}
