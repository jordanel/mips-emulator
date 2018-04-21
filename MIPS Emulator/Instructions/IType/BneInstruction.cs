
using System;

namespace MIPS_Emulator.Instructions.IType {
	public class BneInstruction : ITypeInstruction{
		protected override string Name => "BNE";
		
		public BneInstruction(int immediate, uint s, uint t) : base(t, s, immediate) {
			
		}

		public override void execute(ref uint pc, MemoryUnit mem, Registers reg) {
			pc += 4;
			Console.Error.Write("NOT IMPLEMENTED!");
		}
		
		public override string ToString() {
			return $"{Name} {Registers.RegisterToName(S)} {Registers.RegisterToName(T)} {Immediate}";
		}
	}
}
