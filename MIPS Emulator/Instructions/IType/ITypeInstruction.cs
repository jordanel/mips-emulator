namespace MIPS_Emulator.Instructions.IType {
	public abstract class ITypeInstruction : Instruction {
		protected abstract string Name { get; }
		protected readonly uint Immediate;
		protected readonly uint S, T;
		
		protected ITypeInstruction(uint t, uint s, uint immediate) {
			this.Immediate = immediate;
			this.S = s;
			this.T = t;
		}

		public abstract void Execute(ref uint pc, MemoryUnit mem, Registers reg);

		protected static uint SignExtend(uint immediate) {
			uint sign = (immediate >> 15) & 0b1;
			immediate = sign == 0 ? immediate : (immediate | 0xFFFF0000);
			return immediate;
		}
		
		public override string ToString() {
			return $"{Name} {Registers.RegisterToName(T)}, {Registers.RegisterToName(S)}, 0x{Immediate:X4}";
		}
	}
}
