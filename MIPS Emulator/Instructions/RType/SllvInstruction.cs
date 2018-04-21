
using System;

namespace MIPS_Emulator.Instructions.RType {
	public class SllvInstruction : RTypeInstruction {

		public SllvInstruction(uint d, uint s, uint t) : base(InstructionFactory.SLLV, d, s, t) {
			
		}

		protected override string name => "SLLV";

		public override void execute(ref uint pc, ref MemoryUnit mem, ref Registers reg) {
			pc += 4;
			Console.Error.Write("NOT IMPLEMENTED!");
		}
	}
}