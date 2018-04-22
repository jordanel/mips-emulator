
using System;

namespace MIPS_Emulator.Instructions.RType {
	public class SltuInstruction : RTypeInstruction {
		protected override string Name => "SLTU";

		public SltuInstruction(uint d, uint s, uint t) : base(d, s, t) {
			
		}

		public override void execute(ref uint pc, MemoryUnit mem, Registers reg) {
			pc += 4;
			Console.Error.WriteLine("NOT IMPLEMENTED!");
		}
	}
}