namespace MIPS_Emulator.Instructions.IType {
	public class AddiInstruction : ITypeInstruction {
		protected override string Name => "ADDI";
		
		public AddiInstruction(uint t, uint s, uint immediate) : base(t, s, immediate) {
			
		}
		
		public override void Execute(ref uint pc, MemoryMapper mem, Registers reg) {
			reg[T] = reg[S] + SignExtend(Immediate);
			pc += 4;
		}
	}
}
