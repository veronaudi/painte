using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;

namespace Grafcreator.Shapes
{
    public class Triangle : ShapeBase
    {
        private float x2, y2, x3, y3;

        public Triangle(Point p1, Point p2, Point p3, Color strokeColor, Color fillColor, int strokeWidth)
            : base(strokeColor, fillColor, strokeWidth)
        {
            center = p1;
            x2 = (float)p2.X;
            y2 = (float)p2.Y;
            x3 = (float)p3.X;
            y3 = (float)p3.Y;
        }

        public override void Draw(Canvas canvas)
        {
            var triangle = new Polygon
            {
                Points = new PointCollection
                {
                    center,
                    new Point(x2, y2),
                    new Point(x3, y3)
                },
                Stroke = new SolidColorBrush(colorStroke),
                Fill = new SolidColorBrush(colorFill),
                StrokeThickness = strokeWidth
            };
            ShapeElement = triangle;
            canvas.Children.Add(triangle);
        }

        public override void Update(Point current)
        {
            x2 = (float)current.X;
            y2 = (float)current.Y;
            x3 = (float)(2 * center.X - current.X);
            y3 = (float)current.Y;

            if (ShapeElement is Polygon triangle)
            {
                triangle.Points.Clear();
                triangle.Points.Add(center);
                triangle.Points.Add(new Point(x2, y2));
                triangle.Points.Add(new Point(x3, y3));
            }
        }
        public override void Move(double dx, double dy)
        {
            center = new Point(center.X + dx, center.Y + dy);
            x2 += (float)dx;
            y2 += (float)dy;
            x3 += (float)dx;
            y3 += (float)dy;

            if (ShapeElement is Polygon triangle)
            {
                triangle.Points.Clear();
                triangle.Points.Add(center);
                triangle.Points.Add(new Point(x2, y2));
                triangle.Points.Add(new Point(x3, y3));
            }
        }
    }
}
