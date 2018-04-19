namespace MIPS_Emulator {
	public abstract class ITypeInstruction : Instruction {
		private uint immediate;
		private int s, t;
		
		protected ITypeInstruction(uint instruction) {
			
		}
		
		public abstract uint Execute(uint pc, MemoryUnit mem, Registers reg);
	}
}