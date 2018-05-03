namespace MIPS_Emulator.Instructions.RType {
	public class AdduInstruction : RTypeInstruction {
		protected override string Name => "ADDU";
		
		public AdduInstruction(uint d, uint s, uint t) : base(d, s, t) {
			
		}

		public override void Execute(ref uint pc, MemoryMapper mem, Registers reg) {
			reg[D] = reg[S] + reg[T];
			pc += 4;
		}
	}
}