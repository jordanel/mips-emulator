
using System;

namespace MIPS_Emulator.Instructions.RType {
	public class SrlInstruction : RTypeInstruction {
		protected override string Name => "SRL";

		public SrlInstruction(uint d, uint s, uint t) : base(d, s, t) {
			
		}

		public override void execute(ref uint pc, MemoryUnit mem, Registers reg) {
			pc += 4;
			Console.Error.Write("NOT IMPLEMENTED!");
		}
	}
}