using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Data;

namespace MIPS_Emulator.GUI {
	public partial class MemoryMapperViewer : DebuggerView {
		private MemoryMapper mapper;
		private List<MappedMemoryUnit> memUnits;
		private ListView selectedList;
		
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
				MemoryTabs.Items.Add(tab);
			}
		}

		private void OnTabChanged(object sender, SelectionChangedEventArgs e) {
			ListView memList = BuildMemoryDisplay(memUnits[MemoryTabs.SelectedIndex]);
			if (MemoryTabs.SelectedItem is TabItem selectedTab) selectedTab.Content = memList;
			selectedList = memList;
		}

		private ListView BuildMemoryDisplay(MappedMemoryUnit selectedUnit) {
			ListView memList = new ListView();
			ObservableCollection<MappedLocation> mapped = new ObservableCollection<MappedLocation>();
			for (uint index = 0; index < selectedUnit.MemUnit.Size; index += selectedUnit.MemUnit.WordSize) {
				uint? mappedAddress = (index < selectedUnit.EndAddr) ? index + selectedUnit.StartAddr : (uint?) null;
				uint relativeAddress = index;
				uint value = selectedUnit[index];
				mapped.Add(new MappedLocation(mappedAddress, relativeAddress, value));
			}
			
			memList.View = BuildColumns();
			memList.ItemsSource = mapped;

			return memList;
		}

		private GridView BuildColumns() {
			GridView gridView = new GridView();
			GridViewColumn mappedAddressColumn = new GridViewColumn {
				Header = "Mapped Address", DisplayMemberBinding = new Binding("MappedAddress")
			};
			GridViewColumn relativeAddressColumn = new GridViewColumn {
				Header = "Relative Address", DisplayMemberBinding = new Binding("RelativeAddress")
			};
			GridViewColumn valueColumn = new GridViewColumn {
				Header = "Value", DisplayMemberBinding = new Binding("Value")
			};
			gridView.Columns.Add(mappedAddressColumn);
			gridView.Columns.Add(relativeAddressColumn);
			gridView.Columns.Add(valueColumn);

			return gridView;
		}

		public void RefreshDisplay() {}
		
		private void OnValueChanged(object sender, ValueSetEventArgs e) {
			if (!Dispatcher.CheckAccess()) {
				Dispatcher.Invoke(delegate {OnValueChanged(sender, e);});
				return;
			}
			
			// TODO: expose more MemUnit functionality in MappedMemUnit, send changed value in args?
			MappedMemoryUnit selectedUnit = memUnits[MemoryTabs.SelectedIndex];
			if (e.Address >= selectedUnit.StartAddr &&
			    e.Address <= selectedUnit.StartAddr + selectedUnit.MemUnit.Size - 1) {
				uint relativeAddress = e.Address - selectedUnit.StartAddr;
				
				if (selectedList.Items[(int) (relativeAddress / selectedUnit.MemUnit.WordSize)] is ListViewItem item) item.Content = $"0x{relativeAddress:X8}: {e.Value} UPDATED";
			}
		}

		private void Close(object sender, EventArgs e) {
			mapper.ValueSet -= OnValueChanged;
		}
	}

	public class MappedLocation {
		public uint? MappedAddress { get; set; }
		public uint RelativeAddress { get; set; }
		public uint Value { get; set; }

		public MappedLocation(uint? mappedAddress, uint relativeAddress, uint value) {
			MappedAddress = mappedAddress;
			RelativeAddress = relativeAddress;
			Value = value;
		}
	}
}
