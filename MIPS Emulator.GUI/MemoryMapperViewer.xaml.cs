using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace MIPS_Emulator.GUI {
	public partial class MemoryMapperViewer : DebuggerView {
		private MemoryMapper mapper;
		private List<MappedMemoryUnit> memUnits;
		
		public MemoryMapperViewer(MemoryMapper mapper) {
			InitializeComponent();
			this.mapper = mapper;
			memUnits = mapper.MemUnits;

			Initialize();
			mapper.ValueSet += OnValueChanged;
		}

		private void Initialize() {
			foreach (MappedMemoryUnit mappedMemoryUnit in memUnits) {
				TabItem tab = new TabItem {Header = mappedMemoryUnit.Name};
				//tab.Content = BuildMemoryDisplay(mappedMemoryUnit);
				MemoryTabs.Items.Add(tab);
			}
		}

		private void OnTabChanged(object sender, SelectionChangedEventArgs e) {
			ListBox memList = BuildMemoryDisplay(memUnits[MemoryTabs.SelectedIndex]);
			if (MemoryTabs.SelectedItem is TabItem selectedTab) selectedTab.Content = memList;
		}

		private ListBox BuildMemoryDisplay(MappedMemoryUnit selectedUnit) {
			ListBox memList = new ListBox();
			for (uint index = 0; index < selectedUnit.MemUnit.Size; index += selectedUnit.MemUnit.WordSize) {
				uint? mappedAddress = (index < selectedUnit.EndAddr) ? index + selectedUnit.StartAddr : (uint?) null;
				uint relativeAddress = index;
				uint value = selectedUnit[index];
				ListBoxItem item = new ListBoxItem {Content = $"0x{relativeAddress:X8}: {value}"};
				memList.Items.Add(item);
			}
			
			return memList;
		}
		
		// To be deleted/repurposed
//		private void PopulateList() {
//			foreach (var unitInfo in mappingInfo) {
//				for (uint index = unitInfo.startAddr; index < unitInfo.endAddr; index += unitInfo.wordSize) {
//					BuildListItem($"0x{index:X8}: {mapper[index]}");
//					addressListBoxMap[index] = MemoryList.Items.Count - 1;
//				}
//			}
//		}

//		private void BuildListItem(object content) {
//			ListBoxItem item = new ListBoxItem {Content = content};
//			MemoryList.Items.Add(item);
//		}

		public void RefreshDisplay() {}
		
		private void OnValueChanged(object sender, ValueSetEventArgs e) {
			if (!Dispatcher.CheckAccess()) {
				Dispatcher.Invoke(delegate {OnValueChanged(sender, e);});
				return;
			}
			
//			if (MemoryList.Items[listIndex] is ListBoxItem item) item.Content = $"0x{e.Address:X8}: {mapper[e.Address]} UPDATED";
		}

		private void Close(object sender, EventArgs e) {
			mapper.ValueSet -= OnValueChanged;
		}
	}
}
