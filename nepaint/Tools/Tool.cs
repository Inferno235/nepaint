using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace nepaint.Tools
{
    public abstract class Tool
    {
        public Color CurrentColor { get; set; } = Colors.Black;
        public double Thickness { get; set; } = 2.0;

        public abstract void OnMouseDown(Point position, Canvas canvas);
        public abstract void OnMouseMove(Point position, Canvas canvas);
        public abstract void OnMouseUp(Point position, Canvas canvas);
    }
}
