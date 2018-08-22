namespace MIPS_Emulator {
	public class Keyboard : MemoryUnit {
		public uint Size => 4;
		public uint WordSize => 4;
		private uint keyCode;
		
		public uint this[uint index] {
			get { return keyCode; }
			set { }
		}

		public void SetKeyCode(uint value) {
			keyCode = value;
		}
	}
}