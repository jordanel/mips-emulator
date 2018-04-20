using MIPS_Emulator.Insturctions;

namespace MIPS_Emulator {
	public abstract class JTypeInstruction : Instruction {
		private uint immediate;
		private uint s;
		
		protected JTypeInstruction(uint instruction) {
			
		}

		public abstract void execute(ref uint pc, ref MemoryUnit mem, ref Registers reg);
	}
}