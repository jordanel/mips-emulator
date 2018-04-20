
using System;

namespace MIPS_Emulator.Instructions.IType {
	public class BeqInstruction : ITypeInstruction{
		public BeqInstruction(uint immediate, uint s, uint t) : base(immediate, s, t) {
			
		}

		protected override string name => "BEQ";
		

		public override string ToString() {
			return $"{name} {Registers.RegisterToName(s)} {Registers.RegisterToName(t)} {immediate}";
		}

		
		public override void execute(ref uint pc, ref MemoryUnit mem, ref Registers reg) {
			pc += 4;
			Console.Error.Write("NOT IMPLEMENTED!");
		}
	}
}
