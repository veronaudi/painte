using Grafcreator.Shapes;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Grafcreator
{
    public partial class MainWindow : Window
    {
        private bool isDrawing = false;
        private Point startPoint;
        private ShapeBase currentShape = null;
        private List<ShapeBase> shapes = new List<ShapeBase>();
        private string selectedTool = "Line";

        private ShapeBase selectedShape = null;
        private bool isDraggingShape = false;
        private Point lastMousePos;

        private Color currentStroke = Colors.Black;
        private Color currentFill = Colors.Transparent;

        private enum EditorMode
        {
            Draw,
            Select
        }
        private EditorMode currentMode = EditorMode.Draw;

        public MainWindow()
        {
            InitializeComponent();

            Chose.Click += (s, e) =>
            {
                currentMode = EditorMode.Select;
            };

            LineTool.Click += (s, e) =>
            {
                currentMode = EditorMode.Draw;
                selectedTool = "Line";
            };

            RectTool.Click += (s, e) =>
            {
                currentMode = EditorMode.Draw;
                selectedTool = "Rectangle";
            };

            CircleTool.Click += (s, e) =>
            {
                currentMode = EditorMode.Draw;
                selectedTool = "Circle";
            };

            TriangleTool.Click += (s, e) =>
            {
                currentMode = EditorMode.Draw;
                selectedTool = "Triangle";
            };

            DrawCanvas.MouseDown += Canvas_MouseDown;
            DrawCanvas.MouseMove += Canvas_MouseMove;
            DrawCanvas.MouseUp += Canvas_MouseUp;
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point pos = e.GetPosition(DrawCanvas);
            if (currentMode == EditorMode.Select)
            {
                foreach (var shape in shapes)
                {
                    if (shape.Contains(pos))
                    {
                        SelectShape(shape);
                        isDraggingShape = true;
                        lastMousePos = pos;
                        return;
                    }
                }

                DeselectShape();
                return;
            }

            isDrawing = true;
            startPoint = pos;

            switch (selectedTool)
            {
                case "Line":
                    currentShape = new Grafcreator.Shapes.Line(startPoint, startPoint);
                    break;
                case "Rectangle":
                    currentShape = new Grafcreator.Shapes.Rect(startPoint, 0, 0);
                    break;
                case "Circle":
                    currentShape = new Grafcreator.Shapes.Circle(startPoint, 0, Colors.Black, Colors.Transparent,2);
                    break;
                case "Triangle":
                    currentShape = new Grafcreator.Shapes.Triangle(startPoint, startPoint, startPoint, Colors.Black, Colors.Transparent, 2);
                    break;
            }

            if (currentShape != null)
            {

                currentShape.Color(currentStroke, currentFill);
                currentShape.Draw(DrawCanvas);
            }
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            Point pos = e.GetPosition(DrawCanvas);

            // Перемещение фигуры
            if (currentMode == EditorMode.Select &&
                isDraggingShape &&
                selectedShape != null)
            {
                double dx = pos.X - lastMousePos.X;
                double dy = pos.Y - lastMousePos.Y;

                selectedShape.Move(dx, dy);
                lastMousePos = pos;
                return;
            }

            // Рисование фигуры
            if (currentMode == EditorMode.Draw &&
                isDrawing &&
                currentShape!=null)
            {
                currentShape.Update(pos);
            }
        }

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (currentMode == EditorMode.Select)
            {
                isDraggingShape = false;
                return;
            }

            if (isDrawing && currentShape != null)
            {
                isDrawing = false;
                shapes.Add(currentShape);
                currentShape = null;
            }
        }

        private void SelectShape(ShapeBase shape)
        {
            DeselectShape();
            selectedShape = shape;
            selectedShape.Choose();
        }

        private void DeselectShape()
        {
            if (selectedShape != null)
                selectedShape.Unchoose();

            selectedShape = null;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (selectedShape == null)
                return;

            selectedShape.Delete(DrawCanvas);
            shapes.Remove(selectedShape);
            selectedShape = null;
        }
        private void Color_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Background is SolidColorBrush brush)
            {
                currentFill = brush.Color;
            }
        }
    }
}
