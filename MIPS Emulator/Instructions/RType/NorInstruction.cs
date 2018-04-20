
using System;

namespace MIPS_Emulator.Instructions.RType {
	public class NorInstruction : RTypeInstruction {

		public NorInstruction(uint d, uint s, uint t) : base(InstructionFactory.NOR, d, s, t) {
			
		}

		protected override string name => "NOR";

		public override void execute(ref uint pc, ref MemoryUnit mem, ref Registers reg) {
			pc += 4;
			Console.Error.Write("NOT IMPLEMENTED!");
		}
	}
}