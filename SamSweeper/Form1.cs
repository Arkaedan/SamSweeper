using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SamSweeper {
    public partial class Form1 : Form {

        const int width = 20;
        const int height = 20;

        Field mineField;
        Button[,] buttons;

        int btnSize;
        int posX;
        int posY;

        public Form1() {
            InitializeComponent();

            mineField = new Field(width, height);

            btnSize = 30;
            posX = 4;
            posY = 4;

            buttons = new Button[width, height];
            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    buttons[x, y] = new Button();
                    Button btn = buttons[x, y];
                    btn.Enabled = true;
                    btn.Width = btnSize;
                    btn.Height = btnSize;
                    btn.Text = "";
                    btn.Font = new Font(btn.Font, btn.Font.Style | FontStyle.Bold);
                    btn.Location = new Point(posX + x * btnSize, posY + y * btnSize);
                    btn.TabStop = false;
                    btn.MouseDown += new MouseEventHandler(btn_Click);
                    btn.Tag = mineField.GetTile(x, y);
                    this.Controls.Add(btn);
                }
            }
        }

        private void ResetGame() {
            foreach (Button btn in buttons) {
                btn.Text = "";
                btn.Enabled = true;
                btn.BackColor = default(Color);
                btn.UseVisualStyleBackColor = true;
            }
            mineField.Reset();
        }

        public void btn_Click(Object sender, MouseEventArgs e) {

            Button btn = (Button)sender;
            Tile tile = (Tile)(btn.Tag);

            if (e.Button == MouseButtons.Right) {
                if (btn.Text == "") {
                    btn.Text = "P";
                } else {
                    btn.Text = "";
                }
                return;
            }

            if (btn.Text == "P") {
                return;
            }

            if (mineField.IsEmpty) {
                mineField.PlaceMines(tile.X, tile.Y);
            }

            btn.BackColor = Color.Gray;

            if (tile.IsBomb) {
                btn.Text = "B";
                MessageBox.Show("You dead bro!");
                ResetGame();
            } else {
                btn.Text = tile.NumSurroundingBombs.ToString();
            }
        } // End btn_Click

    }
}
