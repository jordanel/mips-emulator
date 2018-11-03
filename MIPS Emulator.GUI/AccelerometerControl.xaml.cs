using System.Windows;

namespace MIPS_Emulator.GUI {
	public partial class AccelerometerControl : DebuggerView {
		private MappedMemoryUnit accelerometerX;
		private MappedMemoryUnit accelerometerY;
		private const uint defaultValue = 255;
		
		public AccelerometerControl(MappedMemoryUnit accelerometerX, MappedMemoryUnit accelerometerY) {
			InitializeComponent();
			
			this.accelerometerX = accelerometerX;
			this.accelerometerY = accelerometerY;

			if (accelerometerX == null) { XPanel.Visibility = Visibility.Collapsed; }
			if (accelerometerY == null) { YPanel.Visibility = Visibility.Collapsed; }
		}

		private void XSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
			if (accelerometerX != null) {
				((AccelerometerX) accelerometerX.MemUnit).xValue = (uint) XSlider.Value;
			}
		}

		private void YSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
			if (accelerometerY != null) {
				((AccelerometerY) accelerometerY.MemUnit).yValue = (uint) YSlider.Value;
			}
		}

		private void ResetButton_OnClick(object sender, RoutedEventArgs e) {
			XSlider.Value = defaultValue;
			YSlider.Value = defaultValue;
		}

		public void RefreshDisplay() { }
	}
}
