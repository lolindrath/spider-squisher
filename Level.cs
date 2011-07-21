using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Diagnostics;

namespace SpiderSquisher
{
	public class Level
	{
		public Bitmap background;
		private Bitmap[] sprites = new Bitmap[GameWindow.NUM_SPRITES];

		public enum Sprites
		{
			Pot = 12,
			CharWithPot = 13,
			Char = 14,
			Spider = 15,
			Blank = 1000
		};

		private int charPos = 3;

		private Sprites[,] initialState = new Sprites[GameWindow.SCREEN_WIDTH,GameWindow.SCREEN_HEIGHT]
			{
				{Sprites.Blank, Sprites.Blank, Sprites.Blank, Sprites.Pot, Sprites.Blank, Sprites.Blank, Sprites.Blank, Sprites.Blank, Sprites.Blank},
				{Sprites.Blank, Sprites.Blank, Sprites.Blank, Sprites.Blank, Sprites.Blank, Sprites.Blank, Sprites.Blank, Sprites.Blank, Sprites.Blank},
				{Sprites.Blank, Sprites.Blank, Sprites.Blank, Sprites.Pot, Sprites.Blank, Sprites.Blank, Sprites.Blank, Sprites.Blank, Sprites.Blank},
				{Sprites.Blank, Sprites.Blank, Sprites.Blank, Sprites.Blank, Sprites.Blank, Sprites.Blank, Sprites.Blank, Sprites.Blank, Sprites.Blank},
				{Sprites.Blank, Sprites.Blank, Sprites.Blank, Sprites.Pot, Sprites.Blank, Sprites.Blank, Sprites.Blank, Sprites.Blank, Sprites.Blank},
				{Sprites.Blank, Sprites.Blank, Sprites.Blank, Sprites.Blank, Sprites.Blank, Sprites.Blank, Sprites.Blank, Sprites.Blank, Sprites.Blank},
				{Sprites.Blank, Sprites.Blank, Sprites.Blank, Sprites.Pot, Sprites.Blank, Sprites.Blank, Sprites.Blank, Sprites.Blank, Sprites.Blank},
				{Sprites.Blank, Sprites.Blank, Sprites.Blank, Sprites.Blank, Sprites.Blank, Sprites.Blank, Sprites.Blank, Sprites.Blank, Sprites.Blank},
				{Sprites.Blank, Sprites.Blank, Sprites.Blank, Sprites.Pot, Sprites.Blank, Sprites.Blank, Sprites.Blank, Sprites.Blank, Sprites.Blank}
			};

		public Sprites[,] gameState = new Sprites[GameWindow.SCREEN_WIDTH,GameWindow.SCREEN_HEIGHT];

		public enum PotStates
		{
			Still,
			Dropping
		};

		public PotStates[] potState = new PotStates[GameWindow.SCREEN_WIDTH] { PotStates.Still, PotStates.Still, PotStates.Still, PotStates.Still, PotStates.Still, PotStates.Still, PotStates.Still, PotStates.Still, PotStates.Still };

		public Level()
		{
			gameState = (Sprites[,])initialState.Clone();
			gameState[3,3] = Sprites.Char;
			LoadSprites();
		}

		public void LoadSprites()
		{
			background = new Bitmap(GetType().Assembly.GetManifestResourceStream("SpiderSquisher.background.bmp"));
			Bitmap temp = new Bitmap(GetType().Assembly.GetManifestResourceStream("SpiderSquisher.blocks1.bmp"));


			sprites[0] = GetSprite(temp, 0, 0, 32, 32, 2);
			sprites[1] = GetSprite(temp, 1, 0, 32, 32, 2);
			sprites[2] = GetSprite(temp, 2, 0, 32, 32, 2);
			sprites[3] = GetSprite(temp, 3, 0, 32, 32, 2);
			sprites[4] = GetSprite(temp, 4, 0, 32, 32, 2);
			sprites[5] = GetSprite(temp, 5, 0, 32, 32, 2);
			sprites[6] = GetSprite(temp, 5, 0, 32, 32, 2);
			sprites[7] = GetSprite(temp, 6, 0, 32, 32, 2);
			sprites[8] = GetSprite(temp, 7, 0, 32, 32, 2);
			sprites[9] = GetSprite(temp, 8, 0, 32, 32, 2);
			sprites[10] = GetSprite(temp, 9, 0, 32, 32, 2);
			sprites[11] = GetSprite(temp, 10, 0, 32, 32, 2);
			sprites[12] = GetSprite(temp, 11, 0, 32, 32, 2);
			sprites[12].MakeTransparent(Color.Black);
			sprites[13] = GetSprite(temp, 12, 0, 32, 32, 2);
			sprites[13].MakeTransparent(Color.Black);
			sprites[14] = GetSprite(temp, 13, 0, 32, 32, 2);
			sprites[14].MakeTransparent(Color.Black);
			sprites[15] = GetSprite(temp, 14, 0, 32, 32, 2);
			sprites[15].MakeTransparent(Color.Black);
		}

