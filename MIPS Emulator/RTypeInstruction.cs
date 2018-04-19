namespace MIPS_Emulator {
	public abstract class RTypeInstruction : Instruction {
		private uint func;
		private int d, s, t;

		protected RTypeInstruction(uint instruction) {
			
		}

		public abstract uint Execute(uint pc, MemoryUnit mem, Registers reg);
	}
}