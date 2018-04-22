
using System;

namespace MIPS_Emulator.Instructions.RType {
	public class OrInstruction : RTypeInstruction {
		protected override string Name => "OR";

		public OrInstruction(uint d, uint s, uint t) : base(d, s, t) {
			
		}

		public override void Execute(ref uint pc, MemoryUnit mem, Registers reg) {
			pc += 4;
			Console.Error.Write("NOT IMPLEMENTED!");
		}
	}
}