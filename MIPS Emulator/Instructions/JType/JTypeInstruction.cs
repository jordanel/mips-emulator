namespace MIPS_Emulator.Instructions.JType {
	public abstract class JTypeInstruction : Instruction {
		protected uint immediate;
		
		protected abstract string name { get; }
		
		protected JTypeInstruction(uint immediate) {
			this.immediate = immediate;
		}
		
		public override string ToString() {
			return $"{name} {immediate}";
		}

		public abstract void execute(ref uint pc, ref MemoryUnit mem, ref Registers reg);
	}
}