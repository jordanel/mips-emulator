namespace MIPS_Emulator.Instructions.IType {
	public abstract class ITypeInstruction : Instruction {
		protected abstract string Name { get; }
		protected readonly int Immediate;
		protected readonly uint S, T;
		
		protected ITypeInstruction(uint t, uint s, int immediate) {
			this.Immediate = immediate;
			this.S = s;
			this.T = t;
		}
		
		public override string ToString() {
			return $"{Name} {Registers.RegisterToName(T)} {Immediate}({Registers.RegisterToName(S)})";
		}

		public abstract void execute(ref uint pc, MemoryUnit mem, Registers reg);
	}
}
