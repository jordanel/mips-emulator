
using System;

namespace MIPS_Emulator.Instructions.RType {
	public class SltuInstruction : RTypeInstruction {

		public SltuInstruction(uint d, uint s, uint t) : base(InstructionFactory.SLTU, d, s, t) {
			
		}

		protected override string name => "SLTU";

		public override void execute(ref uint pc, ref MemoryUnit mem, ref Registers reg) {
			pc += 4;
			Console.Error.Write("NOT IMPLEMENTED!");
		}
	}
}