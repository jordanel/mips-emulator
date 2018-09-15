using System;
using System.Text.RegularExpressions;

namespace MIPS_Emulator {
	public class MappedMemoryUnit {
		public MemoryUnit MemUnit { get; }
		public uint StartAddr { get; } 
		public uint EndAddr { get; }
		public uint Size {
			get { return MemUnit.Size; }
		}
		public uint WordSize {
			get { return MemUnit.WordSize; }
		}
		public string Name { get; }
		private readonly Regex bitmaskFormat = new Regex("^(0|1)+x*$");

		public MappedMemoryUnit(MemoryUnit memUnit, uint startAddr, uint endAddr, string name = null) {
			this.MemUnit = memUnit;
			this.StartAddr = startAddr;
			this.EndAddr = endAddr;
			this.Name = name ?? memUnit?.GetType().Name;
		}

		public MappedMemoryUnit(MemoryUnit memUnit, uint startAddr, string name = null) 
			: this(memUnit, startAddr, startAddr + memUnit.Size - 1, name) {
		}
		
		public MappedMemoryUnit(MemoryUnit memUnit, string bitmask, string name = null) {
			string cleanedBitmask = bitmask.Trim().ToLower().Replace("_", "");
			if (!bitmaskFormat.IsMatch(cleanedBitmask)) {
				throw new ArgumentException($"Invalid bitmask: \"{bitmask}\", must match regex /{bitmaskFormat.ToString()}/");
			}

			this.MemUnit = memUnit;
			this.StartAddr = Convert.ToUInt32(cleanedBitmask.Replace("x", "0"), 2);
			this.EndAddr = Convert.ToUInt32(cleanedBitmask.Replace("x", "1"), 2);
			this.Name = name ?? memUnit?.GetType().Name;
		}
		
		public uint this[uint index] {
			get => MemUnit[index];
			set => MemUnit[index] = value;
		}
	}
}