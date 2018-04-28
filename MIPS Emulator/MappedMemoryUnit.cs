namespace MIPS_Emulator {
	public class MappedMemoryUnit {
		public MemoryUnit MemUnit { get; }
		public uint StartAddr { get; } 
		public uint EndAddr { get; }

		public MappedMemoryUnit(MemoryUnit memUnit, uint startAddr, uint endAddr) {
			this.MemUnit = memUnit;
			this.StartAddr = startAddr;
			this.EndAddr = endAddr;
		}

		public MappedMemoryUnit(MemoryUnit memUnit, uint startAddr) 
			: this(memUnit, startAddr, startAddr + memUnit.Size) {
		}
		
		//TODO: Add constructor with parameters: MemoryUnit, bitmask - size autocalculated

		public uint this[uint index] {
			get => MemUnit[index];
			set => MemUnit[index] = value;
		}

	}
}