using System;
using System.Windows.Controls;

namespace MIPS_Emulator.GUI {
	public partial class MemoryMapperViewer : DebuggerView {
		private MemoryMapper mapper;
		private const int wordSize = 4;
		
		public MemoryMapperViewer(MemoryMapper mapper) {
			InitializeComponent();
			this.mapper = mapper;
			
			for (uint i = 0; i < mapper.Size; i += 4) {
				try {
					ListBoxItem item = new ListBoxItem();
					item.Content = $"0x{i:X8}: {mapper[i]}";
					memoryList.Items.Add(item);
				} catch (MemoryMapper.UnmappedAddressException e) {
					ListBoxItem item = new ListBoxItem();
					item.Content = "Exception";
					memoryList.Items.Add(item);
				}
			}
		}

		public void RefreshDisplay() {
			//throw new System.NotImplementedException();
		}
	}
}
