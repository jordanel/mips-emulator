using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MIPS_Emulator;

namespace GUI
{
    /// <summary>
    /// Interaction logic for VGA_Display.xaml
    /// </summary>
    public partial class VGADisplay : Window, DebuggerView
    {
	    private Mips mips;
	    private BitmapSource[] bitmaps;
	    private ScreenMemory smem;
		private Image[] images = new Image[40 * 30];

	    public VGADisplay(Mips mips)
        {
            InitializeComponent();
            for (var i = 0; i < 40; i++) {
                displayGrid.ColumnDefinitions.Add(new ColumnDefinition() {
                    Width = new GridLength(16)
                });
                if (i < 30) {
                    displayGrid.RowDefinitions.Add(new RowDefinition() {
                        Height = new GridLength(16)
                    });
                }
            }

	        this.mips = mips;
			Console.WriteLine(typeof(BitmapMemory));
	        BitmapMemory bmem = (BitmapMemory) mips.MemDict[typeof(BitmapMemory)][0];
	        int bitmapCount = (int) (bmem.Size / 4 / 16 / 16);
			bitmaps = new BitmapSource[bitmapCount];

	        for (var i = 0; i < bitmapCount; i++) {
		        var pixels = new byte[256 * 3];
		        for (var j = 0; j < 256; j++) {
			        uint p = bmem[(uint) ((i * 256 + j) * 4)];
			        pixels[j * 3 + 2] = (byte) ((p >> 8) * 16);
			        pixels[j * 3 + 1] = (byte) ((p >> 4 & 0xf) * 16); 
					pixels[j * 3] = (byte) ((p & 0xf) * 16);
				}
		        var bitmap = BitmapSource.Create(16, 16, 96, 96, PixelFormats.Bgr24, null, pixels, 16*3);
		        bitmaps[i] = bitmap;
	        }

	        for (var i = 0; i < 40 * 30; i++) {
				Image cell = new Image();
		        
		        displayGrid.Children.Add(cell);
				Grid.SetRow(cell, i/40);
				Grid.SetColumn(cell, i % 40);
		        images[i] = cell;
	        }

	        this.smem = (ScreenMemory) mips.MemDict[typeof(ScreenMemory)][0];

			Tick();

        }

	    public void Tick() {
		    for (var i = 0; i < 40 * 30; i++) {
			    images[i].Source = bitmaps[smem[(uint) i * 4]];
			}
    }
    }
}
