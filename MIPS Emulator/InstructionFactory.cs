using System;
using MIPS_Emulator.Instructions;
using MIPS_Emulator.Instructions.IType;
using MIPS_Emulator.Instructions.JType;
using MIPS_Emulator.Instructions.RType;

namespace MIPS_Emulator {
	public class InstructionFactory {
		private enum Opcode : uint {
			LW    = 0b100011,
			SW    = 0b101011,
			ADDI  = 0b001000,
			ADDIU = 0b001001,
			SLTI  = 0b001010,
			SLTIU = 0b001011,
			ORI   = 0b001101,
			LUI   = 0b001111,
			BEQ   = 0b000100,
			BNE   = 0b000101,
			J     = 0b000010,
			JAL   = 0b000011
		}

		private enum Func : uint {
			ADD  = 0b100000,
			SUB  = 0b100010,
			AND  = 0b100100,
			OR   = 0b100101,
			XOR  = 0b100110,
			NOR  = 0b100111,
			SLT  = 0b101010,
			SLTU = 0b101011,
			SLL  = 0b000000,
			SLLV = 0b000100,
			SRL  = 0b000010,
			SRA  = 0b000011,
			JR   = 0b001000
		}
		
		private const uint SIX_MASK = 0b111111;
		private const uint FIVE_MASK = 0b11111;
		private const uint SIXTEEN_MASK = 0b1111111111111111;
		private const uint TWENTY_SIX_MASK = 0b11111111111111111111111111;
		
		public Instruction CreateInstruction(uint instruction) {

			uint func = instruction & SIX_MASK;
			uint shamt = (instruction >> 6) & FIVE_MASK;
			uint rd = (instruction >> 11) & FIVE_MASK;
			uint rt = (instruction >> 16) & FIVE_MASK;
			uint rs = (instruction >> 21) & FIVE_MASK;
			uint op = (instruction >> 26) & SIX_MASK;
			uint immediate = instruction & SIXTEEN_MASK;
			uint address = instruction & TWENTY_SIX_MASK;

			if (op == 0) {
				switch ((Func) func) {
					case Func.ADD:
						return new AddInstruction(rd, rs, rt);
					case Func.SUB:
						return new SubInstruction(rd, rs, rt);
					case Func.AND:
						return new AndInstruction(rd, rs, rt);
					case Func.OR:
						return new OrInstruction(rd, rs, rt);
					case Func.XOR:
						return new XorInstruction(rd, rs, rt);
					case Func.NOR:
						return new NorInstruction(rd, rs, rt);
					case Func.SLT:
						return new SltInstruction(rd, rs, rt);
					case Func.SLTU:
						return new SltuInstruction(rd, rs, rt);
					case Func.SLL:
						return new SllInstruction(rd, rt, shamt);
					case Func.SLLV:
						return new SllvInstruction(rd, rt, rs);
					case Func.SRL:
						return new SrlInstruction(rd, rt, shamt);
					case Func.SRA:
						return new SraInstruction(rd, rt, shamt);
					case Func.JR:
						return new JrInstruction(rs);
				}
			} else {
				switch ((Opcode) op) {
					case Opcode.LW:
						return new LwInstruction(rt, rs, immediate);
					case Opcode.SW:
						return new SwInstruction(rt, rs, immediate);
					case Opcode.ADDI:
						return new AddiInstruction(rt, rs, immediate);
					case Opcode.ADDIU:
						return new AddiuInstruction(rt, rs, immediate);
					case Opcode.SLTI:
						return new SltiInstruction(rt, rs, immediate);
					case Opcode.SLTIU:
						return new SltiuInstruction(rt, rs, immediate);
					case Opcode.ORI:
						return new OriInstruction(rt, rs, immediate);
					case Opcode.LUI:
						return new LuiInstruction(rt, immediate);
					case Opcode.BEQ:
						return new BeqInstruction(rs, rt, immediate);
					case Opcode.BNE:
						return new BneInstruction(rs, rt, immediate);
					case Opcode.J:
						return new JInstruction(address);
					case Opcode.JAL:
						return new JalInstruction(address);
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