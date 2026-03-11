using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;

namespace Grafcreator.Shapes
{
    public class Circle : ShapeBase
    {
        private double radius;

        public Circle(Point center, double radius, Color strokeColor, Color fillColor, int strokeWidth)
            : base(strokeColor, fillColor, strokeWidth)
        {
            this.center=center;
            this.radius=radius;
        }

        public override void Draw(Canvas canvas)
        {
            var ellipse = new Ellipse
            {
                Width = radius*2,
                Height = radius*2,
                Stroke = new SolidColorBrush(colorStroke),
                Fill = new SolidColorBrush(colorFill),
                StrokeThickness = strokeWidth
            };
            Canvas.SetLeft(ellipse, center.X - radius);
            Canvas.SetTop(ellipse, center.Y - radius);
            ShapeElement = ellipse;
            canvas.Children.Add(ellipse);
        }

        public override void Update(Point current)
        {
            radius = Math.Sqrt(Math.Pow(current.X - center.X, 2) + Math.Pow(current.Y - center.Y, 2));

            if (ShapeElement is Ellipse ellipse)
            {
                ellipse.Width = radius*2;
                ellipse.Height = radius*2;
                Canvas.SetLeft(ellipse, center.X - radius);
                Canvas.SetTop(ellipse, center.Y - radius);
            }
        }
    }
}
