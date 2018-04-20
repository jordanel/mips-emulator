namespace MIPS_Emulator.Insturctions.RType {
	public class AddInstruction : RTypeInstruction {

		public AddInstruction(uint d, uint s, uint t) : base(InstructionFactory.ADD, d, s, t) {
			
		}

		protected override string name => "ADD";

		public override void execute(ref uint pc, ref MemoryUnit mem, ref Registers reg) {
			pc += 4;
		}
	}
}