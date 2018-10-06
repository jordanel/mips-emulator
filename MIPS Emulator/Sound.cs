using System.Runtime.Remoting.Messaging;

namespace MIPS_Emulator {
	public class Sound : MemoryUnit {
		public uint Size => 4;
		public uint WordSize => 4;
		
		public uint this[uint index] {
			get => SoundModule.generator.Period;
			set {
				if (SoundModule.generator == null) return;
				if (value == 0) {
					SoundModule.generator.Amplitude = 0;
				}
				else {
					SoundModule.generator.Amplitude = 4096;
					SoundModule.generator.Period = value;
				}
			}
		}

		public Sound(uint size, uint wordSize = 4) {
			SoundModule.generator.Amplitude = 0;
			SoundModule.waveOut.Play();
		}
	}
}