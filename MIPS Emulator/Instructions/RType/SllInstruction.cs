
using System;

namespace MIPS_Emulator.Instructions.RType {
	public class SllInstruction : RTypeInstruction {

		public SllInstruction(uint d, uint s, uint t) : base(InstructionFactory.SLL, d, s, t) {
			
		}

		protected override string name => "SLL";

		public override void execute(ref uint pc, ref MemoryUnit mem, ref Registers reg) {
			pc += 4;
			Console.Error.Write("NOT IMPLEMENTED!");
		}
	}
}