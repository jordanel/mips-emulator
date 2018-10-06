using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace MIPS_Emulator.GUI {
	public partial class MemoryMapperViewer : DebuggerView {
		private MemoryMapper mapper;
		private List<MappedMemoryUnit> memUnits;
		private ObservableCollection<MemoryLocationInfo> selectedMemoryContents;

		private string mappedAddressFormat = "0x{0:X8}";
		private string relativeAddressFormat = "0x{0:X8}";
		private string valueFormat = "0x{0:X8}";
		
		public MemoryMapperViewer(MemoryMapper mapper) {
			InitializeComponent();
			this.mapper = mapper;
			memUnits = mapper.MemUnits;

			InitializeTabs();
		}

		private void InitializeTabs() {
			foreach (MappedMemoryUnit mappedMemoryUnit in memUnits) {
				TabItem tab = new TabItem {Header = mappedMemoryUnit.Name};
				tab.Content = new ListView {View = BuildColumns()};
				MemoryTabs.Items.Add(tab);
			}
		}
		
		private GridView BuildColumns() {
			GridView gridView = new GridView();
			gridView.Columns.Add(BuildGridViewColumn("Mapped Address", "MappedAddress", mappedAddressFormat));
			gridView.Columns.Add(BuildGridViewColumn("Relative Address", "RelativeAddress", relativeAddressFormat));
			gridView.Columns.Add(BuildGridViewColumn("Value", "Value", valueFormat));

			ContextMenu context = new ContextMenu();
			MenuItem decimalItem = new MenuItem {Header = "Decimal"};
			decimalItem.Click += DecimalItem_Click;
			context.Items.Add(decimalItem);
			MenuItem hexItem = new MenuItem {Header = "Hexadecimal"};
			hexItem.Click += HexItem_Click;
			context.Items.Add(hexItem);
			MenuItem binaryItem = new MenuItem {Header = "Binary"};
			binaryItem.Click += BinaryItem_Click;
			context.Items.Add(binaryItem);
			gridView.ColumnHeaderContextMenu = context;
			
			return gridView;
		}
		
		private GridViewColumn BuildGridViewColumn(string header, string boundProperty, string format) {
			Binding binding = new Binding(boundProperty) {StringFormat = format, TargetNullValue = "Unmapped"};
			GridViewColumn mappedAddressColumn = new GridViewColumn {
				Header = header, DisplayMemberBinding = binding
			};
			return mappedAddressColumn;
		}

		private void OnTabChanged(object sender, SelectionChangedEventArgs e) {
			ListView memList = (ListView) ((TabItem) MemoryTabs.SelectedItem).Content;
			var memoryItems = GetMemoryContents(memUnits[MemoryTabs.SelectedIndex]);
			memList.ItemsSource = memoryItems;
			selectedMemoryContents = memoryItems;
		}

		private static ObservableCollection<MemoryLocationInfo> GetMemoryContents(MappedMemoryUnit selectedUnit) {
			ObservableCollection<MemoryLocationInfo> memoryItems = new ObservableCollection<MemoryLocationInfo>();
			for (uint index = 0; index < selectedUnit.Size; index += selectedUnit.WordSize) {
				uint? mappedAddress = (index + selectedUnit.StartAddr < selectedUnit.EndAddr) ? index + selectedUnit.StartAddr : (uint?) null;
				uint relativeAddress = index;
				uint value = selectedUnit[index];
				memoryItems.Add(new MemoryLocationInfo(mappedAddress, relativeAddress, value));
			}

			return memoryItems;
		}
		
		private void DecimalItem_Click(object sender, RoutedEventArgs e) {
			ChangeBindingStringFormat((MenuItem) sender, "{0}");
		}
		
		private void HexItem_Click(object sender, RoutedEventArgs e) {
			ChangeBindingStringFormat((MenuItem) sender, "0x{0:X8}");
		}
		
		// TODO: figure out how to format as binary
		private void BinaryItem_Click(object sender, RoutedEventArgs e) {
			ChangeBindingStringFormat((MenuItem) sender, "0b{0:B32}");
		}
		
		private void ChangeBindingStringFormat(MenuItem sender, string format) {
			MenuItem item = sender;
			ContextMenu contextMenu = (ContextMenu) item.Parent;
			GridViewColumnHeader header = (GridViewColumnHeader) contextMenu.PlacementTarget;
			GridViewColumn column = header.Column;
			string bindingPath = ((Binding) column.DisplayMemberBinding)?.Path.Path;
			column.DisplayMemberBinding = new Binding(bindingPath) {StringFormat = format, TargetNullValue = "Unmapped"};
		}

		public void RefreshDisplay() {
			MappedMemoryUnit selectedUnit = memUnits[MemoryTabs.SelectedIndex];
			for (uint index = selectedUnit.StartAddr; index < selectedUnit.StartAddr + selectedUnit.Size; index += selectedUnit.WordSize) {
				uint relativeAddress = index - selectedUnit.StartAddr;
				if (selectedMemoryContents[(int) (relativeAddress / selectedUnit.WordSize)].Value != selectedUnit[relativeAddress]) {
					uint? mappedAddress = (index < selectedUnit.EndAddr) ? index : (uint?) null;
					selectedMemoryContents[(int) (relativeAddress / selectedUnit.WordSize)] = new MemoryLocationInfo(mappedAddress, relativeAddress, mapper[index]);
				}
			}
		}

		private void Close(object sender, EventArgs e) {}
	}

	public class MemoryLocationInfo {
		public uint? MappedAddress { get; set; }
		public uint RelativeAddress { get; set; }
		public uint Value { get; set; }

		public MemoryLocationInfo(uint? mappedAddress, uint relativeAddress, uint value) {
			MappedAddress = mappedAddress;
			RelativeAddress = relativeAddress;
			Value = value;
		}
	}
}
