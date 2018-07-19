using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;
using Microsoft.Win32;

namespace GUI {
	public partial class FileSelect {
		List<MipsToolbar> toolbars = new List<MipsToolbar>();
		
		public FileSelect() {
			InitializeComponent();
		}

		private void OpenFile(object sender, RoutedEventArgs e) {
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Project files (*.json)|*.json|All files (*.*)|*.*";
			if (openFileDialog.ShowDialog() == true) {
				FileName.Text = openFileDialog.FileName;
			}
		}

		private void Start(object sender, RoutedEventArgs e) {
			MipsToolbar toolbar = new MipsToolbar(new FileInfo(FileName.Text));
			toolbars.Add(toolbar);
			toolbar.Show();
		}

		private void FileSelect_OnClosing(object sender, CancelEventArgs e) {
			foreach (MipsToolbar toolbar in toolbars) {
				toolbar.Close();
			}
		}
	}
}
