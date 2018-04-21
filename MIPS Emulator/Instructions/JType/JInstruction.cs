using System;

namespace MIPS_Emulator.Instructions.JType {
	public class JInstruction : JTypeInstruction {
		protected override string Name => "J";
		
		public JInstruction(uint immediate) : base(immediate) {
			
		}
		
		public override void execute(ref uint pc, MemoryUnit mem, Registers reg) {
			pc += 4;
			Console.Error.Write("NOT IMPLEMENTED!");
		}
	}
}