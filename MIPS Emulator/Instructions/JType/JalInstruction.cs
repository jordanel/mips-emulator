using System;

namespace MIPS_Emulator.Instructions.JType {
	public class JalInstruction : JTypeInstruction {
		protected override string Name => "JAL";
		
		public JalInstruction(uint target) : base(target) {
			
		}
		
		public override void Execute(ref uint pc, MemoryUnit mem, Registers reg) {
			reg[31] = pc + 4;
			pc = (pc & 0xF0000000) | (Target << 2);
		}
	}
}