using System;

namespace MIPS_Emulator.Instructions.IType {
	public class LwInstruction : ITypeInstruction{
		public LwInstruction(uint immediate, uint s, uint t) : base(immediate, s, t) {
			
		}

		protected override string name => "LW";
		
		public override void execute(ref uint pc, ref MemoryUnit mem, ref Registers reg) {
			pc += 4;
			Console.Error.Write("NOT IMPLEMENTED!");
		}
	}
}