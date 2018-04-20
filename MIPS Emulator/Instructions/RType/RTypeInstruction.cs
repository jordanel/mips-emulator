namespace MIPS_Emulator.Instructions.RType {
	public abstract class RTypeInstruction : Instruction {

		private uint func;
		private readonly uint d, s, t;

		protected abstract string name { get; }

		protected RTypeInstruction(uint func, uint d, uint s, uint t) {
			this.func = func;
			this.d = d;
			this.s = s;
			this.t = t;
		}

		public override string ToString() {
			return $"{name} {Registers.RegisterToName(d)} {Registers.RegisterToName(s)} {Registers.RegisterToName(t)}";
		}

		public abstract void execute(ref uint pc, ref MemoryUnit mem, ref Registers reg);
	}

}