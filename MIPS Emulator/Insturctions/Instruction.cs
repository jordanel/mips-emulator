namespace MIPS_Emulator.Insturctions {
	public interface Instruction {
		
		void execute(ref uint pc, ref MemoryUnit mem, ref Registers reg);
		
	}
}