using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LB4_Csharp
{
    public partial class Form1 : Form
    {
        enum DrawingTool
        {
            Line,
            Ellip,
            Rect,
            None
        }
        private int lastX, lastY, X, Y;
        bool drawing = false;
        private DrawingTool drawingTool;
        Pen pen = new Pen(Color.Red);
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = ImageFromScreen();
        }

        public Bitmap ImageFromScreen()
        {
            Bitmap bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height,
                PixelFormat.Format32bppRgb);
            using (Graphics gr = Graphics.FromImage(bmp))
            {
                gr.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y,
                    0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
            }
            return bmp;
        }
        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            X = e.X;
            Y = e.Y;
            drawing = true;
        }

        private void Line_CheckedChanged(object sender, EventArgs e)
        {
            panel2.Visible = false;
            if (Line.Checked == true)
            {
                drawingTool = DrawingTool.Line;
            }
            else drawingTool = DrawingTool.None;                  
        }

        private void Ellip_CheckedChanged(object sender, EventArgs e)
        {
            panel2.Visible = true; 
            if(Ellip.Checked == true)
            {
                drawingTool = DrawingTool.Ellip;
            }
            else drawingTool = DrawingTool.None;
        }

        private void Rect_CheckedChanged(object sender, EventArgs e)
        {
            panel2.Visible = true;
            if(Rect.Checked == true)
            {
                drawingTool = DrawingTool.Rect;
            }
            else drawingTool = DrawingTool.None;
        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            panel2.Visible = false;
            if (radioButton1.Checked)
                drawingTool = DrawingTool.None;
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < textBox1.Text.Length; i++)
            {
                if (textBox1.Text[i] < 48 || textBox1.Text[i] > 57)
                    textBox1.Text = "";
            }
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < textBox1.Text.Length; i++)
            {
                if (textBox1.Text[i] < 48 || textBox1.Text[i] > 57)
                    textBox1.Text = "";
            }
        }

        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (drawing)
            {
                switch(drawingTool)
                {
                    case DrawingTool.None:
                        {
                            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);
                            SolidBrush redBrush = new SolidBrush(Color.Red);
                            g.FillEllipse(redBrush, e.X, e.Y, 10, 10);
                            break; 

                        }
                    default:
                        {
                            lastX = e.X;
                            lastY = e.Y;
                            pictureBox1.Invalidate();
                            break;
                        }
                }
               
            }
        }

        private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            drawing = false;
            pictureBox1.Image = ImageFromScreen();
            lastX = -1;
            lastY = -1;
            X = -1;
            Y = -1;
        }

        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if(drawing)
            switch (drawingTool)
            {
                case DrawingTool.Line:
                       e.Graphics.DrawLine(pen, lastX, lastY, X, Y); break;
                case DrawingTool.Ellip:
                        if (!string.IsNullOrWhiteSpace(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox2.Text))
                            e.Graphics.DrawEllipse(pen, lastX, lastY, Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text));
                    break;
                case DrawingTool.Rect:
                        if (!string.IsNullOrWhiteSpace(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox2.Text))
                            e.Graphics.DrawRectangle(pen, lastX, lastY, Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text));
                    break;
                case DrawingTool.None:
                        break;
            }

          
        }
    }
}
