
using System;

namespace MIPS_Emulator.Instructions.RType {
	public class SubInstruction : RTypeInstruction {

		public AddInstruction(uint d, uint s, uint t) : base(InstructionFactory.SUB, d, s, t) {
			
		}

		protected override string name => "SUB";

		public override void execute(ref uint pc, ref MemoryUnit mem, ref Registers reg) {
			pc += 4;
			Console.Error.Write("NOT IMPLEMENTED!");
		}
	}
}