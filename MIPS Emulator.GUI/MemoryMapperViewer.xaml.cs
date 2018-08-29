using System.Collections.Generic;
using System.Windows.Controls;

namespace MIPS_Emulator.GUI {
	public partial class MemoryMapperViewer : DebuggerView {
		private MemoryMapper mapper;
		private List<(uint startAddr, uint endAddr, uint wordSize, string name)> mappingInfo;
		private Dictionary<uint, int> addressListBoxMap;
		
		public MemoryMapperViewer(MemoryMapper mapper) {
			InitializeComponent();
			this.mapper = mapper;
			mappingInfo = mapper.GetMappingInfo();

			addressListBoxMap = new Dictionary<uint, int>();
			PopulateList();
			mapper.ValueSet += OnValueChanged;
		}
		
		private void PopulateList() {
			foreach (var unitInfo in mappingInfo) {
				for (uint index = unitInfo.startAddr; index < unitInfo.endAddr; index += unitInfo.wordSize) {
					BuildListItem($"0x{index:X8}: {mapper[index]}");
					addressListBoxMap[index] = memoryList.Items.Count - 1;
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
