namespace MIPS_Emulator {
	public abstract class JTypeInstruction : Instruction {
		private uint immediate;
		private uint s;
		
		protected JTypeInstruction(uint instruction) {
			
		}
		
		public abstract uint Execute(uint pc, MemoryUnit mem, Registers reg);
	}
}