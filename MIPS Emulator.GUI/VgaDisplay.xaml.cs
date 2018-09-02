using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MIPS_Emulator.GUI {
	public partial class VgaDisplay : DebuggerView {
		private BitmapSource[] bitmaps;
		private ScreenMemory smem;
		private Image[] images;
		
		private bool hasSprite = false;
		private SpriteMemory spmem;
		private Image spriteImage;

		private const int gridWidth = 40;
		private const int gridHeight = 30;
		private const int bitmapWidth = 16;
		private const int bitmapHeight = 16;

		public VgaDisplay(Mips mips) {
			images = new Image[gridWidth * gridHeight];
			
			InitializeComponent();
			InitializeGrid(gridWidth, gridHeight);
			AddGridImages();
			BitmapMemory bmem = (BitmapMemory) mips.MemDict[typeof(BitmapMemory)][0];
			GenerateBitmaps(bmem);

			this.smem = (ScreenMemory) mips.MemDict[typeof(ScreenMemory)][0];

			hasSprite = mips.MemDict.ContainsKey(typeof(SpriteMemory)) &&
			            mips.MemDict.ContainsKey(typeof(SpriteBitmapMemory));

			if (hasSprite) {
				GenerateSpriteImage((SpriteBitmapMemory) mips.MemDict[typeof(SpriteBitmapMemory)][0]);
			}

			RefreshDisplay();
		}

		private void InitializeGrid(int l, int d) {
			for (int i = 0; i < l; i++) {
				displayGrid.ColumnDefinitions.Add(new ColumnDefinition() {
					Width = new GridLength(16)
				});
				if (i < d) {
					displayGrid.RowDefinitions.Add(new RowDefinition() {
						Height = new GridLength(16)
					});
				}
			}
		}

		private void AddGridImages() {
			for (int i = 0; i < gridWidth * gridHeight; i++) {
				Image cell = new Image();
				displayGrid.Children.Add(cell);
				Grid.SetRow(cell, i / gridWidth);
				Grid.SetColumn(cell, i % gridWidth);
				images[i] = cell;
			}
		}

		private void GenerateBitmaps(BitmapMemory bmem) {
			int bitmapCount = (int) (bmem.Size / (bitmapWidth * bitmapHeight * bmem.WordSize));
			bitmaps = new BitmapSource[bitmapCount];

			for (int i = 0; i < bitmapCount; i++) {
				var pixels = new byte[256 * 3];
				for (int j = 0; j < 256; j++) {
					uint pixel = bmem[(uint) ((i * 256 + j) * bmem.WordSize)];
					pixels[j * 3 + 2] = (byte) ((pixel >> 8) * 16);
					pixels[j * 3 + 1] = (byte) ((pixel >> 4 & 0xf) * 16);
					pixels[j * 3] = (byte) ((pixel & 0xf) * 16);
				}

				BitmapSource bitmap = BitmapSource.Create(bitmapWidth, bitmapHeight, 96, 96, PixelFormats.Bgr24, null, pixels,
					bitmapWidth * 3);
				bitmaps[i] = bitmap;
			}
		}

		private void GenerateSpriteImage(SpriteBitmapMemory spriteBitmapMemory) {
			var pixels = new byte[256 * 4];
			for (int j = 0; j < 256; j++) {
				uint pixel = spriteBitmapMemory[(uint) ((j) * spriteBitmapMemory.WordSize)];
				pixels[j * 4 + 3] = 128; //a
				pixels[j * 4 + 2] = (byte) ((pixel >> 8) * 16); //r
				pixels[j * 4 + 1] = (byte) ((pixel >> 4 & 0xf) * 16); //g
				pixels[j * 4] = (byte) ((pixel & 0xf) * 16); //b
			}
			BitmapSource bitmap = BitmapSource.Create(bitmapWidth, bitmapHeight, 96, 96, PixelFormats.Bgra32, null, pixels,
				bitmapWidth * 4);
			spriteImage = new Image();
			spriteImage.Source = bitmap;

		}

		public void RefreshDisplay() {
			for (int i = 0; i < gridWidth * gridHeight; i++) {
				images[i].Source = bitmaps[smem[(uint) i * smem.WordSize]];
			}
		}

		public void Close() {}
	}
}