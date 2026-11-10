using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace nepaint.Tools
{
    public class LineTool : Tool
    {
        private Line? _currentLine;
        private Point _startPoint;

        public override void OnMouseDown(Point position, Canvas canvas)
        {
            _startPoint = position;
            _currentLine = new Line
            {
                Stroke = new SolidColorBrush(CurrentColor),
                StrokeThickness = Thickness,
                X1 = position.X,
                Y1 = position.Y,
                X2 = position.X,
                Y2 = position.Y
            };
            canvas.Children.Add(_currentLine);
        }

        public override void OnMouseMove(Point position, Canvas canvas)
        {
            if (_currentLine != null)
            {
               
                _currentLine.X2 = position.X;
                _currentLine.Y2 = position.Y;
            }
        }

        public override void OnMouseUp(Point position, Canvas canvas)
        {
            _currentLine = null;
        }
    }
}
