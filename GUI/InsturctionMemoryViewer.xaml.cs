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
    /// Interaction logic for InsturctionMemoryViewer.xaml
    /// </summary>
    public partial class InsturctionMemoryViewer : Window, DebuggerView {

		private Mips mips; 

        public InsturctionMemoryViewer(Mips mips) {
            InitializeComponent();
			this.mips = mips;
			for (uint i = 0; i < mips.InstrMem.Size; i += 4) {
				var item = new ListBoxItem();
				item.Content = $"0x{i:X8}: {mips.InstrMem.GetInstruction(i)}";
				instructionsList.Items.Add(item);
			}
			Tick();
		}

		public void Tick() {
			instructionsList.SelectedIndex = (int) (mips.Pc & 0xffff) / 4;
			instructionsList.ScrollIntoView(instructionsList.Items[(int)(mips.Pc & 0xffff) / 4]);
		}
	}
}
