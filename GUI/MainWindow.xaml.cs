using MIPS_Emulator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

		private List<DebuggerView> debuggerViews = new List<DebuggerView>();
		private Mips mips;

        public MainWindow()
        {
            InitializeComponent();
			ProgramLoader loader = new ProgramLoader(new FileInfo("../../../projects/imem_test/imemtest.json"));
			mips = loader.Mips;
		}

        private void View_imem(object sender, RoutedEventArgs e) {
            var imemWindow = new InsturctionMemoryViewer(mips);
            imemWindow.Show();
			debuggerViews.Add(imemWindow);
        }

		private void ViewRegisters(object sender, RoutedEventArgs e) {
			var registersWindow = new RegistersViewer(mips);
			registersWindow.Show();
			debuggerViews.Add(registersWindow);
		}

        private void ViewVGA(object sender, RoutedEventArgs e)
        {
            var vgaWindow = new VGADisplay(mips);
            vgaWindow.Show();
            debuggerViews.Add(vgaWindow);
        }

	    private void OpenAll(object sender, RoutedEventArgs e) {
		    var imemWindow = new InsturctionMemoryViewer(mips);
		    imemWindow.Show();
		    debuggerViews.Add(imemWindow);
			var registersWindow = new RegistersViewer(mips);
		    registersWindow.Show();
		    debuggerViews.Add(registersWindow);
			var vgaWindow = new VGADisplay(mips);
		    vgaWindow.Show();
		    debuggerViews.Add(vgaWindow);
	    }

		private void Step(object sender, RoutedEventArgs e) {
			mips.ExecuteNext();
			foreach (var view in debuggerViews) {
				view.Tick();
			}
		}
	    
	    private void RunAll(object sender, RoutedEventArgs e) {
		    var thread1 = new Thread(() => ExecuteAll(mips));
			thread1.Start();
			var thread2 = new Thread(() => TickTimer());
			thread2.Start();

		}

	    private void ExecuteAll(Mips mips) {
			while(true) {
				mips.ExecuteNext();
			}
		    
	    }

		private void TickTimer() {
			var Timer = new Timer((state) => TickAll(debuggerViews), "state", 0, 33);
			while(true);
		}
		

	    private void TickAll(List<DebuggerView> debuggerViews) {
			this.Dispatcher.Invoke(() => {
				foreach (var view in debuggerViews) {
					view.Tick();
				}
			});
		}

	    private void MainWindow_OnClosing(object sender, CancelEventArgs e) {
			Console.Beep();
			//TODO: Something
	    }
    }
}
