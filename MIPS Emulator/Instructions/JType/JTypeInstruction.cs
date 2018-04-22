namespace MIPS_Emulator.Instructions.JType {
	public abstract class JTypeInstruction : Instruction {
		protected abstract string Name { get; }
		protected readonly uint Target;
		
		protected JTypeInstruction(uint target) {
			this.Target = target;
		}

		public abstract void Execute(ref uint pc, MemoryUnit mem, Registers reg);
		
		public override string ToString() {
			return $"{Name} 0x{Target:X8}";
		}
	}
}