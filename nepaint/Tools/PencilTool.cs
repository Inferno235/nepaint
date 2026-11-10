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
    public class PencilTool : Tool
    {
        private Polyline? _currentPolyline;

        public override void OnMouseDown(Point position, Canvas canvas)
        {
            _currentPolyline = new Polyline
            {
                Stroke = new SolidColorBrush(CurrentColor),
                StrokeThickness = Thickness,
                StrokeStartLineCap = PenLineCap.Round,
                StrokeEndLineCap = PenLineCap.Round
            };
            _currentPolyline.Points.Add(position);
            canvas.Children.Add(_currentPolyline);
        }

        public override void OnMouseMove(Point position, Canvas canvas)
        {
            if (_currentPolyline != null)
            {
                _currentPolyline.Points.Add(position);
            }
        }

        public override void OnMouseUp(Point position, Canvas canvas)
        {
            _currentPolyline = null; 
        }
    }

}
