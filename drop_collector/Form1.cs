using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            button1.Text = "Play";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        struct Drop
        {
            public bool exist;
            public int X, Y, R;
        }

        struct Backet
        {
            public int X, Y, W, H;
        }

        Drop[] drops = new Drop[12];
        int lives, score;
        Backet backet = new Backet();

        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < drops.Length; i = i + 1)
            {
                drops[i].Y = drops[i].Y + 6;
                if(drops[i].Y > backet.Y && drops[i].exist)
                {
                    if (drops[i].X > backet.X && drops[i].X < backet.X + backet.W)
                    {
                        score = score + 1;
                    }
                    else
                    {
                        lives = lives - 1;
                    }
                    drops[i].exist = false;
                }
            }
            panel1.Invalidate();
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            backet.X = e.X;
            panel1.Invalidate();
        }

        private void panel1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            label1.Text = String.Format("Score = {0}", score);
            label2.Text = String.Format("Lives = {0}", lives);
            if(lives <= 0)
            {
                e.Graphics.DrawString("Yoy are lose!", DefaultFont, new SolidBrush(Color.Red), 100, 100);
                return;
            }
            if (score >= 10)
            {
                e.Graphics.DrawString("You are wine!", DefaultFont, new SolidBrush(Color.Green), 100, 100);
                return;
            }
            for (int i = 0; i < drops.Length; i = i + 1)
            {
                if(drops[i].exist)
                    e.Graphics.DrawEllipse(new Pen(Color.Blue), new Rectangle(drops[i].X - drops[i].R, drops[i].Y - drops[i].R, 2 * drops[i].R, 2 * drops[i].R));
            }
            e.Graphics.DrawRectangle(new Pen(Color.Black), new Rectangle(backet.X, backet.Y, backet.W, backet.H));
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            lives = 3;
            score = 0;

            backet.X = 200;
            backet.Y = 250;
            backet.W = 100;
            backet.H = 100;


            Random random = new Random(0);
            for(int i = 0, y = 0; i < drops.Length; i = i + 1)
            {
                drops[i].exist = true;
                drops[i].X = (int)Math.Round(random.NextDouble()*(panel1.Width - 10));
                y = y - 50 - (int)Math.Round(random.NextDouble() * 100);
                drops[i].Y = y;
                drops[i].R = 10;
            }

            timer1.Start();
            panel1.Invalidate();
        }   
    }
}
