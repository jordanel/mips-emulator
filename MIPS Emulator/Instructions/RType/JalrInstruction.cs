using System;

namespace MIPS_Emulator.Instructions.RType {
	public class JalrInstruction : RTypeInstruction {
		protected override string Name => "JALR";

		public JalrInstruction(uint s) : base(0, s, 0)
		{

		}

		public override void Execute(ref uint pc, MemoryMapper mem, Registers reg)
		{
			reg[31] = pc + 4;
			pc = reg[S];
		}

		public override string ToString()
		{
			return $"{Name} {Registers.RegisterToName(S)}";
		}
	}
}