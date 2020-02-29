using System;
using System.Drawing;
using System.Windows.Forms;

namespace ReflectionIT.Minesweeper
{
	public class Square
	{
		public event EventHandler Dismantle;
		public event EventHandler Explode;

		private Button _button;
		private bool _dismantled = false;
		private Game _game;
		private bool _minded = false;
		private bool _opened = false;
		private int _x;
		private int _y;

		public Square(Game game, int x, int y)
		{
			_game = game;
			_x = x;
			_y = y;
			_button = new Button();
			Button.Text = "";
			
			int w = _game.Panel.Width / _game.Width;
			int h = _game.Panel.Height / _game.Height;

			_button.Width = w + 1;
			_button.Height = h + 1;
			_button.Left = w * X;
			_button.Top = h * Y;
			_button.Font = new System.Drawing.Font("Arial Black", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			_button.Click += new EventHandler(Click);
			_button.MouseDown += new MouseEventHandler(DismantleClick);

			_game.Panel.Controls.Add(Button);
		}

		public Button Button {
			get { return (this._button); }
		}

		private void Click(object sender, System.EventArgs e) {
			if (!Dismantled) {
				if (Minded) {
					Button.BackColor = Color.Red;
					OnExplode();
				} else {
					this.Open();
				}
			}
		}

		private void DismantleClick(object sender, MouseEventArgs e) {
			if (!Opened && e.Button == MouseButtons.Right) {
				if (Dismantled) {
					_dismantled = false;
					Button.BackColor = SystemColors.Control;
					Button.Text = "?";
				} else {
					_dismantled = true;
					Button.BackColor = Color.Green;
				}
				OnDismantle();
			}
		}

		public bool Dismantled {
			get { return (this._dismantled); }
		}

		public bool Minded {
			get { return (this._minded); }
			set { this._minded = value; }
		}

		protected void OnDismantle() {
			if (Dismantle != null) {
				Dismantle(this, new EventArgs());
			}
		}

		protected void OnExplode() {
			if (Explode != null) {
				Explode(this, new EventArgs());
			}
		}

		public void Open() {
			if (!Opened && !Dismantled) {
				_opened = true;
				// Count Bombs
				int c = 0;
				if (_game.IsBomb(X - 1, Y - 1)) c++;
				if (_game.IsBomb(X - 0, Y - 1)) c++;
				if (_game.IsBomb(X + 1, Y - 1)) c++;
				if (_game.IsBomb(X - 1, Y - 0)) c++;
				if (_game.IsBomb(X - 0, Y - 0)) c++;
				if (_game.IsBomb(X + 1, Y - 0)) c++;
				if (_game.IsBomb(X - 1, Y + 1)) c++;
				if (_game.IsBomb(X - 0, Y + 1)) c++;
				if (_game.IsBomb(X + 1, Y + 1)) c++;

				if (c > 0) {
					Button.Text = c.ToString();
					switch (c) {
						case 1:
							Button.ForeColor = Color.Blue;
							break;
						case 2:
							Button.ForeColor = Color.Green;
							break;
						case 3:
							Button.ForeColor = Color.Red;
							break;
						case 4:
							Button.ForeColor = Color.DarkBlue;
							break;
						case 5:
							Button.ForeColor = Color.DarkRed;
							break;
						case 6:
							Button.ForeColor = Color.LightBlue;
							break;
						case 7:
							Button.ForeColor = Color.Orange; // Guesed, never seen one!
							break;
						case 8:
							Button.ForeColor = Color.Ivory; // Guesed, never seen one!
							break;
					}
				} else {
					Button.BackColor = SystemColors.ControlLight;
					Button.FlatStyle = FlatStyle.Flat;
					Button.Enabled = false;

					_game.OpenSpot(X - 1, Y - 1);
					_game.OpenSpot(X - 0, Y - 1);
					_game.OpenSpot(X + 1, Y - 1);
					_game.OpenSpot(X - 1, Y - 0);
					_game.OpenSpot(X - 0, Y - 0);
					_game.OpenSpot(X + 1, Y - 0);
					_game.OpenSpot(X - 1, Y + 1);
					_game.OpenSpot(X - 0, Y + 1);
					_game.OpenSpot(X + 1, Y + 1);
				}
			}
		}

		public bool Opened {
			get { return (this._opened); }
		}

		public int X {
			get { return (this._x); }
		}

		public int Y {
			get { return (this._y); }
		}

		public void RemoveEvents() {
			_button.Click -= new EventHandler(Click);
			_button.MouseDown -= new MouseEventHandler(DismantleClick);
		}
	}
}
