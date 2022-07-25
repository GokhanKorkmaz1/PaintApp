using PaintApp.Shapes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PaintApp
{
    public partial class Form1 : Form
    {
        private Graphics g;
        private Pen pen;
        private List<Shape> Shapes = new List<Shape>();
        private int x1, x2, y1, y2;
        private int selectedShape = -1;
        private bool shapeChooser = false;
        private string filePath;

        enum ShapeSelector
        {
            Square, Circle, Triangle, Hexagon
        };
        public Form1()
        {
            InitializeComponent();
            g = panel1.CreateGraphics();

            // set and mark color black for init
            pen = new Pen(Color.Black, 1);
            markColor(pictureBox6);
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            pen.Color = p.BackColor;
            shapeChooser = false;
            markColor(p);
            markShapeChooser();

        }
        private void triangleBox_Click(object sender, EventArgs e)
        {
            selectedShape = (int)ShapeSelector.Triangle;
            shapeChooser = false;
            markSelectedShape();
            markShapeChooser();
        }

        private void squareBox_Click(object sender, EventArgs e)
        {
            selectedShape = (int)ShapeSelector.Square;
            shapeChooser = false;
            markSelectedShape();
            markShapeChooser();
        }

        private void circleBox_Click(object sender, EventArgs e)
        {
            selectedShape = (int)ShapeSelector.Circle;
            shapeChooser = false;
            markSelectedShape();
            markShapeChooser();
        }

        private void hexagonBox_Click(object sender, EventArgs e)
        {
            selectedShape = (int)ShapeSelector.Hexagon;
            shapeChooser = false;
            markSelectedShape();
            markShapeChooser();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            x1 = e.X;
            y1 = e.Y;
        }

        private void ClearScreenBox_Click(object sender, EventArgs e)
        {
            clearScreen();
        }
        private void SelectShapeBox_Click(object sender, EventArgs e)
        {
            shapeChooser = shapeChooser == true ? false : true;
            markShapeChooser();
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            x2 = e.X;
            y2 = e.Y;
            drawShape();
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            selectShape(panel1.PointToClient(Cursor.Position).X, panel1.PointToClient(Cursor.Position).Y);
            markShapeChooser();
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            panel1.Cursor = shapeChooser == true ? Cursors.Hand : Cursors.Default;
        }

        private void importFromFileBox_Click(object sender, EventArgs e)
        {
            clearScreen();
            getFileName();
            ReadToTxt(filePath);
        }

        private void DeleteSelectedShapeBox_Click(object sender, EventArgs e)
        {
            foreach (Shape shape in Shapes.ToList())
            {
                if (shape.selected)
                {
                    shape.DeleteShape(g, pen);
                    Shapes.Remove(shape);
                    shape.DeleteSelected(g);
                }
            }
        }

        private void SavetoFileBox_Click(object sender, EventArgs e)
        {
            getFileName();
            SaveToTxt(filePath, Shapes);
            clearScreen();
        }

        private void clearScreen()
        {
            panel1.Refresh();
            Shapes.Clear();
            shapeChooser = false;
            selectedShape = -1;
            markSelectedShape();
            markColor(pictureBox6);
            markShapeChooser();
        }

        /// <summary>
        /// Shape Operations
        /// </summary>

        private void drawShape()
        {
            //Program draws selected shape. If there isn't any selection(case -1), it won't draw anything
            switch (selectedShape)
            {
                case (int)ShapeSelector.Square:
                    {
                        Shape square = new Square(x1, y1, x2, y2, pen.Color);
                        square.shapeType = (int)ShapeSelector.Square;
                        square.Draw(g, pen);
                        Shapes.Add(square);
                    }
                    break;
                case (int)ShapeSelector.Circle:
                    {
                        Shape circle = new Circle(x1, y1, x2, y2, pen.Color);
                        circle.shapeType = (int)ShapeSelector.Circle;
                        circle.Draw(g, pen);
                        Shapes.Add(circle);
                    }
                    break;
                case (int)ShapeSelector.Triangle:
                    {
                        Shape triangle = new Triangle(x1, y1, x2, y2, pen.Color);
                        triangle.shapeType = (int)ShapeSelector.Triangle;
                        triangle.Draw(g, pen);
                        Shapes.Add(triangle);
                    }
                    break;
                case (int)ShapeSelector.Hexagon:
                    {
                        Shape hexagon = new Hexagon(x1, y1, x2, y2, pen.Color);
                        hexagon.shapeType = (int)ShapeSelector.Hexagon;
                        hexagon.Draw(g, pen);
                        Shapes.Add(hexagon);
                    }
                    break;
                case -1:
                    {

                    }
                    break;
            }

        }

        private void selectShape(int x, int y)
        {
            while (shapeChooser)
            {
                foreach (Shape shape in Shapes)
                {
                    if (x >= shape.x && x <= shape.width + shape.x && y >= shape.y && y <= shape.height + shape.y)
                    {
                        shape.DrawSelected(g);
                        shapeChooser = false;
                    }
                }
                break;
            }
        }

        /// <summary>
        /// File Operations
        /// </summary>

        private void getFileName()
        {
            using (OpenFileDialog choofdlog = new OpenFileDialog())
            {
                choofdlog.Filter = "All Files (*.*)|*.*";
                choofdlog.FilterIndex = 1;

                if (choofdlog.ShowDialog() == DialogResult.OK)
                    filePath = choofdlog.FileName;
                else
                {
                    filePath = string.Empty;
                }
            }
        }

        public void SaveToTxt(string path, List<Shape> list)
        {
            using (TextWriter tw = new StreamWriter(path))
            {
                foreach (var item in list)
                {
                    tw.WriteLine(string.Format("x {0} y {1} width {2} height {3} shapeType {4} color {5}"
                        , item.x.ToString(), item.y.ToString(), item.width.ToString(), item.height.ToString(), item.shapeType.ToString(), item.color.ToString()));
                }
            }
        }

        public void ReadToTxt(string path)
        {
            using (System.IO.StreamReader file = new System.IO.StreamReader(path))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    string[] words = line.Split(' ');
                    int tempX1 = Convert.ToInt16(words[1]);
                    int tempY1 = Convert.ToInt16(words[3]);
                    int tempX2 = Convert.ToInt16(words[1]) + Convert.ToInt16(words[5]);
                    int tempY2 = Convert.ToInt16(words[3]) + Convert.ToInt16(words[7]);
                    Color tempColor;
                    if (words[12].Contains("="))
                    {
                        words[12] = words[12].Substring(3, words[12].Length - 4);
                        words[13] = words[13].Substring(2, words[13].Length - 3);
                        words[14] = words[14].Substring(2, words[14].Length - 3);
                        words[15] = words[15].Substring(2, words[15].Length-3);
                        tempColor = Color.FromArgb(Convert.ToInt32(words[12]), Convert.ToInt32(words[13]),
                            Convert.ToInt32(words[14]), Convert.ToInt32(words[15]));
                    }
                    else
                    {
                        // remove first and last char at string "[red]" => "red" so Color.FromName("red") 
                         tempColor = Color.FromName(words[12].Substring(1, words[12].Length-2));
                    }

                    if (Convert.ToInt16(words[9]) == 0)
                    {
                        Shape shape = new Square(tempX1, tempY1, tempX2, tempY2, tempColor);
                        shape.shapeType = 0;
                        shape.Draw(g, pen);
                        Shapes.Add(shape);
                    }

                    else if (Convert.ToInt16(words[9]) == 1)
                    {
                        Shape shape = new Circle(tempX1, tempY1, tempX2, tempY2, tempColor);
                        shape.shapeType = 1;
                        shape.Draw(g, pen);
                        Shapes.Add(shape);
                    }
                    else if (Convert.ToInt16(words[9]) == 2)
                    {
                        Shape shape = new Triangle(tempX1, tempY1, tempX2, tempY2, tempColor);
                        shape.shapeType = 2;
                        shape.Draw(g, pen);
                        Shapes.Add(shape);
                    }
                    else if (Convert.ToInt16(words[9]) == 3)
                    {
                        Shape shape = new Hexagon(tempX1, tempY1, tempX2, tempY2, tempColor);
                        shape.shapeType = 3;
                        shape.Draw(g, pen);
                        Shapes.Add(shape);
                    }

                }
            }
        }

        /// <summary>
        /// Mark Operations
        /// </summary>
        private void markColor(PictureBox p)
        {
            foreach (PictureBox pb in this.groupBox1.Controls.OfType<PictureBox>())
            {
                pb.BorderStyle = BorderStyle.Fixed3D;
            }
            p.BorderStyle = BorderStyle.None;
        }

        private void markSelectedShape()
        {
            foreach (PictureBox pb in this.groupBox2.Controls.OfType<PictureBox>())
            {
                pb.BorderStyle = BorderStyle.None;
                switch (selectedShape)
                {
                    case 0:
                        {
                            squareBox.BorderStyle = BorderStyle.FixedSingle;
                        }break;
                    case 1:
                        {
                            circleBox.BorderStyle = BorderStyle.FixedSingle;
                        }
                        break;
                    case 2:
                        {
                            triangleBox.BorderStyle = BorderStyle.FixedSingle;
                        }
                        break;
                    case 3:
                        {
                            hexagonBox.BorderStyle = BorderStyle.FixedSingle;
                        }
                        break;
                }
            }
        }

        private void markShapeChooser()
        {
            SelectShapeBox.BorderStyle = shapeChooser == true ? BorderStyle.Fixed3D : BorderStyle.None;
        }

    }
}
