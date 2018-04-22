﻿
using System;

namespace MIPS_Emulator.Instructions.RType {
	public class JrInstruction : RTypeInstruction {
		protected override string Name => "JR";
		
		public JrInstruction(uint d, uint s, uint t) : base(d, s, t) {
			
		}

		public override void execute(ref uint pc, MemoryUnit mem, Registers reg) {
			pc += 4;
			Console.Error.WriteLine("NOT IMPLEMENTED!");
		}
	}
}