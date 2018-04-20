using System;

namespace MIPS_Emulator {
	public class InstructionFactory {
		private enum Opcode {
			R = 0b000000,
			LW = 0b100011,
			SW = 0b101011,
			ADDI = 0b001000,
			ADDIU = 0b001001,
			SLTI = 0b001010,
			SLTIU = 0b001011,
			ORI = 0b001101,
			LUI = 0b001111,
			BEQ = 0b000100,
			BNE = 0b000101,
			J = 0b000010,
			JAL = 0b000011
		}

		private enum Func {
			ADD = 0b100000,
			SUB = 0b100010,
			AND = 0b100100,
			OR = 0b100101,
			XOR = 0b100110,
			NOR = 0b100111,
			SLT = 0b101010,
			SLTU = 0b101011,
			SLL = 0b000000,
			SLLV = 0b000100,
			SRL = 0b000010,
			SRA = 0b000011,
			JR = 0b001000
		}
		
		public Instruction GetInstruction(uint instruction) {
			Instruction instr;
			uint opcode = instruction >> 26;
			if ((Opcode) opcode == Opcode.R) {
				instr = CreateRType(opcode);
			} else {
				instr = CreateIType(opcode);
			}
			
			return instr;
		}

		public Instruction CreateRType(uint func) {
			switch ((Func) func) {
				case Func.ADD:
					break;
				case Func.SUB:
					break;
				case Func.AND:
					break;
				case Func.OR:
					break;
				case Func.XOR:
					break;
				case Func.NOR:
					break;
				case Func.SLT:
					break;
				case Func.SLTU:
					break;
				case Func.SLL:
					break;
				case Func.SLLV:
					break;
				case Func.SRL:
					break;
				case Func.SRA:
					break;
				case Func.JR:
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(func), func, null);
			}
			
			return null;
		}

		public Instruction CreateIType(uint opcode) {
			switch ((Opcode) opcode) {
				case Opcode.ADDI:
					break;
				case Opcode.LW:
					break;
				case Opcode.SW:
					break;
				case Opcode.ADDIU:
					break;
				case Opcode.SLTI:
					break;
				case Opcode.SLTIU:
					break;
				case Opcode.ORI:
					break;
				case Opcode.LUI:
					break;
				case Opcode.BEQ:
					break;
				case Opcode.BNE:
					break;
				case Opcode.J:
					break;
				case Opcode.JAL:
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(opcode), opcode, null);
			}
			
			return null;
		}
	}
}