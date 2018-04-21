namespace MIPS_Emulator {
	public class MemoryUnit {
		private readonly uint[] dataMem;

		public uint this[int i] {
			get => dataMem[i];
			set => dataMem[i] = value;
		}
		
		public MemoryUnit(uint size) {
			dataMem = new uint[size];
		}
		
	}
}