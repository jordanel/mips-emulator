
using System;

namespace MIPS_Emulator.Instructions.IType {
	public class LuiInstruction : ITypeInstruction{
		protected override string Name => "LUI";
		
		public LuiInstruction(uint t, uint immediate) : base(t, 0, immediate) {
			
		}
		
		public override void Execute(ref uint pc, MemoryMapper mem, Registers reg) {
			reg[T] = Immediate << 16;
			pc += 4;
		}
		
		public override string ToString() {
			return $"{Name} {Registers.RegisterToName(T)}, 0x{Immediate:X4}";
		}
	}
}
