using System.Windows.Controls;

namespace MIPS_Emulator.GUI {
    /// <summary>
    /// Interaction logic for InstructionMemoryViewer.xaml
    /// </summary>
    public partial class InstructionMemoryViewer : DebuggerView {
		private Mips mips;

        public InstructionMemoryViewer(Mips mips) {
            InitializeComponent();
			this.mips = mips;
	        
			for (uint i = 0; i < mips.InstrMem.Size; i += mips.InstrMem.WordSize) {
				ListBoxItem item = new ListBoxItem();
				item.Content = $"0x{i:X8}: {mips.InstrMem.GetInstruction(i)}";
				instructionsList.Items.Add(item);
			}
			RefreshDisplay();
		}

		public void RefreshDisplay() {
			instructionsList.SelectedIndex = (int)(mips.Pc & 0xffff) / (int) mips.InstrMem.WordSize;
			instructionsList.ScrollIntoView(instructionsList.Items[(int)(mips.Pc & 0xffff) / (int) mips.InstrMem.WordSize]);						
		}
	}
}
