using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;

namespace MIPS_Emulator.GUI {
	public partial class MainWindow {
		private Mips mips;
		private MappedMemoryUnit keyboard;
		private List<DebuggerView> debuggerViews = new List<DebuggerView>();
		private Thread execution;
		private bool isExecuting;
		private int cycleCount;
		private DateTime lastCheck = DateTime.Now;
		private Timer tickTimer;

		public MainWindow() {
			InitializeComponent();
			KeyDown += OnKeyDown;
			KeyUp += OnKeyUp;
		}

		#region CommandMethods

		private void OpenProject_Executed(object sender, RoutedEventArgs e) {
			isExecuting = false;
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Project files (*.json)|*.json|All files (*.*)|*.*";
			if (openFileDialog.ShowDialog() == true) {
				ProgramLoader loader;
				try {
					loader = new ProgramLoader(new FileInfo(openFileDialog.FileName));
				} catch (Exception ex) {
					MessageBox.Show($"Error initializing project: {ex}");
					return;
				}

				mips = loader.Mips;
				keyboard = mips.Memory.MemUnits.Find(x => (x.MemUnit.GetType() == typeof(Keyboard)));
				
				foreach (DebuggerView view in debuggerViews) {
					view.Close();
				}
				
				VgaDisplay vga = new VgaDisplay(mips);
				Display.Child = vga;
				debuggerViews.Add(vga);

				Title = $"MIPS Emulator - {mips.Name}";
			}
		}

		private List<MemoryUnit> GetMemoryTypeIfPresent(Type type) {
			return (mips.MemDict.TryGetValue(type, out var memories) && memories.Count != 0) ? memories : null;
		}

		private void RunAll_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
			e.CanExecute = !isExecuting && mips != null;
		}
		
		[MethodImpl(MethodImplOptions.NoOptimization | MethodImplOptions.NoInlining)]
		private void RunAll_Executed(object sender, RoutedEventArgs e) {
			isExecuting = true;
			execution = new Thread(ExecuteAll);
			execution.Start();
			tickTimer = new Timer((state) => TickAll(), "state", 0, 33);
		}

		private void ExecuteAll() {
			try {
				for (; isExecuting; Interlocked.Increment(ref cycleCount)) {
					mips.ExecuteNext();
				}
			}
			catch (Exception e) {
				HandleRuntimeException(e);
			}
		}

		private void TryExecuteNextInstruction() {
			try {
				mips.ExecuteNext();
			} catch (Exception e) {
				HandleRuntimeException(e);
			}
		}

		private void HandleRuntimeException(Exception e) {
			if (e.GetType() != typeof(ThreadAbortException)) {
				MessageBox.Show($"Runtime Exception encountered: {e}");
				isExecuting = false;
			}
		}
		
		private void TickAll() {
			Dispatcher.Invoke(() => {
				foreach (DebuggerView view in debuggerViews) {
					view.RefreshDisplay();
				}
			});
			UpdateFrequencyDisplay();
		}

		private void UpdateFrequencyDisplay() {
			if (cycleCount > 10_000_000) {
				TimeSpan timeSinceLastCheck = DateTime.Now - lastCheck;
				double hertz = cycleCount / timeSinceLastCheck.TotalSeconds / 1_000_000;
				Dispatcher.Invoke(() => { Title = $"MIPS Emulator - {mips.Name} - {hertz:F} MHz"; });

				lastCheck = DateTime.Now;
				cycleCount = 0;
			}
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
			TryExecuteNextInstruction();
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
			imemViewer.Top = this.Top;
			imemViewer.Left = this.Left - imemViewer.Width;
			imemViewer.Show();
			debuggerViews.Add(imemViewer);
		}
		
		private void ViewMemory_Executed(object sender, RoutedEventArgs e) {
			MemoryMapperViewer memoryViewer = new MemoryMapperViewer(mips.Memory);
			memoryViewer.Top = this.Top + this.Height;
			memoryViewer.Left = this.Left;
			memoryViewer.Show();
			debuggerViews.Add(memoryViewer);
		}

		private void OpenAllViews_Executed(object sender, RoutedEventArgs e) {
			ViewRegisters_Executed(sender, e);
			ViewInstructions_Executed(sender, e);
			ViewMemory_Executed(sender, e);
		}
		
		private void Exit_Executed(object sender, EventArgs e) {
			foreach (DebuggerView view in debuggerViews) {
				view.Close();
			}
			execution.Abort();
		}

		#endregion

		private void OnKeyDown(object sender, KeyEventArgs e) {
			if (keyboard != null) {
				Keyboard kb = (Keyboard) keyboard.MemUnit;
				kb.SetKeyCode(ScanCodeMapper.GetScanCode(e.Key));
				mips.Memory[keyboard.StartAddr] = ScanCodeMapper.GetScanCode(e.Key);          // Setter used to update MemoryMapperViewer
			}
		}

		private void OnKeyUp(object sender, KeyEventArgs e) {
			if (keyboard != null) {
				Keyboard kb = (Keyboard) keyboard.MemUnit;
				kb.SetKeyCode(ScanCodeMapper.GetScanCode(e.Key) | 0xF000);
				mips.Memory[keyboard.StartAddr] = ScanCodeMapper.GetScanCode(e.Key) | 0xF000; // Setter used to update MemoryMapperViewer
			}
		}
	}
}
