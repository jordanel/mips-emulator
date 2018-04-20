using System;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using MIPS_Emulator.Instructions;
using MIPS_Emulator.Instructions.IType;
using MIPS_Emulator.Instructions.RType;

namespace MIPS_Emulator {
	public class InstructionFactory {

		
		// op codes
		public const uint LW = 0b100011;
		public const uint SW = 0b101011;
		public const uint ADDI = 0b001000;
		public const uint ADDIU = 0b001001;
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
				switch (func) {
					case ADD:
						return new AddInstruction(rd, rs, rt);
					case SUB:
						return new SubInstruction(rd, rs, rt);
					case AND:
						return new AndInstruction(rd, rs, rt);
					case OR:
						return new OrInstruction(rd, rs, rt);
					case XOR:
						return new XorInstruction(rd, rs, rt);
					case NOR:
						return new NorInstruction(rd, rs, rt);
					case SLT:
						return new SltInstruction(rd, rs, rt);
					case SLTU:
						return new SltuInstruction(rd, rs, rt);
					case SLL:
						return new SllInstruction(rd, rs, rt);
					case SLLV:
						return new SllvInstruction(rd, rs, rt);
					case SRL:
						return new SrlInstruction(rd, rs, rt);
					case SRA:
						return new SraInstruction(rd, rs, rt);
					case JR:
						return new JrInstruction(rd, rs, rt);
				}
			} else {
				switch (op) {
					case LW:
						return new LwInstruction(immediate, rs, rt);
					case SW:
						return new SwInstruction(immediate, rs, rt);
					case ADDI:
						return new AddiInstruction(immediate, rs, rt);
					case ADDIU:
						return new AddiuInstruction(immediate, rs, rt);
					case SLTI:
						return new SltiInstruction(immediate, rs, rt);
					case SLTIU:
						return new SltiuInstruction(immediate, rs, rt);
					case ORI:
						return new OriInstruction(immediate, rs, rt);
					case LUI:
						return new LuiInstruction(immediate, rs, rt);
					case BEQ:
						return new BeqInstruction(immediate, rs, rt);
					case BNE:
						return new BneInstruction(immediate, rs, rt);
					case J:
						break;
					case JAL:
						break;
				}
			}

			throw new UnknownInstructionException(instruction);

		}

		public class UnknownInstructionException : NotImplementedException {
			public UnknownInstructionException(uint instruction) 
				: base ($"Unknown instruction: {instruction}") {
				
			}
		}
	}
}