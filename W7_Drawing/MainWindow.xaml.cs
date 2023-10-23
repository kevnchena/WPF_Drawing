﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace W7_Drawing
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string shapeType = "line";
        Color strokecolor = Colors.Red; //Brush 是更多東西  Color在它之下
        Color fillcolor = Colors.Yellow;
        Brush strokeBrush;
        int strokeThickness = 1; //預設筆刷大小
        Point start, dest;
        public MainWindow()
        {
            InitializeComponent();
            strokeColorPicker.SelectedColor = strokecolor; //預設顏色
            fillColorPicker.SelectedColor = fillcolor;
        }

        private void ShapeButton_Click(object sender, RoutedEventArgs e)
        {
            var targetRadioBotton = sender as RadioButton;
            shapeType = targetRadioBotton.Tag.ToString();

        }

        private void myCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                dest = e.GetPosition(myCanvas);
                Point origin;
                origin.X = Math.Min(start.X, dest.X);
                origin.Y = Math.Min(start.Y, dest.Y);
                DisplayStatus();

                switch (shapeType)
                {
                    case "line":
                        var line = myCanvas.Children.OfType<Line>().LastOrDefault();
                        line.X2 = dest.X;
                        line.Y2 = dest.Y;
                        break;
                    case "rectangle":
                        var rec = myCanvas.Children.OfType<Rectangle>().LastOrDefault();
                        rec.Width =Math.Abs(start.X - dest.X);
                        rec.Height= Math.Abs(start.Y - dest.Y);
                        rec.SetValue(Canvas.LeftProperty,origin.X);
                        rec.SetValue(Canvas.TopProperty,origin.Y);
                        break;
                    case "ellipse":
                        var ell = myCanvas.Children.OfType<Ellipse>().LastOrDefault();
                        ell.Width = Math.Abs(start.X - dest.X);
                        ell.Height = Math.Abs(start.Y - dest.Y);
                        ell.SetValue(Canvas.LeftProperty, origin.X);
                        ell.SetValue(Canvas.TopProperty, origin.Y);
                        break;
                }
            }
        }

        private void myCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            start = e.GetPosition(myCanvas);
            DisplayStatus();

            myCanvas.Cursor = Cursors.Cross;
            switch (shapeType)
            {
                case "line":
                    Line line = new Line()
                    {
                        X1 = start.X,
                        Y1 = start.Y,
                        X2 = dest.X,
                        Y2 = dest.Y,
                        StrokeThickness = 1,
                        Stroke = Brushes.Gray
                    };
                    myCanvas.Children.Add(line);
                    break;
                case "rectangle":
                    Rectangle rectangle = new Rectangle
                    {
                        Stroke= Brushes.Gray,
                        Fill=Brushes.Yellow
                    };
                    myCanvas.Children.Add(rectangle);
                    rectangle.SetValue (Canvas.LeftProperty,start.X);
                    rectangle.SetValue (Canvas.TopProperty,start.Y);
                    break;
                case "ellipse":
                    Ellipse ellipse = new Ellipse
                    {
                        Stroke = Brushes.Gray,
                        Fill = Brushes.Yellow
                    };
                    myCanvas.Children.Add(ellipse);
                    ellipse.SetValue(Canvas.LeftProperty, start.X);
                    ellipse.SetValue(Canvas.TopProperty, start.Y);
                    break;
            }
        }
        private void myCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Brush strokeBrush = new SolidColorBrush(strokecolor);
            Brush fillBrush = new SolidColorBrush(fillcolor);
            switch (shapeType)
            {
                case "line":
                    var line = myCanvas.Children.OfType<Line>().LastOrDefault();
                    line.Stroke = strokeBrush;
                    line.StrokeThickness = strokeThickness;
                    line.X2 = dest.X;
                    line.Y2 = dest.Y;
                    break;
                case "rectangle":
                    var rec = myCanvas.Children.OfType<Rectangle>().LastOrDefault();
                    rec.Stroke = strokeBrush;
                    rec.Fill = fillBrush;
                    rec.StrokeThickness = strokeThickness;
                    break;
                case "ellipse":
                    var ell = myCanvas.Children.OfType<Ellipse>().LastOrDefault();
                    ell.Stroke = strokeBrush;
                    ell.Fill = fillBrush;
                    ell.StrokeThickness = strokeThickness;
                    break;
            }
        }

        private void DisplayStatus()
        {
            coordinateLabel.Content = $"座標點:({Math.Round(start.X)},{Math.Round(start.Y)})-({Math.Round(dest.X)},{Math.Round(dest.Y)})";
            int lineCount = myCanvas.Children.OfType<Line>().Count();
            int rectCount = myCanvas.Children.OfType<Rectangle>().Count();
            int ellipseCount = myCanvas.Children.OfType<Ellipse>().Count();
            shapeLabel.Content = $"Line:{lineCount} Rect:{rectCount} Ellipse:{ellipseCount}";
        }

        private void strokeColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            strokecolor = (Color)strokeColorPicker.SelectedColor;
        }

        private void strokeThicknessSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            strokeThickness = Convert.ToInt32(strokeThicknessSlider.Value);
        }

        private void clear(object sender, RoutedEventArgs e)
        {
            myCanvas.Children.Clear();
        }

        private void fillColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            fillcolor = (Color)fillColorPicker.SelectedColor;
        }

    }
}
