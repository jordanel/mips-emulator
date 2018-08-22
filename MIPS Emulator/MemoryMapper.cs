using System;
using System.Collections.Generic;
using System.Linq;

namespace MIPS_Emulator {
	public class MemoryMapper : MemoryUnit {
		private readonly List<MappedMemoryUnit> memUnits;
		public uint Size => memUnits[memUnits.Count - 1].EndAddr - memUnits[0].StartAddr;
		public uint WordSize => 1;
		public uint StartAddr => memUnits[0].StartAddr;

		public event EventHandler<ValueSetEventArgs> ValueSet;

		public MemoryMapper(List<MappedMemoryUnit> memUnits) {
			this.memUnits = memUnits;
			this.memUnits.Sort((x, y) => x.StartAddr.CompareTo(y.StartAddr));
		}
		
		[Obsolete]
		public MemoryMapper(uint size) {
			uint[] data = new uint[size];
			DataMemory dataMem = new DataMemory(data);
			MappedMemoryUnit mappedMem = new MappedMemoryUnit(dataMem, 0);
			memUnits = new List<MappedMemoryUnit> {mappedMem};
		}
		
		public uint this[uint address] {
			get {
				MappedMemoryUnit unit = FindContainingUnit(address);
				return unit[ResolveAddress(address, unit)];
			}
			set {
				MappedMemoryUnit m = FindContainingUnit(address);
				m[ResolveAddress(address, m)] = value;
				
				ValueSetEventArgs args = new ValueSetEventArgs { Address = address };
				OnValueSet(args);
			}
		}

		private MappedMemoryUnit FindContainingUnit(uint addr) {		
			foreach (MappedMemoryUnit m in memUnits) {
				if (m.StartAddr <= addr && addr <= m.EndAddr) {
					return m;
				}
			}
			throw new UnmappedAddressException(addr);
		}

		private uint ResolveAddress(uint addr, MappedMemoryUnit memUnit) {
			return addr - memUnit.StartAddr;
		}

		private void OnValueSet(ValueSetEventArgs e) {
			EventHandler<ValueSetEventArgs> handler = ValueSet;
			handler?.Invoke(this, e);
		}
		
		public class UnmappedAddressException : ArgumentOutOfRangeException {
			public UnmappedAddressException(uint address) 
				: base ($"Unable to find memory unit mapped to 0x{address:X8}") {
				
			}
		}
	}
	
	public class ValueSetEventArgs : EventArgs {
		public uint Address { get; set; }
	}
}