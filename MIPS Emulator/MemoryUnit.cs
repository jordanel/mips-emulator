namespace MIPS_Emulator {
	public interface MemoryUnit {
		uint this[uint index] { get; set; }
		uint Size { get; }
	}
}