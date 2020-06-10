﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab12
{
    public partial class Form1 : Form
    {
        Bitmap bmp;
        Bitmap bmpCry;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) //OPEN
        {
            // выбор рисунка в котором будет храниться информация
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files (*.BMP, *.JPG, *.GIF, *.TIF, *.PNG, *.ICO, *.EMF, *.WMF)|*.bmp;*.jpg;*.gif; *.tif; *.png; *.ico; *.emf; *.wmf";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (bmp != null)
                {
                    bmp.Dispose();
                    pictureBox1.Image.Dispose();
                }
                if (bmpCry != null)
                {
                    bmpCry.Dispose();
                    pictureBox2.Image.Dispose();
                }
                Image image = Image.FromFile(dialog.FileName);
                int width = image.Width;
                int height = image.Height;
                bmp = new Bitmap(image, width, height);
                pictureBox1.Image = bmp;
            }
        }

        private void button2_Click(object sender, EventArgs e) //save
        {
            // сохранение рисунка с внедренным текстом
            SaveFileDialog savedialog = new SaveFileDialog();
            savedialog.Title = "Сохранить...";
            savedialog.OverwritePrompt = true;
            savedialog.CheckPathExists = true;
            savedialog.Filter =
                "Bitmap File(*.bmp)|*.bmp|" +
                "GIF File(*.gif)|*.gif|" +
                "JPEG File(*.jpg)|*.jpg|" +
                "TIF File(*.tif)|*.tif|" +
                "PNG File(*.png)|*.png";
            if (savedialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = savedialog.FileName;
                bmpCry.Save(fileName);
            }
        }
        // кодирование
        private void button3_Click(object sender, EventArgs e)
        {
            // считывание текста для внедрение
            string txt = textBox1.Text;
            int len = Math.Min(txt.Length, 255);
            bmpCry = (Bitmap)bmp.Clone();
            if (len != 0 && bmp != null)
            {
                int n = bmp.Height;
                int m = bmp.Width;
                for (int i = 0; i < 8; i++)
                {
                    Color p = bmp.GetPixel(i, n - 1);
                    int a = p.A;
                    int r = p.R;
                    int g = p.G;
                    int b = p.B;
                    // внедрение бита в последний бит байта красного цвета
                    r = ((r & 254) | ((len & (1 << i)) > 0 ? 1 : 0));
                    p = Color.FromArgb(a, r, g, b);
                    bmpCry.SetPixel(i, n - 1, p);
                }

                int x = 8;
                int y = n - 1;
                for (int i = 0; i < len; i++)
                {
                    int c = txt[i];
                    for (int j = 0; j < 8; j++)
                    {
                        if (x >= m)
                        {
                            y--;
                            x = 0;
                        }
                        Color p = bmp.GetPixel(x, y);
                        int a = p.A;
                        int r = p.R;
                        int g = p.G;
                        int b = p.B;
                        r = ((r & 254) | ((c & (1 << j)) > 0 ? 1 : 0));
                        p = Color.FromArgb(a, r, g, b);
                        bmpCry.SetPixel(x, y, p);
                        x++;
                    }
                }
                pictureBox2.Image = bmpCry;
            }
        }
        //декодирование
        private void button4_Click(object sender, EventArgs e)
        {
            string txt = "";
            int len = 0;
            bmpCry = (Bitmap)bmp.Clone();
            if (bmp != null)
            {
                int n = bmp.Height;
                int m = bmp.Width;
                // вытаскиваем биты текста
                for (int i = 0; i < 8; i++)
                {
                    Color p = bmp.GetPixel(i, n - 1);
                    int r = p.R;

                    len = len | ((r & 1) << i);
                }

                int x = 8;
                int y = n - 1;
                for (int i = 0; i < len; i++)
                {
                    int c = 0;

                    for (int j = 0; j < 8; j++)
                    {
                        if (x >= m)
                        {
                            y--;
                            x = 0;
                        }
                        Color p = bmp.GetPixel(x, y);
                        int r = p.R;

                        c = c | ((r & 1) << j);
                        x++;
                    }
                    txt += (char)(c);
                }

                label1.Text = txt;
            }
        }
    }
}
