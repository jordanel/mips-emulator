using System.Windows;

namespace MIPS_Emulator.GUI {
	public partial class AccelerometerControl : DebuggerView {
		private MappedMemoryUnit accelerometer;
		private MemoryMapper mapper;
		
		public AccelerometerControl(MappedMemoryUnit accelerometer, MemoryMapper mapper) {
			InitializeComponent();
			this.accelerometer = accelerometer;
			this.mapper = mapper;
		}

		private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
			((Accelerometer) accelerometer.MemUnit).Coordinates = ((uint) XSlider.Value, (uint) YSlider.Value);
			mapper[accelerometer.StartAddr] = (uint) XSlider.Value;
			mapper[accelerometer.StartAddr + accelerometer.WordSize] = (uint) YSlider.Value;
		}

		public void RefreshDisplay() { }
	}
}
