using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows;

namespace MIPS_Emulator.GUI {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MipsToolbar {
		private List<DebuggerView> debuggerViews = new List<DebuggerView>();
		private Mips mips;
		private Thread execution;
		private Thread refresh;

		public MipsToolbar(FileInfo project) {
			InitializeComponent();
			ProgramLoader loader = new ProgramLoader(project);
			mips = loader.Mips;
		}

		private void ViewImem(object sender, RoutedEventArgs e) {
			InstructionMemoryViewer imemWindow = new InstructionMemoryViewer(mips);
			imemWindow.Show();
			debuggerViews.Add(imemWindow);
		}

		private void ViewRegisters(object sender, RoutedEventArgs e) {
			RegistersViewer registersWindow = new RegistersViewer(mips.Reg);
			registersWindow.Show();
			debuggerViews.Add(registersWindow);
		}

		private void ViewVga(object sender, RoutedEventArgs e) {
			VgaDisplay vgaWindow = new VgaDisplay(mips);
			vgaWindow.Show();
			debuggerViews.Add(vgaWindow);
		}

		private void OpenAll(object sender, RoutedEventArgs e) {
			ViewImem(sender, e);
			ViewRegisters(sender, e);
			ViewVga(sender, e);
		}

		private void Step(object sender, RoutedEventArgs e) {
			mips.ExecuteNext();
			foreach (DebuggerView view in debuggerViews) {
				view.RefreshDisplay();
			}
		}
	    
		private void RunAll(object sender, RoutedEventArgs e) {
			execution = new Thread(ExecuteAll);
			execution.Start();
			refresh = new Thread(TickTimer);
			refresh.Start();
		}

		private void ExecuteAll() {
			while(true) {
				mips.ExecuteNext();
			}  
		}

		private void TickTimer() {
			Timer timer = new Timer((state) => TickAll(), "state", 0, 33);
			while(true);
		}
		
		private void TickAll() {
			this.Dispatcher.Invoke(() => {
				foreach (DebuggerView view in debuggerViews) {
					view.RefreshDisplay();
				}
			});
		}

		private void MipsToolbar_OnClosing(object sender, CancelEventArgs e) {
			foreach (DebuggerView view in debuggerViews) {
				view.Close();
			}
			refresh.Abort();
			execution.Abort();
		}
	}
}