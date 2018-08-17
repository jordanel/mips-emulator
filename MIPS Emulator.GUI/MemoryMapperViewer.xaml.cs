using System.Collections.Generic;
using System.Windows.Controls;

namespace MIPS_Emulator.GUI {
	public partial class MemoryMapperViewer : DebuggerView {
		private MemoryMapper mapper;
		private Dictionary<uint, int> addressListBoxMap;
		private const int wordSize = 4;
		
		public MemoryMapperViewer(MemoryMapper mapper) {
			InitializeComponent();
			this.mapper = mapper;

			addressListBoxMap = new Dictionary<uint, int>();
			PopulateList();
			mapper.ValueSet += OnValueChanged;
		}
		
		private void PopulateList() {
			bool isFirstBlank = true;
			for (uint index = mapper.StartAddr; index < (mapper.StartAddr + mapper.Size); index += wordSize) {
				try {
					BuildListItem($"0x{index:X8}: {mapper[index]}");
					addressListBoxMap[index] = memoryList.Items.Count - 1;
					isFirstBlank = true;
				} catch (MemoryMapper.UnmappedAddressException e) {
					if (isFirstBlank) {
						BuildListItem("...");
						isFirstBlank = false;
					}
				}
			}
		}

		private void BuildListItem(object content) {
			ListBoxItem item = new ListBoxItem {Content = content};
			memoryList.Items.Add(item);
		}

		public void RefreshDisplay() {}
		
		private void OnValueChanged(object sender, ValueSetEventArgs e) {
			if (!Dispatcher.CheckAccess()) {
				Dispatcher.Invoke(delegate {OnValueChanged(sender, e);});
				return;
			}
			
			int listIndex = addressListBoxMap[e.Address];
			if (memoryList.Items[listIndex] is ListBoxItem item) item.Content = $"0x{e.Address:X8}: {mapper[e.Address]} UPDATED";
		}
	}
}
