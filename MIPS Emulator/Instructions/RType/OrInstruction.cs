
using System;

namespace MIPS_Emulator.Instructions.RType {
	public class OrInstruction : RTypeInstruction {

		public OrInstruction(uint d, uint s, uint t) : base(InstructionFactory.OR, d, s, t) {
			
		}

		protected override string name => "OR";

		public override void execute(ref uint pc, ref MemoryUnit mem, ref Registers reg) {
			pc += 4;
			Console.Error.Write("NOT IMPLEMENTED!");
		}
	}
}