using System;
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
            MessageBox.Show(shapeType);

        }

        private void myCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                dest = e.GetPosition(myCanvas);
                DisplayStatus();
                switch (shapeType)
                {
                    case "line":
                        var line = myCanvas.Children.OfType<Line>().LastOrDefault();
                        line.X2 = dest.X;
                        line.Y2 = dest.Y;
                        break;
                    case "retangle":
                        var rectangle = myCanvas.Children.OfType<Rectangle>().LastOrDefault();
                        //rectangle.Stroke = 
                        break;
                    case "ellipse":
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
                case "retangle":
                    Rectangle rectangle = new Rectangle();
                    myCanvas.Children.Add(rectangle);

                    break;
                case "ellipse":
                    break;
            }
        }
        private void myCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            switch (shapeType)
            {
                case "line":
                    var line = myCanvas.Children.OfType<Line>().LastOrDefault();
                    line.Stroke = new SolidColorBrush(strokecolor);
                    line.StrokeThickness = strokeThickness;
                    line.X2 = dest.X;
                    line.Y2 = dest.Y;
                    break;
                case "retangle":
                    break;
                case "ellipse":
                    break;
            }
        }

        private void DisplayStatus()
        {
            coordinateLabel.Content = $"座標點:({Math.Round(start.X)},{Math.Round(start.Y)})-({Math.Round(dest.X)},{Math.Round(dest.Y)})";
            shapeLabel.Content = $"{myCanvas.Children.OfType<Line>().Count()}";
        }

        private void strokeColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            strokecolor = (Color)strokeColorPicker.SelectedColor;
        }

        private void strokeThicknessSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            strokeThickness = Convert.ToInt32(strokeThicknessSlider.Value);
        }

        private void fillColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            fillcolor = (Color)fillColorPicker.SelectedColor;
        }

    }
}
