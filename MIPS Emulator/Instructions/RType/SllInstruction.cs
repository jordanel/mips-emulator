
using System;

namespace MIPS_Emulator.Instructions.RType {
	public class SllInstruction : RTypeInstruction {
		protected override string Name => "SLL";
		private readonly uint shamt;

		public SllInstruction(uint d, uint t, uint shamt) : base(d, shamt, t) {
			this.shamt = shamt;
		}

		public override void Execute(ref uint pc, MemoryMapper mem, Registers reg) {
			reg[D] = reg[T] << (int) shamt;
			pc += 4;
		}
		
		public override string ToString() {
			return $"{Name} {Registers.RegisterToName(D)}, {Registers.RegisterToName(T)}, {shamt}";
		}
	}
}