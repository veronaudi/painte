using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;

namespace Grafcreator.Shapes
{
    public class Line : ShapeBase
    {
        private float endX;
        private float endY;

        public Line(Point start, Point end)
            : this(start, end, Colors.Black, 2) { }

        public Line(Point start, Point end, Color strokeColor, int strokeWidth)
            : base(strokeColor, Colors.Transparent, strokeWidth)
        {
            endX = (float)end.X;
            endY = (float)end.Y;
            center = start;
        }

        public override void Draw(Canvas canvas)
        {
            var line = new System.Windows.Shapes.Line
            {
                X1 = center.X,
                Y1 = center.Y,
                X2 = endX,
                Y2 = endY,
                Stroke = new SolidColorBrush(colorStroke),
                StrokeThickness = strokeWidth
            };

            ShapeElement = line;
            canvas.Children.Add(line);
        }

        public override void Update(Point end)
        {
            endX = (float)end.X;
            endY = (float)end.Y;

            if (ShapeElement is System.Windows.Shapes.Line line)
            {
                line.X2 = endX;
                line.Y2 = endY;
            }
        }

        public override void Move(double dx, double dy)
        {
            center = new Point(center.X + dx, center.Y + dy);
            endX += (float)dx;
            endY += (float)dy;

            if (ShapeElement is System.Windows.Shapes.Line line)
            {
                line.X1 = center.X;
                line.Y1 = center.Y;
                line.X2 = endX;
                line.Y2 = endY;
            }
        }
    }
}
