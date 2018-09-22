using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace MIPS_Emulator.GUI {
    public partial class InstructionMemoryViewer : DebuggerView {
		private Mips mips;

	    public InstructionMemoryViewer(Mips mips) {
	        InitializeComponent();
			this.mips = mips;
	        var instructions = new ObservableCollection<InstructionInfo>();
	        
			for (uint i = 0; i < mips.InstrMem.Size; i += mips.InstrMem.WordSize) {
				instructions.Add(new InstructionInfo() {Address = i, Instruction = mips.InstrMem.GetInstruction(i).ToString()});
			}
	        InstructionsList.ItemsSource = instructions;
	        
			RefreshDisplay();
		}

		public void RefreshDisplay() {
			InstructionsList.SelectedIndex = (int)(mips.Pc & 0xffff) / (int) mips.InstrMem.WordSize;
			InstructionsList.ScrollIntoView(InstructionsList.Items[(int)(mips.Pc & 0xffff) / (int) mips.InstrMem.WordSize]);						
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
	
	public class InstructionInfo {
		public uint Address { get; set; }
		public string Instruction { get; set; }
	}
}
