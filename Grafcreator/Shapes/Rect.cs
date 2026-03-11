using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;

namespace Grafcreator.Shapes
{
    public class Rect: ShapeBase
    {
        private double width;
        private double height;

        public Rect(Point topLeft, double width, double height)
            : base(Colors.Black, Colors.Transparent, 2)
        {
            center = topLeft;
            this.width = width;
            this.height = height;
        }

        public override void Draw(Canvas canvas)
        {
            var rect = new Rectangle
            {
                Width = width,
                Height = height,
                Stroke = new SolidColorBrush(colorStroke),
                Fill = new SolidColorBrush(colorFill),
                StrokeThickness = strokeWidth
            };

            Canvas.SetLeft(rect, center.X);
            Canvas.SetTop(rect, center.Y);
            ShapeElement = rect;
            canvas.Children.Add(rect);
        }

        public override void Update(Point end)
        {
            if (ShapeElement is Rectangle rect)
            {
                double x = Math.Min(center.X, end.X);
                double y = Math.Min(center.Y, end.Y);
                width = Math.Abs(end.X - center.X);
                height = Math.Abs(end.Y - center.Y);

                rect.Width = width;
                rect.Height = height;
                Canvas.SetLeft(rect, x);
                Canvas.SetTop(rect, y);
            }
        }

    }
}
