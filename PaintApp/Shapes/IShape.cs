using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaintApp.Shapes
{
    public interface IShape
    {
        void DeleteShape(Graphics g, Pen p);
        void Draw(Graphics g, Pen p);
        void DrawSelected(Graphics g);
        void DeleteSelected(Graphics g);
    }
}
