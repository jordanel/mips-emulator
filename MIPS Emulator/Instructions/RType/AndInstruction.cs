using System;

namespace MIPS_Emulator.Instructions.RType {
	public class AndInstruction : RTypeInstruction {
		protected override string Name => "AND";
		
		public AndInstruction(uint d, uint s, uint t) : base(d, s, t) {
			
		}

		public override void execute(ref uint pc, MemoryUnit mem, Registers reg) {
			pc += 4;
			Console.Error.Write("NOT IMPLEMENTED!");
		}
	}
}