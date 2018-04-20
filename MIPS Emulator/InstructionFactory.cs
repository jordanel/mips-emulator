using MIPS_Emulator.Insturctions;
using MIPS_Emulator.Insturctions.RType;

namespace MIPS_Emulator {
	public class InstructionFactory {

		
		// op codes
		public const uint LW = 0b100011;
		public const uint SW = 0b101011;
		public const uint ADDI = 0b001000;
		public const uint ADDIU = 1001;
		public const uint SLTI = 0b001010;
		public const uint SLTIU = 0b001011;
		public const uint ORI = 0b001101;
		public const uint LUI = 0b001111;
		public const uint BEQ = 0b000100;
		public const uint BNE = 0b000101;
		public const uint J = 0b000010;
		public const uint JAL = 0b000011;
		
		// func codes
		public const uint ADD = 0b100000;
		public const uint SUB = 0b100010;
		public const uint AND = 0b100100;
		public const uint OR = 0b100101;
		public const uint XOR = 0b100110;
		public const uint NOR = 0b100111;
		public const uint SLT = 0b101010;
		public const uint SLTU = 0b101011;
		public const uint SLL = 0b000000;
		public const uint SLLV = 0b000100;
		public const uint SRL = 0b000010;
		public const uint SRA = 0b000011;
		public const uint JR = 0b001000;
		
		private const uint SIX_MASK = 0b111111;
		private const uint FIVE_MASK = 0b11111;
		private const uint SIXTEEN_MASK = 0b1111111111111111;
		private const uint TWENTY_SIX_MASK = 0b11111111111111111111111111;
		
		public static Instruction createInstruction(uint instruction) {

			uint func = instruction & SIX_MASK;
			uint shamt = (instruction >> 6) & FIVE_MASK;
			uint rd = (instruction >> 11) & FIVE_MASK;
			uint rt = (instruction >> 16) & FIVE_MASK;
			uint rs = (instruction >> 21) & FIVE_MASK;
			uint op = (instruction >> 26) & SIX_MASK;
			uint immediate = instruction & SIXTEEN_MASK;
			uint address = instruction & TWENTY_SIX_MASK;

			if (op == 0) {
				
			}
			else {
				switch (op) {
					case LW:
						break;
					default:
						break;
				}
			}

			return new AddInstruction(rd, rs, rt);


		}
		
	}
}