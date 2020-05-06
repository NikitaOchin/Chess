﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Chess
{
    public partial class Form1 : Form
    {
        int n; //Count of cell. (Desk n х n)
        Control[] Lb; //Array of cell
        Color[] main_color; //Colors for cover the desk
        int[] A; //Array to describe the knight's steps
        int[] p, np, lp; // Current, next and previous point of the knight's location
        int index = 0;//For count of the knight's steps

        public Form1()
        {
            InitializeComponent();
            n = 8;
            Lb = new Label[n * n];
            main_color = new Color[2] { Color.White, Color.Black };
            A = new int[4] { -2, 2, -1, 1 };
            p = new int[2] { 0, 0 };
            np = new int[2] { -1, -1 };
            lp = new int[2] { p[0], p[1] };

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int size = 720;
            int lb_size = (size) / n;
            this.Size = new Size(size+50, size+50);
            imageList1.Images.Add(Image.FromStream(new StreamReader("Конь.jpg").BaseStream));
            imageList1.ImageSize = new Size(lb_size, lb_size);
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    Lb[i * n + j] = new Label();
                    Lb[i * n + j].Parent = this;
                    Lb[i * n + j].Size = new Size(lb_size, lb_size);
                    Lb[i * n + j].BackColor = main_color[(i + j) % 2];
                    Lb[i * n + j].Location = new Point(i * lb_size + i, j * lb_size + j);
                    Lb[i * n + j].Text = 0.ToString();
                    Lb[i * n + j].ForeColor = main_color[(i + j + 1) % 2];
                }

            //It counts the number of possible horse moves for each cell
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    for (int k = 0; k < 2; k++)
                        for (int m = 2; m < 4; m++)
                        {
                            if (i + A[k] >= 0 && j + A[m] >= 0 && i + A[k] < n && j + A[m] < n)
                                Lb[i * n + j].Text = (Convert.ToInt32(Lb[i * n + j].Text) + 1).ToString();
                            if (i + A[m] >= 0 && j + A[k] >= 0 && i + A[m] < n && j + A[k] < n)
                                Lb[i * n + j].Text = (Convert.ToInt32(Lb[i * n + j].Text) + 1).ToString();
                        }

        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (p[0] == -1 && p[1] == -1)
            { timer1.Enabled = false; return; }

            //It covers previous place with red color. New place cover picture of knight 
            Lb[lp[0] * n + lp[1]].BackgroundImage = null;
            Lb[lp[0] * n + lp[1]].BackColor = Color.Red;
            Lb[lp[0] * n + lp[1]].Text = index++.ToString();
            Lb[lp[0] * n + lp[1]].ForeColor = Color.Black;
            Lb[p[0] * n + p[1]].BackgroundImage = imageList1.Images[0];

            //It takes away one move options from cell which is available for step
            for (int i = 0; i < 2; i++)
                for (int j = 2; j < 4; j++)
                {
                    if (p[0] + A[i] >= 0 && p[1] + A[j] >= 0 && p[0] + A[i] < n && p[1] + A[j] < n
                        && main_color.Contains(Lb[(p[0] + A[i]) * n + (p[1] + A[j])].BackColor))
                    {
                        Lb[(p[0] + A[i]) * n + (p[1] + A[j])].Text = (Convert.ToInt32(Lb[(p[0] + A[i]) * n + (p[1] + A[j])].Text) - 1).ToString();
                    }
                    if (p[0] + A[j] >= 0 && p[1] + A[i] >= 0 && p[0] + A[j] < n && p[1] + A[i] < n
                        && main_color.Contains(Lb[(p[0] + A[j]) * n + (p[1] + A[i])].BackColor))
                    {
                        Lb[(p[0] + A[j]) * n + (p[1] + A[i])].Text = (Convert.ToInt32(Lb[(p[0] + A[j]) * n + (p[1] + A[i])].Text) - 1).ToString();
                    }
                }

            //Find cell for next step - it has minimum move options.
            int min = 100;
            np[0] = -1; np[1] = -1;
            for (int i = 0; i < 2; i++)
                for (int j = 2; j < 4; j++)
                {
                    if (p[0] + A[i] >= 0 && p[1] + A[j] >= 0 && p[0] + A[i] < n && p[1] + A[j] < n 
                        && main_color.Contains(Lb[(p[0] + A[i]) * n + (p[1] + A[j])].BackColor) && Convert.ToInt32(Lb[(p[0] + A[i]) * n + (p[1] + A[j])].Text) <= min)
                    {
                        min = Convert.ToInt32(Lb[(p[0] + A[i]) * n + (p[1] + A[j])].Text); np[0] = p[0] + A[i]; np[1] = p[1] + A[j];
                    }
                    if (p[0] + A[j] >= 0 && p[1] + A[i] >= 0 && p[0] + A[j] < n && p[1] + A[i] < n 
                        && main_color.Contains(Lb[(p[0] + A[j]) * n + (p[1] + A[i])].BackColor) && Convert.ToInt32(Lb[(p[0] + A[j]) * n + (p[1] + A[i])].Text) <= min)
                    {
                        min = Convert.ToInt32(Lb[(p[0] + A[j]) * n + (p[1] + A[i])].Text); np[1] = p[1] + A[i]; np[0] = p[0] + A[j];
                    }
                }
            lp[0] = p[0]; lp[1] = p[1];
            p[0] = np[0]; p[1] = np[1];
        }
    }
}
