namespace MIPS_Emulator {
	public interface Instruction {
		uint Execute(uint pc, MemoryUnit mem, Registers reg);
	}
}