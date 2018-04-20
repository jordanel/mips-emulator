using MIPS_Emulator.Insturctions;

namespace MIPS_Emulator {
	public abstract class ITypeInstruction : Instruction {
		private uint immediate;
		private int s, t;

		protected ITypeInstruction(uint immediate, int s, int t) {
			this.immediate = immediate;
			this.s = s;
			this.t = t;
		}

		public abstract void execute(ref uint pc, ref MemoryUnit mem, ref Registers reg);

	}
}