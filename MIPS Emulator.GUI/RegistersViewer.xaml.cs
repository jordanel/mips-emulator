using System.Windows.Controls;

namespace MIPS_Emulator.GUI {
	/// <summary>
	/// Interaction logic for RegistersViewer.xaml
	/// </summary>
	public partial class RegistersViewer : DebuggerView {
		private Registers reg;

		public RegistersViewer(Registers reg) {
			InitializeComponent();
			this.reg = reg;

			for (int i = 0; i < 32; i++) {
				ListBoxItem item = new ListBoxItem();
				item.Content = $"{Registers.RegisterToName(i)}:\t0x{reg[(uint) i]:X8}";
				registerList.Items.Add(item);
			}
		}

		public void RefreshDisplay() {
			for (int i = 0; i < 32; i++) {
				ListBoxItem item = (ListBoxItem) registerList.Items[i];
				item.Content = $"{Registers.RegisterToName(i)}:\t0x{reg[(uint) i]:X8}";
			}
		}
	}
}
