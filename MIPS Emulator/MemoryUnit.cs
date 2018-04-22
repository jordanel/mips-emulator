namespace MIPS_Emulator {
	public class MemoryUnit {
		private readonly uint[] dataMem;

		public uint this[uint i] {
			get => dataMem[i >> 2];
			set => dataMem[i >> 2] = value;
		}
		
		public MemoryUnit(uint size) {
			dataMem = new uint[size];
		}
		
	}
}