using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess
{
    public partial class Form1 : Form
    {
        Control[] Lb;
        int n = 8;
        int[] p = new int[2] { 0, 0 };
        Color main_color = Color.Blue;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Lb = new Label[n*n];
            
            for(int i = 0; i < n; i++)
                for(int j = 0; j < n; j++)
                {
                    Lb[i * n + j] = new Label();
                    Lb[i * n + j].Parent = this;
                    Lb[i * n + j].Visible = true;
                    Lb[i * n + j].Size = new Size(100, 100);
                    Lb[i * n + j].BackColor = main_color;
                    Lb[i * n + j].Location = new Point(i * 100 + i, j * 100 + j);
                    Lb[i * n + j].Click += new EventHandler(Click_label);
                    Lb[i * n + j].Text = 0.ToString();
                    Lb[i * n + j].Name = (i * n + j).ToString();
                }
        }

        private void Click_label(object sender, EventArgs e)
        {
            Control l = sender as Label;
            int m = Convert.ToInt32(l.Text);
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    if (Lb[i * n + j].BackgroundImage != null)
                    {
                        Lb[i * n + j].BackgroundImage = null;
                        Lb[i * n + j].BackColor = Color.Red;
                    }
                    Lb[i * n + j].Text = 0.ToString();
                }
            if (l.BackColor == main_color)
                l.BackgroundImage = imageList1.Images[0];
            else
                l.BackColor = main_color;
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    if (Lb[i * n + j].BackColor == Color.Red) continue;
                    if (i + 2 >= 0 && j + 1 >= 0  && i + 2 < n && j + 1 < n && Lb[(i + 2) * n + (j + 1)].BackColor == main_color)
                        Lb[i * n + j].Text = (Convert.ToInt32(Lb[i * n + j].Text) + 1).ToString();
                    if (i + 2 >= 0 && j - 1 >= 0 && i + 2 < n && j - 1 < n && Lb[(i + 2) * n + (j - 1)].BackColor == main_color)
                        Lb[i * n + j].Text = (Convert.ToInt32(Lb[i * n + j].Text) + 1).ToString();
                    if (i + 1 >= 0 && j - 2 >= 0 && i + 1 < n && j - 2 < n && Lb[(i + 1) * n + (j - 2)].BackColor == main_color)
                        Lb[i * n + j].Text = (Convert.ToInt32(Lb[i * n + j].Text) + 1).ToString();
                    if (i + 1 >= 0 && j + 2 >= 0 && i + 1 < n && j + 2 < n && Lb[(i + 1) * n + (j + 2)].BackColor == main_color)
                        Lb[i * n + j].Text = (Convert.ToInt32(Lb[i * n + j].Text) + 1).ToString();

                    if (i - 2 >= 0 && j + 1 >= 0 && i - 2 < n && j + 1 < n && Lb[(i - 2) * n + (j + 1)].BackColor == main_color)
                        Lb[i * n + j].Text = (Convert.ToInt32(Lb[i * n + j].Text) + 1).ToString();
                    if (i - 2 >= 0 && j - 1 >= 0 && i - 2 < n && j - 1 < n && Lb[(i - 2) * n + (j - 1)].BackColor == main_color)
                        Lb[i * n + j].Text = (Convert.ToInt32(Lb[i * n + j].Text) + 1).ToString();
                    if (i - 1 >= 0 && j - 2 >= 0 && i - 1 < n && j - 2 < n && Lb[(i - 1) * n + (j - 2)].BackColor == main_color)
                        Lb[i * n + j].Text = (Convert.ToInt32(Lb[i * n + j].Text) + 1).ToString();
                    if (i - 1 >= 0 && j + 2 >= 0 && i - 1 < n && j + 2 < n && Lb[(i - 1) * n + (j + 2)].BackColor == main_color)
                        Lb[i * n + j].Text = (Convert.ToInt32(Lb[i * n + j].Text) + 1).ToString();
                }
            timer1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            p[0] = r.Next(0, n - 1);
            p[1] = r.Next(0, n - 1);
            foreach (Label l in Lb)
                l.BackColor = main_color;
            Click_label(Lb[p[0] * n + p[1]], new EventArgs());
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int min = 8;
            int[] np = new int[2] { 0, 0 };
            
            if (p[0] + 2 >= 0 && p[1] + 1 >= 0 && p[0] + 2 < n && p[1] + 1 < n && Lb[(p[0] + 2) * n + (p[1] + 1)].BackColor == main_color && Convert.ToInt32(Lb[(p[0] + 2) * n + (p[1] + 1)].Text)<=min)
            {
                min = Convert.ToInt32(Lb[(p[0] + 2) * n + (p[1] + 1)].Text); np[0] = p[0] + 2; np[1] = p[1] + 1;
            }
            if (p[0] + 2 >= 0 && p[1] - 1 >= 0 && p[0] + 2 < n && p[1] - 1 < n && Lb[(p[0] + 2) * n + (p[1] - 1)].BackColor == main_color && Convert.ToInt32(Lb[(p[0] + 2) * n + (p[1] - 1)].Text) <= min)
            {
                min = Convert.ToInt32(Lb[(p[0] + 2) * n + (p[1] - 1)].Text); np[0] = p[0] + 2; np[1] = p[1] - 1;
            }
            if (p[0] + 1 >= 0 && p[1] - 2 >= 0 && p[0] + 1 < n && p[1] - 2 < n && Lb[(p[0] + 1) * n + (p[1] - 2)].BackColor == main_color && Convert.ToInt32(Lb[(p[0] + 1) * n + (p[1] - 2)].Text) <= min)
            {
                min = Convert.ToInt32(Lb[(p[0] + 1) * n + (p[1] - 2)].Text); np[0] = p[0] + 1; np[1] = p[1] - 2;
            }
            if (p[0] + 1 >= 0 && p[1] + 2 >= 0 && p[0] + 1 < n && p[1] + 2 < n && Lb[(p[0] + 1) * n + (p[1] + 2)].BackColor == main_color && Convert.ToInt32(Lb[(p[0] + 1) * n + (p[1] + 2)].Text) <= min)
            {
                min = Convert.ToInt32(Lb[(p[0] + 1) * n + (p[1] + 2)].Text); np[0] = p[0] + 1; np[1] = p[1] + 2;
            }
            if (p[0] - 2 >= 0 && p[1] + 1 >= 0 && p[0] - 2 < n && p[1] + 1 < n && Lb[(p[0] - 2) * n + (p[1] + 1)].BackColor == main_color && Convert.ToInt32(Lb[(p[0] - 2) * n + (p[1] + 1)].Text) <= min)
            {
                min = Convert.ToInt32(Lb[(p[0] - 2) * n + (p[1] + 1)].Text); np[0] = p[0] - 2; np[1] = p[1] + 1;
            }
            if (p[0] - 2 >= 0 && p[1] - 1 >= 0 && p[0] - 2 < n && p[1] - 1 < n && Lb[(p[0] - 2) * n + (p[1] - 1)].BackColor == main_color && Convert.ToInt32(Lb[(p[0] - 2) * n + (p[1] - 1)].Text) <= min)
            {
                min = Convert.ToInt32(Lb[(p[0] - 2) * n + (p[1] - 1)].Text); np[0] = p[0] - 2; np[1] = p[1] - 1;
            }
            if (p[0] - 1 >= 0 && p[1] - 2 >= 0 && p[0] - 1 < n && p[1] - 2 < n && Lb[(p[0] - 1) * n + (p[1] - 2)].BackColor == main_color && Convert.ToInt32(Lb[(p[0] - 1) * n + (p[1] - 2)].Text) <= min)
            {
                min = Convert.ToInt32(Lb[(p[0] - 1) * n + (p[1] - 2)].Text); np[0] = p[0] - 1; np[1] = p[1] - 2;
            }
            if (p[0] - 1 >= 0 && p[1] + 2 >= 0 && p[0] - 1 < n && p[1] + 2 < n && Lb[(p[0] - 1) * n + (p[1] + 2)].BackColor == main_color && Convert.ToInt32(Lb[(p[0] - 1) * n + (p[1] + 2)].Text) <= min)
            {
                min = Convert.ToInt32(Lb[(p[0] - 1) * n + (p[1] + 2)].Text); np[0] = p[0] - 1; np[1] = p[1] + 2;
            }
            if (p[0] == np[0] && p[1] == np[1]) timer1.Enabled = false;
            p[0] = np[0]; p[1] = np[1];
            Click_label(Lb[p[0] * n + p[1]], new EventArgs());

        }
    }
}
