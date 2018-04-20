namespace MIPS_Emulator.Instructions.RType {
	public abstract class RTypeInstruction : Instruction {

		protected uint func;
		protected readonly uint d, s, t, shamt;

		protected abstract string name { get; }

		protected RTypeInstruction(uint func, uint d, uint s, uint t) {
			this.func = func;
			this.d = d;
			this.s = s;
			this.t = t;
			this.shamt = 0;
		}
		
		//TODO: Add shamt to shift instructions
		protected RTypeInstruction(uint func, uint d, uint s, uint t, uint shamt) {
			this.func = func;
			this.d = d;
			this.s = s;
			this.t = t;
			this.shamt = shamt;
		}

		public override string ToString() {
			return $"{name} {Registers.RegisterToName(d)} {Registers.RegisterToName(s)} {Registers.RegisterToName(t)}";
		}

		public abstract void execute(ref uint pc, ref MemoryUnit mem, ref Registers reg);
	}

}