using System;

namespace MIPS_Emulator.Instructions.JType {
	public class JInstruction : JTypeInstruction {
		protected override string Name => "J";
		
		public JInstruction(uint target) : base(target) {
			
		}
		
		public override void Execute(ref uint pc, MemoryMapper mem, Registers reg) {
			pc = (pc & 0xF0000000) | (Target << 2);
		}
	}
}