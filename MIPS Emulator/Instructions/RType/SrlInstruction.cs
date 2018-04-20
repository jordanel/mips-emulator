
using System;

namespace MIPS_Emulator.Instructions.RType {
	public class SrlInstruction : RTypeInstruction {

		public AddInstruction(uint d, uint s, uint t) : base(InstructionFactory.SRL, d, s, t) {
			
		}

		protected override string name => "SRL";

		public override void execute(ref uint pc, ref MemoryUnit mem, ref Registers reg) {
			pc += 4;
			Console.Error.Write("NOT IMPLEMENTED!");
		}
	}
}