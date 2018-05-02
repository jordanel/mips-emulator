namespace MIPS_Emulator {
	public class Mips {
		public uint Pc { get; }
		public InstructionMemory InstrMem { get; }
		public MemoryMapper Memory { get; }
		public Registers Reg { get; }

		public Mips(uint pc, InstructionMemory instrMem, MemoryMapper memory, Registers reg = null) {
			this.Pc = pc;
			this.InstrMem = instrMem;
			this.Memory = memory;
			this.Reg = reg ?? new Registers();
		}

		public void ExecuteNext() {
			
		}
	}
}