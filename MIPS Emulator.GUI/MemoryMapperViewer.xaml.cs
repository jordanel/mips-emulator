using System.Windows.Controls;

namespace MIPS_Emulator.GUI {
	public partial class MemoryMapperViewer : DebuggerView {
		private MemoryMapper mapper;
		private uint[] addressListBoxMap;
		private const int wordSize = 4;
		
		public MemoryMapperViewer(MemoryMapper mapper) {
			InitializeComponent();
			this.mapper = mapper;

			addressListBoxMap = new uint[mapper.Size];
			PopulateList();
		}
		
		private void PopulateList() {
			bool isFirstBlank = true;
			for (uint index = mapper.StartAddr; index < (mapper.StartAddr + mapper.Size); index += wordSize) {
				try {
					BuildListItem($"0x{index:X8}: {mapper[index]}");
					addressListBoxMap[memoryList.Items.Count - 1] = index; // Reverse this?
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
			ListBoxItem item = new ListBoxItem();
			item.Content = content;
			memoryList.Items.Add(item);
		}
		
		public void RefreshDisplay() {
			//PopulateList();
		}
	}
}
