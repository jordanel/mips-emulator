using System;

namespace MIPS_Emulator.Instructions.IType {
	public class LwInstruction : ITypeInstruction{
		protected override string Name => "LW";
		
		public LwInstruction(int immediate, uint s, uint t) : base(t, s, immediate) {
			
		}
		
		public override void execute(ref uint pc, MemoryUnit mem, Registers reg) {
			pc += 4;
			Console.Error.WriteLine("NOT IMPLEMENTED!");
		}
	}
}