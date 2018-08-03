using System.Windows.Input;

namespace MIPS_Emulator.GUI {
	public static class Commands {
		public static readonly RoutedUICommand Exit = new RoutedUICommand (
			"Exit",
			"Exit",
			typeof(Commands),
			new InputGestureCollection()
			{
				new KeyGesture(Key.F4, ModifierKeys.Alt)
			}
		);
		
		public static readonly RoutedUICommand Run = new RoutedUICommand (
			"Run",
			"Run",
			typeof(Commands),
			new InputGestureCollection()
			{
				new KeyGesture(Key.F5)
			}
		);
		
		public static readonly RoutedUICommand ViewRegisters = new RoutedUICommand (
			"ViewRegisters",
			"ViewRegisters",
			typeof(Commands),
			new InputGestureCollection()
			{
				new KeyGesture(Key.R, ModifierKeys.Control)
			}
		);
		
		public static readonly RoutedUICommand ViewInstructions = new RoutedUICommand (
			"ViewInstructions",
			"ViewInstructions",
			typeof(Commands),
			new InputGestureCollection()
			{
				new KeyGesture(Key.I, ModifierKeys.Control)
			}
		);
	}
}