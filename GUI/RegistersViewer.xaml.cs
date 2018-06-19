using MIPS_Emulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GUI {
	/// <summary>
	/// Interaction logic for RegistersViewer.xaml
	/// </summary>
	public partial class RegistersViewer : Window, DebuggerView {

		private Mips mips;

		public RegistersViewer(Mips mips) {
			InitializeComponent();
			this.mips = mips;

			for (int i = 0; i < 32; i++) {
				var item = new ListBoxItem();
				item.Content = $"{Registers.RegisterToName(i)}:\t{mips.Reg[(uint) i]}";
				registerList.Items.Add(item);
			}
		}

		public void Tick() {
			for (int i = 0; i < 32; i++) {
				ListBoxItem item = (ListBoxItem) registerList.Items[i];
				item.Content = $"{Registers.RegisterToName(i)}:\t{mips.Reg[(uint) i]}";
			}
		}
	}
}
