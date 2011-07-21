using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Diagnostics;

namespace SpiderSquisher
{
	public class SpiderSquisher : Form
	{
		private GameWindow game = new GameWindow();
		public SpiderSquisher()
		{
			this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer,true);
			ClientSize = game.ClientSize;
			Controls.Add(game);
			this.Refresh();
		}

		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus (e);

			game.Focus();
		}


		public static int Main(string[] args)
		{
			Application.Run(new SpiderSquisher());

			return 0;
		}
	}

	
}
