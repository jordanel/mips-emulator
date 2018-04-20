namespace MIPS_Emulator.Instructions.IType {
	public abstract class ITypeInstruction : Instruction {
		private uint immediate;
		private int s, t;

		protected abstract string name { get; }	
		
		protected ITypeInstruction(uint immediate, int s, int t) {
			this.immediate = immediate;
			this.s = s;
			this.t = t;
		}
		
		public override string ToString() {
			return $"{name} {Registers.RegisterToName(t)} {immediate}({Registers.RegisterToName(t)})";
		}

		public abstract void execute(ref uint pc, ref MemoryUnit mem, ref Registers reg);

	}
}