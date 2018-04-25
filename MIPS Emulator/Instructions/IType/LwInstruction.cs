using System;

namespace MIPS_Emulator.Instructions.IType {
	public class LwInstruction : ITypeInstruction{
		protected override string Name => "LW";
		
		public LwInstruction(uint t, uint s, uint offset) : base(t, s, offset) {
			
		}
		
		public override void Execute(ref uint pc, MemoryMapper mem, Registers reg) {
			reg[T] = mem[reg[S] + Immediate];
			pc += 4;
		}

		public override string ToString() {
			return $"{Name} {Registers.RegisterToName(T)}, 0x{Immediate:X4}({Registers.RegisterToName(S)})";
		}
	}
}