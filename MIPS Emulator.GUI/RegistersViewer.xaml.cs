using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace MIPS_Emulator.GUI {
	public partial class RegistersViewer : DebuggerView {
		private Registers reg;
		private ObservableCollection<RegisterInfo> regInfo;

		public RegistersViewer(Registers reg) {
			InitializeComponent();
			this.reg = reg;
			regInfo = new ObservableCollection<RegisterInfo>();

			for (int i = 0; i < 32; i++) {
				regInfo.Add(new RegisterInfo() {Name = Registers.RegisterToName(i), Value = reg[(uint) i]});
			}
			RegisterList.ItemsSource = regInfo;
		}

		public void RefreshDisplay() {
			for (int i = 0; i < 32; i++) {
				regInfo[i] = new RegisterInfo() {Name = Registers.RegisterToName(i), Value = reg[(uint) i]};
			}
		}
		
		private void DecimalItem_Click(object sender, RoutedEventArgs e) {
			ChangeBindingStringFormat((MenuItem) sender, "{0}");
		}
		
		private void HexItem_Click(object sender, RoutedEventArgs e) {
			ChangeBindingStringFormat((MenuItem) sender, "0x{0:X8}");
		}
		
		private void ChangeBindingStringFormat(MenuItem sender, string format) {
			MenuItem item = sender;
			ContextMenu contextMenu = (ContextMenu) item.Parent;
			GridViewColumnHeader header = (GridViewColumnHeader) contextMenu.PlacementTarget;
			GridViewColumn column = header.Column;
			string bindingPath = ((Binding) column.DisplayMemberBinding)?.Path.Path;
			column.DisplayMemberBinding = new Binding(bindingPath) {StringFormat = format};
		}
	}

	// TODO: Consider making INotifyPropertyChanged so ObservableCollection picks up property changes
	public class RegisterInfo {
		public string Name { get; set; }
		public uint Value { get; set; }
	}
}
