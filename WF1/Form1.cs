using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace WF1
{
    public partial class Form1 : Form
    {
        Bitmap pic;
        Brush br;
        Pen p, er;
        Graphics g;
        int x1, y1, x_l, y_l;
        int height, width;
        public Form1()
        {
            InitializeComponent();
            radioButton2.Checked = true;
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            pic = new Bitmap(1000, 1000);
            g = Graphics.FromImage(pic);
            p = new Pen(panel2.BackColor, trackBar1.Value);
            er = new Pen(pictureBox1.BackColor, trackBar1.Value);
            br = new SolidBrush(panel2.BackColor);
            x1 = y1 = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            panel2.BackColor = b.BackColor;
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
                pic.Save(saveFileDialog1.FileName);
        }

        private void оПрогеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Пусто");
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != "")
            {
                pic = (Bitmap)Image.FromFile(openFileDialog1.FileName);
                pictureBox1.Image = pic;
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (radioButton1.Checked) 
            {
                x1 = e.X;
                y1 = e.Y;
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (radioButton1.Checked)
            {
                x_l = x1; y_l = y1;
                height = Math.Abs(e.Y - y1);
                width = Math.Abs(e.X - x1);

                if (e.X < x1)
                    x1 = e.X;
                if (e.Y < y1)
                    y1 = e.Y;

                if (comboBox1.Text == "Эллипс")
                    g.DrawEllipse(p, x1, y1, width, height);
                else if (comboBox1.Text == "Прямоугольник")
                    g.DrawRectangle(p, x1, y1, width, height);
                else if (comboBox1.Text == "Линия")
                    g.DrawLine(p, x_l, y_l, e.X, e.Y);

                pictureBox1.Image = pic;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            //System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
            Cursor.Current = Cursors.Default;
            g = Graphics.FromImage(pic);
            p = new Pen(panel2.BackColor, trackBar1.Value);
            er = new Pen(pictureBox1.BackColor, trackBar1.Value);
            br = new SolidBrush(panel2.BackColor);
            p.EndCap = LineCap.Round;
            p.StartCap = LineCap.Round;

            if (radioButton2.Checked)
            {
                if (comboBox2.Text == "Карандаш")
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        g.DrawLine(p, x1, y1, e.X, e.Y);
                        pictureBox1.Image = pic;
                    }
                    x1 = e.X;
                    y1 = e.Y;
                }
                else if (comboBox2.Text == "Кисть") 
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        g.FillEllipse(br, x1, y1, trackBar1.Value, trackBar1.Value);
                        pictureBox1.Image = pic;
                    }
                    x1 = e.X;
                    y1 = e.Y;
                } 
                
            } else if (radioButton3.Checked) 
            {
                Cursor.Current = Cursors.Cross;
                if (e.Button == MouseButtons.Left)
                {
                    g.DrawLine(er, x1, y1, e.X, e.Y);
                    pictureBox1.Image = pic;
                }
                x1 = e.X;
                y1 = e.Y;
            }
        }

    }
} 
