namespace MIPS_Emulator.Instructions {
	public interface Instruction {
		
		void execute(ref uint pc, MemoryUnit mem, Registers reg);
		
	}
}