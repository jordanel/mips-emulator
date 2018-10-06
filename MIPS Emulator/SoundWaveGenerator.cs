using System;
using NAudio.Wave;

namespace MIPS_Emulator {
	public class SoundWaveGenerator : WaveProvider16 {
		private double phaseAngle;
		
		public enum WaveShape {
			SINE,
			SQUARE,
			SAWTOOTH
		}
		
		public uint Period { set; get; }
		public short Amplitude { set; get; }
		public WaveShape Shape { get; set; }

		public SoundWaveGenerator(int sampleRate) : base(sampleRate, 1) {
			Shape = WaveShape.SQUARE;
		}
		
		public override int Read(short[] buffer, int offset, int sampleCount) {
			double frequency = 100_000_000.0 / Period;
			for (int index = 0; index < sampleCount; index++) {
				switch (Shape) {
					case WaveShape.SINE:
						buffer[offset + index] = (short) (Amplitude * Math.Sin(phaseAngle));
						break;
					case WaveShape.SQUARE:
						buffer[offset + index] = (short) (Amplitude * (phaseAngle > Math.PI ? 1 : 0));
						break;
					case WaveShape.SAWTOOTH:
						buffer[offset + index] = (short) (Amplitude * (phaseAngle / Math.PI - 1));
						break;

				}
 				
				phaseAngle += 2 * Math.PI * frequency / WaveFormat.SampleRate;
				if (phaseAngle > 2 * Math.PI) {
					phaseAngle -= 2 * Math.PI;
				}
			}

			return sampleCount;
		}
	}
}