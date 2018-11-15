using NAudio.Wave;

namespace MIPS_Emulator {
	public static class SoundModule {
		public static WaveOut waveOut;
		public static SoundWaveGenerator generator;

		static SoundModule() {
			waveOut = new WaveOut {DesiredLatency = 150};
			generator = new SoundWaveGenerator(44100) {Period = 100_000, Amplitude = 0};
			waveOut.Init(generator);
		}
	}
}