using BetterFont;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace BFontCore.Forms
{
	public partial class MainForm : Form
	{
		private BFont currentFont;
		private string fontPath;

		public MainForm()
		{
			InitializeComponent();
		}

		private void UpdateLoadMode(bool system)
		{
			listSystem.Enabled = system;

			buttonFile.Enabled = !system;
			textFile.Enabled = !system;
		}

		private void SetFont(string path)
		{
			fontPath = path;

			if (path == null)
			{
				return;
			}

			currentFont = BFont.FromFile(path, new BFontOptions
			{
				Characters = textRenderText.Text,
				DistanceField = (int)numericDistanceField.Value,
				Padding = (int)numericPadding.Value,
				Size = (int)numericSize.Value,
				PageSize = int.Parse((string)comboPageSize.SelectedItem)
			});

			panelRender.Invalidate();
		}

		private void UpdateFont()
		{
			SetFont(fontPath);
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			listSystem.Items.AddRange(FontPath.SystemFonts);
			listSystem.SelectedItem = FontPath.SystemFonts.FirstOrDefault(p => p.Name == "Arial");
			listSystem.DisplayMember = "Name";

			comboPageSize.SelectedIndex = 2;

			UpdateLoadMode(true);
		}

		private void panelRender_Paint(object sender, PaintEventArgs e)
		{
			if (currentFont == null)
			{
				return;
			}

			e.Graphics.Clear(Color.Black);
			e.Graphics.DrawImageUnscaled(currentFont.Pages[0].ToBitmap(), Point.Empty);
		}

		private void buttonFile_Click(object sender, EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Filter = "TrueType Font (*.ttf)|*.ttf";

			DialogResult result = ofd.ShowDialog();

			if (result == DialogResult.OK)
			{
				SetFont(ofd.FileName);
			}
		}

		private void radioSystem_CheckedChanged(object sender, EventArgs e)
		{
			UpdateLoadMode(true);
		}

		private void radioFile_CheckedChanged(object sender, EventArgs e)
		{
			UpdateLoadMode(false);
		}

		private void listSystem_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (listSystem.SelectedItem is FontPath path)
			{
				SetFont(path.Path);
			}
		}

		private void textRenderText_TextChanged(object sender, EventArgs e)
		{
			UpdateFont();
		}

		private void numericSize_ValueChanged(object sender, EventArgs e)
		{
			UpdateFont();
		}

		private void comboPageSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			UpdateFont();
		}

		private void checkLcd_CheckedChanged(object sender, EventArgs e)
		{
			UpdateFont();
		}

		private void numericDistanceField_ValueChanged(object sender, EventArgs e)
		{
			UpdateFont();
		}

		private void numericPadding_ValueChanged(object sender, EventArgs e)
		{
			UpdateFont();
		}

		private class FontPath
		{
			public static FontPath[] SystemFonts { get; } = GetAllSystemFonts();

			public string Name { get; set; }
			public string Path { get; set; }

			private static FontPath[] GetAllSystemFonts()
			{
				return Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.Fonts))
					.Where(p => p.EndsWith(".ttf"))
					.Select(p => new FontPath
					{
						Name = System.IO.Path.GetFileNameWithoutExtension(p),
						Path = p
					}).ToArray();
			}
		}
	}
}
