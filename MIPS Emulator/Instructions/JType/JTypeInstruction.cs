namespace MIPS_Emulator.Instructions.JType {
	public abstract class JTypeInstruction : Instruction {
		protected abstract string Name { get; }
		protected readonly uint Immediate;
		
		protected JTypeInstruction(uint immediate) {
			this.Immediate = immediate;
		}
		
		public override string ToString() {
			return $"{Name} {Immediate}";
		}

		public abstract void Execute(ref uint pc, MemoryUnit mem, Registers reg);
	}
}