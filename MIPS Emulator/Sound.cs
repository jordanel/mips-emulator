using System.Data.OleDb;
using NAudio.Wave;

namespace MIPS_Emulator {
	public class Sound : MemoryUnit {

		private uint wavelength;
		private SoundWaveGenerator generator;
		
		public uint Size => 4;
		public uint WordSize => 4;
		
		public uint this[uint index] {
			get => throw new System.NotImplementedException();
			set {
				if (this.generator == null) return;
				if (value == 0) {
					generator.Amplitude = 0;
				}
				else {
					generator.Amplitude = 4096;
					generator.Period = value;
				}
			}
		}

		public Sound(uint size, uint wordSize = 4) {
			var waveOut = new WaveOut();
			waveOut.DesiredLatency = 50;
			generator = new SoundWaveGenerator(44100) {Period = 100_000, Amplitude = 0};
			waveOut.Init(generator);
			waveOut.Play();
		}
		
	}
}