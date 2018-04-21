
using System;

namespace MIPS_Emulator.Instructions.RType {
	public class SraInstruction : RTypeInstruction {

		public SraInstruction(uint d, uint s, uint t) : base(InstructionFactory.SRA, d, s, t) {
			
		}

		protected override string name => "SRA";

		public override void execute(ref uint pc, ref MemoryUnit mem, ref Registers reg) {
			pc += 4;
			Console.Error.Write("NOT IMPLEMENTED!");
		}
	}
}