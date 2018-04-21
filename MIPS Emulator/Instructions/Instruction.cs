namespace MIPS_Emulator.Instructions {
	public interface Instruction {
		
		void execute(ref uint pc, ref MemoryUnit mem, ref Registers reg);
		
	}
}