using System.Windows;

namespace MIPS_Emulator.GUI {
	public partial class AccelerometerControl : DebuggerView {
		private AccelerometerX accelerometerX;
		private AccelerometerY accelerometerY;
		private const uint defaultValue = 255;
		
		public AccelerometerControl(AccelerometerX accelerometerX, AccelerometerY accelerometerY) {
			InitializeComponent();
			
			this.accelerometerX = accelerometerX;
			this.accelerometerY = accelerometerY;

			if (accelerometerX == null) {
				XPanel.Visibility = Visibility.Collapsed;
			} else {
				accelerometerX.XValue = (uint) XSlider.Value;
			}

			if (accelerometerY == null) {
				YPanel.Visibility = Visibility.Collapsed;
			} else {
				accelerometerY.YValue = (uint) YSlider.Value;
			}
		}

		private void XSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
			if (accelerometerX != null) {
				accelerometerX.XValue = (uint) XSlider.Value;
			}
		}

		private void YSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
			if (accelerometerY != null) {
				accelerometerY.YValue = (uint) YSlider.Value;
			}
		}

		private void ResetButton_OnClick(object sender, RoutedEventArgs e) {
			XSlider.Value = defaultValue;
			YSlider.Value = defaultValue;
		}

		public void RefreshDisplay() { }
	}
}
