namespace MIPS_Emulator.Instructions {
	public interface Instruction {
		
		void Execute(ref uint pc, MemoryUnit mem, Registers reg);
		
	}
}