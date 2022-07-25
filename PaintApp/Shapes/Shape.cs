using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaintApp.Shapes
{
    public abstract class Shape : IShape
    {
        private int _x;
        public int x {
            get { return _x; } 
            set { _x = value; } 
        }

        private int _y;
        public int y {
            get { return _y; } 
            set { _y = value; } 
        }

        private int _width;
        public int width { 
            get { return _width; } 
            set { _width = value; } 
        }

        private int _height;
        public int height {
            get { return _height; } 
            set { _height = value; } 
        }

        private Color _color;
        public Color color { 
            get { return _color; }
            set { _color = value; } 
        }

        private bool _selected=false;
        public bool selected{
            get { return _selected; }
            set { _selected = value; }
        }

        private int _shapeType;
        public int shapeType{ 
            get { return _shapeType; } 
            set { _shapeType = value; } 
        }

        public Shape(int x1, int y1, int x2, int y2, Color colour)
        {
            _x = x1;
            _y = y1;
            _width = x2 - x1;
            _height = y2 - y1;
            _color = colour;
        }
        public abstract void DeleteShape(Graphics g, Pen p);

        public abstract void Draw(Graphics g, Pen p);

        public void DrawSelected(Graphics g)
        {
            // This method paints an area around the applied shape transparent brown

            Rectangle rect = new Rectangle(x - 5, y - 5, width + 10, height + 10);
            if (!selected)
            {
                g.FillRectangle(new SolidBrush(Color.FromArgb(100, 165, 42, 42)), rect);
                selected = !selected;
            }
            else
            {
                g.FillRectangle(new SolidBrush(Color.FromArgb(100, 165, 42, 42)), rect);
                selected = !selected;

            }
        }

        public void DeleteSelected(Graphics g)
        {
            
            Rectangle rect = new Rectangle(x - 5, y - 5, width + 10, height + 10);
            g.FillRectangle(new SolidBrush(Color.White), rect);
        }
    }
}
