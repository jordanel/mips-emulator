using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using MessageBox = System.Windows.MessageBox;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using Timer = System.Threading.Timer;

namespace MIPS_Emulator.GUI {
	public partial class MainWindow {
		private Mips mips;
		private MappedMemoryUnit keyboard;
		private (AccelerometerX x, AccelerometerY y) accelerometer;
		private List<DebuggerView> debuggerViews = new List<DebuggerView>();
		private Thread execution;
		private bool isExecuting;
		private int cycleCount;
		private Stopwatch globalTickCounter = new Stopwatch();
		private Timer tickTimer;

		public MainWindow() {
			globalTickCounter.Start();
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
				
				accelerometer = ExtractAccelerometer();

				SoundMenu.IsEnabled = mips.Memory.MemUnits.Find(x => (x.MemUnit.GetType() == typeof(Sound))) != null;
				
				foreach (DebuggerView view in debuggerViews) {
					view.Close();
				}
				
				VgaDisplay vga = new VgaDisplay(mips);
				Display.Child = vga;
				debuggerViews.Add(vga);

				Title = $"MIPS Emulator - {mips.Name}";
				DefaultClockSpeed.CommandParameter = mips.ClockSpeed.ToString(CultureInfo.InvariantCulture);
				string defaultSpeed = (mips.ClockSpeed != 0) ? mips.ClockSpeed.ToString(CultureInfo.InvariantCulture) : "∞";
				DefaultClockSpeed.Header = $"Default ({defaultSpeed} MHz)";
			}
		}

		private (AccelerometerX, AccelerometerY) ExtractAccelerometer() {
			Accelerometer accelerometer = (Accelerometer) mips.Memory.MemUnits.Find(x => (x.MemUnit.GetType() == typeof(Accelerometer)))?.MemUnit;
			if (accelerometer == null) {
				AccelerometerX accelerometerX =
					(AccelerometerX) mips.Memory.MemUnits.Find(x => (x.MemUnit.GetType() == typeof(AccelerometerX)))?.MemUnit;
				AccelerometerY accelerometerY =
					(AccelerometerY) mips.Memory.MemUnits.Find(x => (x.MemUnit.GetType() == typeof(AccelerometerY)))?.MemUnit;
				return (accelerometerX, accelerometerY);
			}

			return (accelerometer.X, accelerometer.Y);
		}

		private void RunAll_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
			e.CanExecute = !isExecuting && mips != null;
		}
		
		private void RunAll_Executed(object sender, RoutedEventArgs e) {
			isExecuting = true;
			execution = mips.ClockSpeed == 0 ? new Thread(ExecuteAll) : new Thread(() => ExecuteAllThrottled(mips.ClockSpeed));
			execution.Start();
			tickTimer?.Dispose();
			tickTimer = new Timer((state) => TickAll(), "state", 0, 10);
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

		private void ExecuteAllThrottled(float clockSpeed) {
			var localStopwatch = new Stopwatch();
			localStopwatch.Start();
			var waitHandle = new AutoResetEvent(false);
			TimeSpan oneFrame = TimeSpan.FromSeconds(1 / 120.0);
			while (isExecuting) {
				int targetCycles = (int) (1_000_000 * clockSpeed / 120);
				try {
					for (int localCycleCount = 0;
						isExecuting && localCycleCount < targetCycles;
						localCycleCount++, Interlocked.Increment(ref cycleCount)) {
						mips.ExecuteNext();
					}
				}
				catch (Exception e) {
					HandleRuntimeException(e);
				}
				TimeSpan elapsedTime = localStopwatch.Elapsed;
				if (elapsedTime > oneFrame) {
					localStopwatch.Restart();
					continue;
				}
				waitHandle.WaitOne(oneFrame - elapsedTime);
				localStopwatch.Restart();
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
				TimeSpan timeSinceLastCheck = globalTickCounter.Elapsed;
				globalTickCounter.Restart();
				double hertz = cycleCount / timeSinceLastCheck.TotalMilliseconds / 1_000;
				cycleCount = 0;
				Dispatcher.Invoke(() => {
					string title = $"MIPS Emulator - {mips.Name} - {hertz:F} MHz";
					if (mips.ClockSpeed != 0) {
						title += $" ({Math.Round(hertz / mips.ClockSpeed * 100)}%)";
					}
					Title = title;
				});
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

		private void ViewAccelerometer_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
			e.CanExecute = mips != null && !accelerometer.Equals((null, null));
		}
		
		private void ViewAccelerometer_Executed(object sender, RoutedEventArgs e) {
			AccelerometerControl accelerometerControl = new AccelerometerControl(accelerometer.x, accelerometer.y);
			accelerometerControl.Top = this.Top - accelerometerControl.Height;
			accelerometerControl.Left = this.Left;
			accelerometerControl.Show();
			debuggerViews.Add(accelerometerControl);
		}

		private void OpenAllViews_Executed(object sender, RoutedEventArgs e) {
			ViewRegisters_Executed(sender, e);
			ViewInstructions_Executed(sender, e);
			ViewMemory_Executed(sender, e);
			if (!accelerometer.Equals((null, null))) {
				ViewAccelerometer_Executed(sender, e);
			}
		}
		
		private void Exit_Executed(object sender, EventArgs e) {
			foreach (DebuggerView view in debuggerViews) {
				view.Close();
			}
			execution.Abort();
		}

		private void SetSoundShape_Executed(object sender, ExecutedRoutedEventArgs e) {
			if (Enum.TryParse((string) e.Parameter, false, out SoundWaveGenerator.WaveShape shape)) {
				SoundModule.generator.Shape = shape;
			}

			CheckSelectedMenuItem(e);
		}
		
		private void SetClockSpeed_Executed(object sender, ExecutedRoutedEventArgs e) {
			float newClockSpeed = float.Parse((string) e.Parameter);
			mips.ClockSpeed = newClockSpeed;
			
			CheckSelectedMenuItem(e);
		}

		private static void CheckSelectedMenuItem(ExecutedRoutedEventArgs e) {
			MenuItem selectedItem = (MenuItem) e.OriginalSource;
			foreach (MenuItem item in ((MenuItem) selectedItem.Parent).Items) {
				item.IsChecked = false;
			}
			selectedItem.IsChecked = true;
		}

		#endregion

		private void OnKeyDown(object sender, KeyEventArgs e) {
			if (keyboard != null) {
				Keyboard kb = (Keyboard) keyboard.MemUnit;
				kb.SetKeyCode(ScanCodeMapper.GetScanCode(e.Key));
			}
		}

		private void OnKeyUp(object sender, KeyEventArgs e) {
			if (keyboard != null) {
				Keyboard kb = (Keyboard) keyboard.MemUnit;
				uint scanCode = ScanCodeMapper.GetScanCode(e.Key);
				if (scanCode == 0xE07C) {
					// Special case for print screen
					kb.SetKeyCode(0xE0F012);
				} else if (scanCode > 0xFF) {
					kb.SetKeyCode(scanCode | 0xE0F000);	
				} else {
					kb.SetKeyCode(scanCode | 0xF000);
				}
			}
		}
	}
}
