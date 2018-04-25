namespace MIPS_Emulator.Instructions.IType {
	public abstract class ITypeInstruction : Instruction {
		protected abstract string Name { get; }
		protected readonly uint T, S, Immediate;
		
		protected ITypeInstruction(uint t, uint s, uint immediate) {
			this.T = t;
			this.S = s;
			this.Immediate = immediate;
		}

		public abstract void Execute(ref uint pc, MemoryMapper mem, Registers reg);

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
