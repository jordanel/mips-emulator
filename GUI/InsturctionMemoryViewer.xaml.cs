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
    /// Interaction logic for InstructionMemoryViewer.xaml
    /// </summary>
    public partial class InstructionMemoryViewer : DebuggerView {
		private Mips mips;
	    private const int wordSize = 4;

        public InstructionMemoryViewer(Mips mips) {
            InitializeComponent();
			this.mips = mips;
	        
			for (uint i = 0; i < mips.InstrMem.Size; i += 4) {
				ListBoxItem item = new ListBoxItem();
				item.Content = $"0x{i:X8}: {mips.InstrMem.GetInstruction(i)}";
				instructionsList.Items.Add(item);
			}
			RefreshDisplay();
		}

		public void RefreshDisplay() {
			instructionsList.SelectedIndex = (int)(mips.Pc & 0xffff) / wordSize;
			instructionsList.ScrollIntoView(instructionsList.Items[(int)(mips.Pc & 0xffff) / wordSize]);						
		}
	}
}
