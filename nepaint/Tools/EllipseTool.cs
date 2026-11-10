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
    public class EllipseTool : Tool
    {
        private Ellipse? _currentEllipse;
        private Point _startPoint;

        public override void OnMouseDown(Point position, Canvas canvas)
        {
            _startPoint = position;
            _currentEllipse = new Ellipse
            {
                Stroke = new SolidColorBrush(CurrentColor),
                StrokeThickness = Thickness
            };
            canvas.Children.Add(_currentEllipse);
        }

        public override void OnMouseMove(Point position, Canvas canvas)
        {
            if (_currentEllipse != null)
            {
                double x = Math.Min(_startPoint.X, position.X);
                double y = Math.Min(_startPoint.Y, position.Y);
                double width = Math.Abs(position.X - _startPoint.X);
                double height = Math.Abs(position.Y - _startPoint.Y);

                Canvas.SetLeft(_currentEllipse, x);
                Canvas.SetTop(_currentEllipse, y);
                _currentEllipse.Width = width;
                _currentEllipse.Height = height;
            }
        }

        public override void OnMouseUp(Point position, Canvas canvas)
        {
            _currentEllipse = null;
        }
    }
}
