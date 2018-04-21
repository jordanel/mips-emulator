namespace MIPS_Emulator.Instructions.RType {
	public abstract class RTypeInstruction : Instruction {
		protected abstract string Name { get; }
		protected readonly uint d, s, t, shamt;

		protected RTypeInstruction(uint d, uint s, uint t) {
			this.d = d;
			this.s = s;
			this.t = t;
			this.shamt = 0;
		}
		
		//TODO: Add shamt to shift instructions
		protected RTypeInstruction(uint d, uint s, uint t, uint shamt) {
			this.d = d;
			this.s = s;
			this.t = t;
			this.shamt = shamt;
		}

		public override string ToString() {
			return $"{Name} {Registers.RegisterToName(d)} {Registers.RegisterToName(s)} {Registers.RegisterToName(t)}";
		}

		public abstract void execute(ref uint pc, MemoryUnit mem, Registers reg);
	}
}