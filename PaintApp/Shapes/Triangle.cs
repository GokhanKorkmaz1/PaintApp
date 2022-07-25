using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaintApp.Shapes
{
    public class Triangle:Shape
    {
        public Triangle(int x1, int y1, int x2, int y2, Color color):base(x1, y1, x2, y2, color)
        {
        }

        public override void DeleteShape(Graphics g, Pen p)
        {
            Point top = new Point((2 * x + width) / 2, y);
            Point left = new Point(x, height + y);
            Point right = new Point(x + width, height + y);

            //draw triangle
            g.FillPolygon(new SolidBrush(Color.White), new Point[] { top, left, right });
        }

        public override void Draw(Graphics g, Pen p)
        {
            p.Color = color;

            // set triangle top, left and right points
            Point top = new Point((2 * x + width) / 2, y);
            Point left = new Point( x , height+y);
            Point right = new Point( x + width , height + y);

            //draw triangle
            g.FillPolygon(new SolidBrush(color), new Point[] { top, left, right});
        }
    }
}
