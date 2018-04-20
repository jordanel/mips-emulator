
using System;

namespace MIPS_Emulator.Instructions.RType {
	public class JrInstruction : RTypeInstruction {

		public AddInstruction(uint d, uint s, uint t) : base(InstructionFactory.JR, d, s, t) {
			
		}

		protected override string name => "JR";

		public override void execute(ref uint pc, ref MemoryUnit mem, ref Registers reg) {
			pc += 4;
			Console.Error.Write("NOT IMPLEMENTED!");
		}
	}
}