using System;

namespace MIPS_Emulator.Instructions.JType {
	public class JalInstruction : JTypeInstruction {
		protected override string Name => "JAL";
		
		public JalInstruction(uint immediate) : base(immediate) {
			
		}
		
		public override void Execute(ref uint pc, MemoryUnit mem, Registers reg) {
			pc += 4;
			Console.Error.Write("NOT IMPLEMENTED!");
		}
	}
}