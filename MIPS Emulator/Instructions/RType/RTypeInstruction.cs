namespace MIPS_Emulator.Instructions.RType {
	public abstract class RTypeInstruction : Instruction {
		protected abstract string Name { get; }
		protected readonly uint D, S, T;

		protected RTypeInstruction(uint d, uint s, uint t) {
			this.D = d;
			this.S = s;
			this.T = t;
		}

		public abstract void Execute(ref uint pc, MemoryUnit mem, Registers reg);
		
		public override string ToString() {
			return $"{Name} {Registers.RegisterToName(D)}, {Registers.RegisterToName(S)}, {Registers.RegisterToName(T)}";
		}
	}
}