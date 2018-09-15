using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Data;

namespace MIPS_Emulator.GUI {
	public partial class MemoryMapperViewer : DebuggerView {
		private MemoryMapper mapper;
		private List<MappedMemoryUnit> memUnits;
		private ObservableCollection<MappedLocation> currentList;

		private string mappedAddressFormat = "0x{0:X8}";
		private string relativeAddressFormat = "0x{0:X8}";
		private string valueFormat = "0x{0:X8}";
		
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
			// TODO: store current ObservableCollection in currentList
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
			
			// TODO: Trying this here for now
			currentList = mapped;

			return memList;
		}

		private GridView BuildColumns() {
			GridView gridView = new GridView();
			gridView.Columns.Add(BuildGridViewColumn("Mapped Address", "MappedAddress", mappedAddressFormat));
			gridView.Columns.Add(BuildGridViewColumn("Relative Address", "RelativeAddress", relativeAddressFormat));
			gridView.Columns.Add(BuildGridViewColumn("Value", "Value", valueFormat));

			return gridView;
		}

		private GridViewColumn BuildGridViewColumn(string header, string boundProperty, string format) {
			Binding binding = new Binding(boundProperty) {StringFormat = format};
			GridViewColumn mappedAddressColumn = new GridViewColumn {
				Header = header, DisplayMemberBinding = binding
			};
			return mappedAddressColumn;
		}

		public void RefreshDisplay() {}
		
		private void OnValueChanged(object sender, ValueSetEventArgs e) {
			if (!Dispatcher.CheckAccess()) {
				Dispatcher.Invoke(delegate {OnValueChanged(sender, e);});
				return;
			}
			
			// TODO: expose more MemUnit functionality in MappedMemUnit?
			MappedMemoryUnit selectedUnit = memUnits[MemoryTabs.SelectedIndex];
			if (e.Address >= selectedUnit.StartAddr &&
			    e.Address <= selectedUnit.StartAddr + selectedUnit.MemUnit.Size - 1) {
				uint relativeAddress = e.Address - selectedUnit.StartAddr;
				
				currentList[(int) (relativeAddress / selectedUnit.MemUnit.WordSize)] = new MappedLocation(e.Address, relativeAddress, e.Value);
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
