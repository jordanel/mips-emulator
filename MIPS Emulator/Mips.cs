namespace MIPS_Emulator {
	public class Mips {
		private uint pc;

		public uint Pc => pc;
		public InstructionMemory InstrMem { get; }
		public MemoryMapper Memory { get; }
		public Registers Reg { get; }

		public Mips(uint pc, InstructionMemory instrMem, MemoryMapper memory, Registers reg = null) {
			this.pc = pc;
			this.InstrMem = instrMem;
			this.Memory = memory;
			this.Reg = reg ?? new Registers();
		}

		public void ExecuteNext() {
			InstrMem[pc].Execute(ref pc, Memory, Reg);
		}

		public void ExecuteAll() {
			while (pc < InstrMem.Size) {
				InstrMem[pc].Execute(ref pc, Memory, Reg);
			}
		}
	}
}