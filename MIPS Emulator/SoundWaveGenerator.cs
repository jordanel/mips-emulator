using System;
using NAudio.Wave;

namespace MIPS_Emulator {
	public class SoundWaveGenerator : WaveProvider16 {
		private double phaseAngle;
		
		public uint Period { set; get; }
		public short Amplitude { set; get; }

		public SoundWaveGenerator(int sampleRate) : base(sampleRate, 1) {
			
		}
		
		public override int Read(short[] buffer, int offset, int sampleCount) {
			double frequency = 100_000_000.0 / Period;
			for (int index = 0; index < sampleCount; index++) {
				buffer[offset + index] = (short) (Amplitude * Math.Sin(phaseAngle));
				phaseAngle += 2 * Math.PI * frequency / WaveFormat.SampleRate;
				if (phaseAngle > 2 * Math.PI) {
					phaseAngle -= 2 * Math.PI;
				}
			}

			return sampleCount;
		}
	}
}