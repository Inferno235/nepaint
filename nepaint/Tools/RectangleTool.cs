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
    public class RectangleTool : Tool
    {
        private Rectangle? _currentRectangle;
        private Point _startPoint;

        public override void OnMouseDown(Point position, Canvas canvas)
        {
            _startPoint = position;
            _currentRectangle = new Rectangle
            {
                Stroke = new SolidColorBrush(CurrentColor),
                StrokeThickness = Thickness
            };
            canvas.Children.Add(_currentRectangle);
        }

        public override void OnMouseMove(Point position, Canvas canvas)
        {
            if (_currentRectangle != null)
            {
                
                double x = Math.Min(_startPoint.X, position.X);
                double y = Math.Min(_startPoint.Y, position.Y);
                double width = Math.Abs(position.X - _startPoint.X);
                double height = Math.Abs(position.Y - _startPoint.Y);

                Canvas.SetLeft(_currentRectangle, x);
                Canvas.SetTop(_currentRectangle, y);
                _currentRectangle.Width = width;
                _currentRectangle.Height = height;
            }
        }

        public override void OnMouseUp(Point position, Canvas canvas)
        {
            _currentRectangle = null;
        }
    }
}
