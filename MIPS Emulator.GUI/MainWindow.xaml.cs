﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;

namespace MIPS_Emulator.GUI {
	public partial class MainWindow {
		private Mips mips;
		private List<DebuggerView> debuggerViews = new List<DebuggerView>();
		private Thread execution;
		private Thread refresh;
		private bool isExecuting = false;
		
		public MainWindow() {
			InitializeComponent();
		}

		private void OpenProject_Executed(object sender, RoutedEventArgs e) {
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Project files (*.json)|*.json|All files (*.*)|*.*";
			if (openFileDialog.ShowDialog() == true) {
				ProgramLoader loader = new ProgramLoader(new FileInfo(openFileDialog.FileName));
				mips = loader.Mips;
				
				VgaDisplay vga = new VgaDisplay(mips);
				Display.Child = vga;
				debuggerViews.Add(vga);
			}
		}

		private void RunAll_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
			e.CanExecute = !isExecuting && mips != null;
		}
		
		private void RunAll_Executed(object sender, RoutedEventArgs e) {
			isExecuting = true;
			execution = new Thread(ExecuteAll);
			execution.Start();
			refresh = new Thread(TickTimer);
			refresh.Start();
		}

		private void ExecuteAll() {
			while(isExecuting) {
				mips.ExecuteNext();
			}
		}

		private void TickTimer() {
			Timer timer = new Timer((state) => TickAll(), "state", 0, 33);
			while(isExecuting);
		}
		
		private void TickAll() {
			Dispatcher.Invoke(() => {
				foreach (DebuggerView view in debuggerViews) {
					view.RefreshDisplay();
				}
			});
		}

		private void Pause_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
			e.CanExecute = isExecuting && mips != null;
		}
		
		private void Pause_Executed(object sender, RoutedEventArgs e) {
			isExecuting = false;
			execution.Join();
		}
		
		private void StepForward_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
			e.CanExecute = !isExecuting && mips != null;
		}
		
		private void StepForward_Executed(object sender, RoutedEventArgs e) {
			isExecuting = true;
			mips.ExecuteNext();
			TickAll();
			isExecuting = false;
		}
		
		private void EmulatorViews_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
			e.CanExecute = mips != null;
		}
		
		private void ViewRegisters_Executed(object sender, RoutedEventArgs e) {
			RegistersViewer registers = new RegistersViewer(mips.Reg);
			registers.Top = this.Top;
			registers.Left = this.Left + this.Width;
			registers.Show();
			debuggerViews.Add(registers);
		}
		private void ViewInstructions_Executed(object sender, RoutedEventArgs e) {
			InstructionMemoryViewer imemViewer = new InstructionMemoryViewer(mips);
			imemViewer.Show();
			debuggerViews.Add(imemViewer);
		}
		
		private void Exit_Executed(object sender, EventArgs e) {
			foreach (DebuggerView view in debuggerViews) {
				view.Close();
			}
			refresh.Abort();
			execution.Abort();
		}
	}
}