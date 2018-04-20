namespace MIPS_Emulator.Instructions.JType {
	public abstract class JTypeInstruction : Instruction {
		private uint immediate;
		private uint s;
		
		protected abstract string name { get; }
		
		protected JTypeInstruction(uint immediate, uint s) {
			this.immediate = immediate;
			this.s = s;
		}
		
		public override string ToString() {
			return $"{name} {immediate}";
		}

		public abstract void execute(ref uint pc, ref MemoryUnit mem, ref Registers reg);
	}
}