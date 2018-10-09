using System;
using NAudio.Wave;

namespace MIPS_Emulator {
	public class SoundWaveGenerator : WaveProvider16 {
		private double phaseAngle;
		
		public enum WaveShape {
			SINE,
			SQUARE,
			SAWTOOTH,
			TRIANGLE
		}
		
		public uint Period { get; set; }
		public short Amplitude { get; set; }
		public WaveShape Shape { get; set; }

		public SoundWaveGenerator(int sampleRate) : base(sampleRate, 1) {
			Shape = WaveShape.SQUARE;
		}
		
		public override int Read(short[] buffer, int offset, int sampleCount) {
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
					case WaveShape.TRIANGLE:
						buffer[offset + index] = (short) (Amplitude * (Math.Abs(phaseAngle / Math.PI - 1) * 2 - 1));
						break;
				}
 				
				phaseAngle += 2 * Math.PI * (100_000_000.0 / Period) / WaveFormat.SampleRate;
				if (phaseAngle > 2 * Math.PI) {
					phaseAngle -= 2 * Math.PI;
				}
			}

			return sampleCount;
		}
	}
}