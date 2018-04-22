
using System;

namespace MIPS_Emulator.Instructions.IType {
	public class BneInstruction : ITypeInstruction{
		protected override string Name => "BNE";
		
		public BneInstruction(uint s, uint t, uint offset) : base(t, s, offset) {
			
		}

		public override void Execute(ref uint pc, MemoryUnit mem, Registers reg) {
			if (reg[S] != reg[T]) {
				pc += (Immediate << 2);
			} else {
				pc += 4;
			}
		}
		
		public override string ToString() {
			return $"{Name} {Registers.RegisterToName(S)}, {Registers.RegisterToName(T)}, 0x{Immediate:X4}";
		}
	}
}
