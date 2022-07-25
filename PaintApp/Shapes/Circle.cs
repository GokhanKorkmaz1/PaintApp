using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaintApp.Shapes
{
    public class Circle : Shape
    {
        public Circle(int x1, int y1, int x2, int y2, Color color) : base(x1, y1, x2, y2, color)
        {
        }

        public override void DeleteShape(Graphics g, Pen p)
        {
            Rectangle rect = new Rectangle(x, y, width, height);
            g.FillEllipse(new SolidBrush(Color.White), rect);
        }

        public override void Draw(Graphics g, Pen p)
        {
            p.Color = color;
            Rectangle rect = new Rectangle(x, y, width, height);
            g.FillEllipse(new SolidBrush(color), rect);
        }
    }
}
