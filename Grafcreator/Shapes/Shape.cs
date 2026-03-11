using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;

namespace Grafcreator.Shapes
{
    public abstract class ShapeBase
    {
        protected Color colorFill;
        protected Color colorStroke;
        protected int strokeWidth;
        protected Point center;

        public bool IsSelected { get; private set; }

        public Shape ShapeElement { get; protected set; }

        public ShapeBase(Color strokeColor, Color fillColor, int strokeWidth)
        {
            this.colorStroke = strokeColor;
            this.colorFill = fillColor;
            this.strokeWidth = strokeWidth;
        }

        public virtual void Move(double dx, double dy)
        {
            if (ShapeElement == null) return;
            double left = Canvas.GetLeft(ShapeElement);
            double top = Canvas.GetTop(ShapeElement);
            Canvas.SetLeft(ShapeElement, left + dx);
            Canvas.SetTop(ShapeElement, top + dy);
        }

        public virtual void Delete(Canvas canvas)
        {
            if (ShapeElement != null)
                canvas.Children.Remove(ShapeElement);
        }

        public virtual void Color(Color stroke, Color fill)
        {
            colorStroke = stroke;
            colorFill = fill;
            if (ShapeElement != null)
            {
                ShapeElement.Stroke = new SolidColorBrush(stroke);
                ShapeElement.Fill = new SolidColorBrush(fill);
            }
        }

        public virtual void Choose()
        {
            if (ShapeElement != null)
                ShapeElement.StrokeDashArray = new DoubleCollection { 4, 2 };
            IsSelected = true;
        }

        public abstract void Draw(Canvas canvas);
        public virtual void Update(Point end)
        {
            //Реализуется в наследниках
        }
        public virtual void Unchoose()
        {
            if (ShapeElement != null)
                ShapeElement.StrokeDashArray = null;
            IsSelected = false;
        }

        public virtual bool Contains(Point p)
        {
            if (ShapeElement == null) return false;

            return ShapeElement.IsMouseOver;
        }
    }
}
