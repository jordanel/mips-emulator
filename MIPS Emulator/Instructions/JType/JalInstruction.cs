using System;

namespace MIPS_Emulator.Instructions.JType {
	public class JalInstruction : JTypeInstruction {
		public JalInstruction(uint immediate) : base(immediate) {
			
		}

		protected override string name => "JAL";
		
		public override void execute(ref uint pc, ref MemoryUnit mem, ref Registers reg) {
			pc += 4;
			Console.Error.Write("NOT IMPLEMENTED!");
		}
	}
}