		/*public Bitmap GetBGImage(int x, int y)
		{
			return sprites[(int)background[x,y]];
		}*/

		public Bitmap GetImage(int x, int y)
		{
			return sprites[(int)gameState[x,y]];
		}

		public void MoveLeft()
		{
			int oldPos = charPos;
			--charPos;
			Debug.WriteLine("L CharPos: " + charPos);

			if(charPos >= 0 && charPos <= GameWindow.SCREEN_WIDTH-1)
			{
				if(initialState[charPos, 3] == Sprites.Pot)
				{
					gameState[charPos, 3] = Sprites.CharWithPot;
				}
				else
				{
					gameState[charPos, 3] = Sprites.Char;
				}
				
				if(gameState[oldPos, 3] == Sprites.CharWithPot)
				{
					gameState[oldPos, 3] = Sprites.Pot;
				}
				else
				{
					gameState[oldPos, 3] = Sprites.Blank;
				}
			}
			else
			{
				charPos = oldPos;
			}
		}

		public void MoveRight()
		{
			int oldPos = charPos;
			++charPos;
			Debug.WriteLine("R CharPos: " + charPos);

			if(charPos >= 0 && charPos <= GameWindow.SCREEN_WIDTH-1)
			{
				if(initialState[charPos, 3] == Sprites.Pot)
				{
					gameState[charPos, 3] = Sprites.CharWithPot;
				}
				else
				{
					gameState[charPos, 3] = Sprites.Char;
				}

				if(gameState[oldPos, 3] == Sprites.CharWithPot && FindPot(oldPos) == 3)
				{
					gameState[oldPos, 3] = Sprites.Pot;
				}
				else
				{
					gameState[oldPos, 3] = Sprites.Blank;
				}
			}
			else
			{
				charPos = oldPos;
			}
		}

		public void DropPot()
		{
			if(gameState[charPos,3] == Sprites.CharWithPot)
			{
				potState[charPos] = PotStates.Dropping;
			}
		}

		public Bitmap GetSprite(Bitmap b, int x, int y, int height, int width, int offset)
		{
			Rectangle size = new Rectangle((x*width) + ((x+1)*offset), (y*height) + ((y+1)*offset), width, height);
			Debug.WriteLine(size.ToString());
			return b.Clone(size, PixelFormat.Format32bppRgb);
		}

		public void UpdatePots()
		{
			for(int i = 0; i < GameWindow.SCREEN_WIDTH; i++)
			{
				if(potState[i] == Level.PotStates.Dropping)
				{
					int x = FindPot(i);
					if(x < GameWindow.SCREEN_HEIGHT-2)
					{
						if(gameState[i,x] == Sprites.CharWithPot)
						{
							gameState[i, x] = Sprites.Char;
						}
						else
						{
							gameState[i, x] = Sprites.Blank;
						}

						gameState[i, x+1] = Sprites.Pot;
					}
					else
					{
						potState[i] = PotStates.Still;
						gameState[i, FindPot(i)] = Sprites.Blank;

						if(gameState[i,3] == Sprites.Char)
						{
							gameState[i,3] = Sprites.CharWithPot;
						}
						else
						{
							gameState[i, 3] = Sprites.Pot;
						}
					}
				}
			}
		}

		private int FindPot(int x)
		{
			for(int i = 0; i < GameWindow.SCREEN_HEIGHT; i++)
			{
				if(gameState[x, i] == Sprites.Pot || gameState[x, i] == Sprites.CharWithPot)
				{
					return i;
				}
			}

			return -1;
		}

		public void UpdateSpiders()
		{
			//here be the spider update
		}
	}
}