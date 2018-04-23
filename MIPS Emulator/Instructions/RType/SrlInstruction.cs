
using System;

namespace MIPS_Emulator.Instructions.RType {
	public class SrlInstruction : RTypeInstruction {
		protected override string Name => "SRL";
		private readonly uint shamt;

		public SrlInstruction(uint d, uint t, uint shamt) : base(d, shamt, t) {
			this.shamt = shamt;
		}

		public override void Execute(ref uint pc, MemoryUnit mem, Registers reg) {
			reg[D] = reg[T] >> (int) shamt;
			pc += 4;
		}
		
		public override string ToString() {
			return $"{Name} {Registers.RegisterToName(D)}, {Registers.RegisterToName(T)}, {shamt}";
		}
	}
}