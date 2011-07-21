using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Diagnostics;

namespace SpiderSquisher
{
	public class GameWindow : Panel
	{
		//Constants
		public const int SPRITE_HEIGHT = 32;
		public const int SPRITE_WIDTH = 32;
		public const int SCREEN_HEIGHT = 9;
		public const int SCREEN_WIDTH = 9;
		public const int NUM_SPRITES = 20;

		private Timer timer = new Timer();
		private bool[] keyState = new bool[2];
		private Level level = new Level();
		
		public GameWindow()
		{
			this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer,true);
			this.Dock = DockStyle.Fill;
			//this.BackColor = Color.Blue;

			ClientSize = new Size(SPRITE_WIDTH * SCREEN_WIDTH, SPRITE_HEIGHT * SCREEN_HEIGHT);

			timer.Interval = 300;
			timer.Start();
			timer.Tick += new EventHandler(timer_Tick);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint (e);
			Graphics g = e.Graphics;

			g.DrawImage(level.background, 0, 0, 9 * 32, 9 * 32);

			for(int x = 0; x < SCREEN_WIDTH; x++)
			{
				for(int y = 0; y < SCREEN_HEIGHT; y++)
				{
					Rectangle r = new Rectangle(x * SPRITE_WIDTH, y * SPRITE_HEIGHT, SPRITE_WIDTH, SPRITE_HEIGHT);
					//Debug.WriteLine("Paint: " + r.ToString());
					if(e.ClipRectangle.IntersectsWith(r))
					{	
						if(level.gameState[x,y] != Level.Sprites.Blank)
						{
							g.DrawImageUnscaled(level.GetImage(x,y), r);
						}
					}
				}
			}
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			//base.OnKeyDown (e);

			if(e.KeyCode == Keys.Left)
			{
				
			}
			else if(e.KeyCode == Keys.Right)
			{

			}

		}

		protected override void OnKeyUp(KeyEventArgs e)
		{
			//base.OnKeyUp (e);

			if(e.KeyCode == Keys.Left)
			{
				level.MoveLeft();
			}
			else if(e.KeyCode == Keys.Right)
			{
				level.MoveRight();
			}
			else if(e.KeyCode == Keys.Enter)
			{
				level.DropPot();
			}

			this.Refresh();
		}

		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			base.OnKeyPress (e);
		}


		private void timer_Tick(object sender, EventArgs e)
		{
			level.UpdatePots();
			this.Refresh();
			//Debug.WriteLine("Tick");
		}
	}

	
}
