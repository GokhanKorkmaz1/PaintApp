using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaintApp.Shapes
{
    public class Hexagon : Shape
    {
        public Hexagon(int x1, int y1, int x2, int y2, Color color) : base(x1, y1, x2, y2, color)
        {
        }

        public override void DeleteShape(Graphics g, Pen p)
        {
            Point TopLeft = new Point(width / 3 + x, y);
            Point TopRight = new Point(2 * width / 3 + x, y);
            Point MidLeft = new Point(x, (height + 2 * y) / 2);
            Point MidRight = new Point(x + width, (height + 2 * y) / 2);
            Point BotLeft = new Point(width / 3 + x, y + height);
            Point BotRight = new Point(2 * width / 3 + x, y + height);

            g.DrawLine(p, TopLeft, TopRight);
            g.DrawLine(p, TopRight, MidRight);
            g.DrawLine(p, MidRight, BotRight);
            g.DrawLine(p, BotRight, BotLeft);
            g.DrawLine(p, BotLeft, MidLeft);
            g.DrawLine(p, MidLeft, TopLeft);
            

            g.FillPolygon(new SolidBrush(Color.White), new Point[] { TopLeft, TopRight, MidLeft, MidRight, BotLeft, BotRight});
        }

        public override void Draw(Graphics g, Pen p)
        {
            p.Color = color;
            Point TopLeft = new Point(width/3+x, y);
            Point TopRight = new Point(2*width/3+x, y);
            Point MidLeft = new Point(x, (height + 2 * y)/2);
            Point MidRight = new Point(x+width, (height + 2 * y) / 2);
            Point BotLeft = new Point(width/3+x, y+height);
            Point BotRight = new Point(2*width/3+x, y+height);

            g.DrawLine(p, TopLeft, TopRight);
            g.DrawLine(p, TopRight, MidRight);
            g.DrawLine(p, MidRight, BotRight);
            g.DrawLine(p, BotRight, BotLeft);
            g.DrawLine(p, BotLeft, MidLeft);
            g.DrawLine(p, MidLeft, TopLeft);
            
            g.FillPolygon(new SolidBrush(color), new Point[] { TopLeft, TopRight, MidLeft, MidRight, BotLeft, BotRight});
        }
    }
}
