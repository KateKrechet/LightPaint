using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LightPaint
{
    public partial class Form1 : Form
    {
        //Создаем холст для рисования
        Graphics graphics;
        SolidBrush brush;
        SolidBrush brush1;

        int x;
        int y;
        float secondAngle;//угол для отклонения веток

        public Form1()
        {
            InitializeComponent();
            graphics = pictureBox1.CreateGraphics();//Связываем холст и PictureBox
            brush = new SolidBrush(Color.PaleGoldenrod);//присваивание цвета кисти и инициализация объекта кисти
            brush1 = new SolidBrush(Color.Black);
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (radioButton1.Checked)
            {
                if (e.Button == MouseButtons.Left)//при нажатии левой кнопкой мыши рисуем
                                                  //положение мыши
                    graphics.FillEllipse(brush, e.X, e.Y, trackBar1.Value, trackBar1.Value);
                if (e.Button == MouseButtons.Right)//при нажатии правой кнопкой мыши рисуем
                                                   //положение мыши
                    graphics.FillEllipse(brush1, e.X, e.Y, trackBar1.Value, trackBar1.Value);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                brush.Color = colorDialog1.Color;
            button1.BackColor = colorDialog1.Color;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                brush1.Color = colorDialog1.Color;
            button2.BackColor = colorDialog1.Color;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            x = e.X;
            y = e.Y;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (radioButton2.Checked)
            {
                Pen pen = new Pen(brush);
                if (e.Button == MouseButtons.Left)
                    pen = new Pen(brush, trackBar1.Value);
                if (e.Button == MouseButtons.Right)
                    pen = new Pen(brush1, trackBar1.Value);

                graphics.DrawLine(pen, x, y, e.X, e.Y);
            }
        }

        void DrawLine(float x, float y, float angle, int length)
        {
            if (length < 10)
                return;

            float x1 = (float)Math.Cos(angle * (Math.PI / 180)) * length + x;
            float y1 = (float)Math.Sin(angle * (Math.PI / 180)) * length + y;
            Pen pen = new Pen(brush, trackBar1.Value);
            if (length < 40)
                pen.Color = brush1.Color;//верхняя крона другим цветом
            graphics.DrawLine(pen, x, y, x1, y1);

            DrawLine(x1, y1, angle - secondAngle, length - 10);
            DrawLine(x1, y1, angle + secondAngle, length - 10);

        }

        void DrawGear(int count, int length)
        {
            float angle = -90;
            float secondAngle = 360 / count;
            int x = pictureBox1.Width / 2;
            int y = pictureBox1.Height / 2;
            for (int i = 0; i <= count; i++)
            {
                float x1 = (float)Math.Cos(angle * (Math.PI / 180)) * length + x;
                float y1 = (float)Math.Sin(angle * (Math.PI / 180)) * length + y;
                Pen pen = new Pen(brush, trackBar1.Value);
                graphics.DrawLine(pen, x, y, x1, y1);
                angle += secondAngle;
            }
            graphics.FillEllipse(brush, x - 80, y - 80, 160, 160);
            graphics.FillEllipse(new SolidBrush(Color.White), x - 20, y - 20, 40, 40);
        }
        void DrawTree()
        {
            Point point1 = new Point(50, 150);
            Point point2 = new Point(150, 50);
            Point point3 = new Point(250, 150);

            Point[] Points = { point1, point2, point3 };

            graphics.FillPolygon(brush, Points);

           Point point4 = new Point(50,200);
            Point point5 = new Point(150,100);
            Point point6 = new Point(250,200);

            Point[] Points1 = { point4, point5, point6 };

            graphics.FillPolygon(brush, Points1);

            Point point7 = new Point(50, 250);
            Point point8 = new Point(150, 150);
            Point point9 = new Point(250, 250);

            Point[] Points2 = { point7, point8, point9 };

            graphics.FillPolygon(brush, Points2);

            Rectangle rect = new Rectangle(140, 250,20, 50);
            graphics.FillRectangle(new SolidBrush(Color.Brown), rect);


        }
        private void button3_Click(object sender, EventArgs e)
        {
            graphics = pictureBox1.CreateGraphics();//холст подстраиваем под размер экрана
            graphics.Clear(Color.White);
            secondAngle = (float)Convert.ToDouble(textBox1.Text);
            secondAngle *= -1;
            DrawLine(pictureBox1.Width / 2, pictureBox1.Height - 40, -90, 100);
        }

        private void button4_Click(object sender, EventArgs e)//Шестеренка
        {
            graphics = pictureBox1.CreateGraphics();
            graphics.Clear(Color.White);
            int count = Convert.ToInt32(textBox2.Text);
            DrawGear(count, 100);
        }

        private void button5_Click(object sender, EventArgs e)//Елка
        {
            graphics = pictureBox1.CreateGraphics();
            graphics.Clear(Color.White);
            DrawTree();
        }
    }
}
