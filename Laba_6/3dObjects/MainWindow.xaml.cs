using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace _3dObjects
{
    public partial class MainWindow : Window
    {
        private Dictionary<string, _3dObject> Objects;
        private string View;
        private _3dObject CurrentObject;
        private int CurrentTab;
        private Action[] UpdateTab;
        private bool Frontally;
        private bool Back;
        private double Rotate;
        private Dictionary<string, double> Offset;

        public MainWindow()
        {
            Objects = new Dictionary<string, _3dObject>();
            Offset = new Dictionary<string, double>();
            Offset.Add("X", 0);
            Offset.Add("Y", 0);
            Offset.Add("Z", 0);
            BuildObjects(8);
            CurrentObject = Objects["Окружность"];
            View = "Прямоугольная диметрия";
            UpdateTab = new Action[3];
            UpdateTab[0] = UpdateTab_1;
            UpdateTab[1] = UpdateTab_2;
            UpdateTab[2] = UpdateTab_3;
            Frontally = false;
            Back = false;
            InitializeComponent();
        }

        private void UpdateCanvas()
        {
            if (Canvas == null) return;
            Canvas.Children.Clear();
            UpdateTab[CurrentTab]();
        }
        private void UpdateTab_1()
        {
            // Upper
            AxesCoordinates upperAxes = new AxesCoordinates();
            upperAxes.Back = Back;
            upperAxes.PositionCenter = new Vector2d(Canvas.ActualWidth / 2, Canvas.ActualHeight / 2 - Canvas.ActualHeight * 0.15f);
            upperAxes.Width = Canvas.ActualWidth * 0.4f;
            upperAxes.Height = Canvas.ActualHeight * 0.4f;
            CurrentObject.Reset();
            upperAxes.PointView = new Vector3d(0, 100, 0);
            if (Frontally)
            {
                CurrentObject.ToRotateX(-90);
                CurrentObject.ToRotateZ(180);
            }
            else CurrentObject.ToReflectX();
            CurrentObject.ToRotateZ(-Rotate);

            upperAxes.Object = CurrentObject;


            List<Line> lines = upperAxes.GetLinesAxes();
            foreach (Line line in lines) Canvas.Children.Add(line);

            lines = upperAxes.GetLinesObject();
            foreach (Line line in lines) Canvas.Children.Add(line);

            // Lower
            AxesCoordinates lowerAxes = new AxesCoordinates();
            lowerAxes.Back = Back;
            lowerAxes.PositionCenter = new Vector2d(Canvas.ActualWidth / 2, Canvas.ActualHeight / 2 + Canvas.ActualHeight * 0.15f);
            lowerAxes.Width = Canvas.ActualWidth * 0.4f;
            lowerAxes.Height = Canvas.ActualHeight * 0.4f;
            CurrentObject.Reset();
            CurrentObject.ToReflectX();
            if (Frontally) CurrentObject.ToRotateX(90);
            CurrentObject.ToRotateZ(-Rotate);
            lowerAxes.Object = CurrentObject;

            lowerAxes.Axes["Z"] = new Vector3d(0, 1, 0);
            lowerAxes.Axes["Y"] = new Vector3d(0, 0, -1);

            lines = lowerAxes.GetLinesAxes();
            foreach (Line line in lines) Canvas.Children.Add(line);

            lines = lowerAxes.GetLinesObject();
            foreach (Line line in lines) Canvas.Children.Add(line);
        }
        private void UpdateTab_2()
        {
            AxesCoordinates axes = new AxesCoordinates();
            axes.Back = Back;
            axes.PositionCenter = new Vector2d(Canvas.ActualWidth / 2, Canvas.ActualHeight / 2);
            axes.Width = Canvas.ActualWidth * 0.6f;
            axes.Height = Canvas.ActualHeight * 0.6f;
            CurrentObject.Reset();
            CurrentObject.ToReflectX();
            axes.Object = CurrentObject;

            List<Line> lines;
            switch (View)
            {
                case "Прямоугольная изометрия":
                    if (Frontally)
                    {
                        CurrentObject.ToRotateY(Rotate);
                        axes.PointView = new Vector3d(1, 1, 1);
                        axes.Axes["X"] = new Vector3d(Math.Cos(7 * Math.PI / 6f), 0, Math.Sin(7 * Math.PI / 6f));
                        axes.Axes["Z"] = new Vector3d(Math.Cos(-Math.PI / 6f), 0, Math.Sin(-Math.PI / 6f));
                        axes.Axes["Y"] = new Vector3d(0, 0, 1);
                    }
                    else
                    {
                        CurrentObject.ToRotateZ(-Rotate);
                        axes.PointView = new Vector3d(1, 1, 1);
                        axes.Axes["X"] = new Vector3d(Math.Cos(7 * Math.PI / 6f), 0, Math.Sin(7 * Math.PI / 6f));
                        axes.Axes["Y"] = new Vector3d(Math.Cos(-Math.PI / 6f), 0, Math.Sin(-Math.PI / 6f));
                        axes.Axes["Z"] = new Vector3d(0, 0, 1);
                    }
                    break;
                case "Прямоугольная диметрия":
                    if (Frontally)
                    {
                        axes.K["Z"] = 0.5f;
                        CurrentObject.ToRotateY(Rotate);
                        axes.PointView = new Vector3d(0.376636982713463f, 0.360921948664222f, 1);
                        axes.Axes["X"] = new Vector3d(Math.Cos(187 * Math.PI / 180f), 0, Math.Sin(187 * Math.PI / 180f));
                        axes.Axes["Z"] = new Vector3d(Math.Cos(-41 * Math.PI / 180f), 0, Math.Sin(-41 * Math.PI / 180f));
                        axes.Axes["Y"] = new Vector3d(0, 0, 1);
                    }
                    else
                    {
                        axes.K["Y"] = 0.5f;
                        CurrentObject.ToRotateZ(-Rotate);
                        axes.PointView = new Vector3d(0.376636982713463f, 1, 1);
                        axes.Axes["X"] = new Vector3d(Math.Cos(187 * Math.PI / 180f), 0, Math.Sin(187 * Math.PI / 180f));
                        axes.Axes["Y"] = new Vector3d(Math.Cos(-41 * Math.PI / 180f), 0, Math.Sin(-41 * Math.PI / 180f));
                        axes.Axes["Z"] = new Vector3d(0, 0, 1);
                    }
                    break;
                case "Фронтальная изометрия":
                    if (Frontally)
                    {
                        CurrentObject.ToRotateY(Rotate);
                        axes.PointView = new Vector3d(0.701414353064432f, 0.680460974332111f, 1);
                        axes.Axes["X"] = new Vector3d(-1, 0, 0);
                        axes.Axes["Z"] = new Vector3d(Math.Cos(-45 * Math.PI / 180f), 0, Math.Sin(-45 * Math.PI / 180f));
                        axes.Axes["Y"] = new Vector3d(0, 0, 1);
                    }
                    else
                    {
                        CurrentObject.ToRotateZ(-Rotate);
                        axes.PointView = new Vector3d(0.701414353064432f, 1, 1);
                        axes.Axes["X"] = new Vector3d(-1, 0, 0);
                        axes.Axes["Y"] = new Vector3d(Math.Cos(-45 * Math.PI / 180f), 0, Math.Sin(-45 * Math.PI / 180f));
                        axes.Axes["Z"] = new Vector3d(0, 0, 1);
                    }
                    break;
                case "Фронтальная диметрия":
                    if (Frontally)
                    {
                        axes.K["Z"] = 0.5f;
                        CurrentObject.ToRotateY(Rotate);
                        axes.PointView = new Vector3d(0.355683603981142f, 0.342587742273442f, 1);
                        axes.Axes["X"] = new Vector3d(-1, 0, 0);
                        axes.Axes["Z"] = new Vector3d(Math.Cos(-45 * Math.PI / 180f), 0, Math.Sin(-45 * Math.PI / 180f));
                        axes.Axes["Y"] = new Vector3d(0, 0, 1);
                    }
                    else
                    {
                        axes.K["Y"] = 0.5f;
                        CurrentObject.ToRotateZ(-Rotate);
                        axes.PointView = new Vector3d(0.355683603981142f, 1, 1);
                        axes.Axes["X"] = new Vector3d(-1, 0, 0);
                        axes.Axes["Y"] = new Vector3d(Math.Cos(-45 * Math.PI / 180f), 0, Math.Sin(-45 * Math.PI / 180f));
                        axes.Axes["Z"] = new Vector3d(0, 0, 1);
                    }
                    break;
                case "Горизонтальная изометрия":
                    if (Frontally)
                    {
                        CurrentObject.ToRotateY(Rotate);
                        axes.PointView = new Vector3d(1.69669984284966f, 1.95599790466213f, 1);
                        axes.Axes["X"] = new Vector3d(Math.Cos(240 * Math.PI / 180f), 0, Math.Sin(240 * Math.PI / 180f));
                        axes.Axes["Z"] = new Vector3d(Math.Cos(-30 * Math.PI / 180f), 0, Math.Sin(-30 * Math.PI / 180f));
                        axes.Axes["Y"] = new Vector3d(0, 0, 1);
                    }
                    else
                    {
                        CurrentObject.ToRotateZ(-Rotate);
                        axes.PointView = new Vector3d(1.69669984284966f, 1, 1);
                        axes.Axes["X"] = new Vector3d(Math.Cos(240 * Math.PI / 180f), 0, Math.Sin(240 * Math.PI / 180f));
                        axes.Axes["Y"] = new Vector3d(Math.Cos(-30 * Math.PI / 180f), 0, Math.Sin(-30 * Math.PI / 180f));
                        axes.Axes["Z"] = new Vector3d(0, 0, 1);
                    }
                    break;
            }
            lines = axes.GetLinesAxes();
            foreach (Line line in lines) Canvas.Children.Add(line);
            lines = axes.GetLinesObject();
            foreach (Line line in lines) Canvas.Children.Add(line);
        }
        private void UpdateTab_3()
        {
            AxesWithHorizon axes = new AxesWithHorizon(Canvas.ActualWidth, Canvas.ActualHeight);
            axes.Back = Back;
            axes.PositionCenter = new Vector2d(Canvas.ActualWidth / 2, Canvas.ActualHeight / 2);
            axes.Width = Canvas.ActualWidth * 0.5f;
            axes.Height = Canvas.ActualHeight * 0.5f;
            CurrentObject.Reset();
            if (Frontally)
            {
                CurrentObject.ToReflectY();
                CurrentObject.ToRotateX(90);
            }
            CurrentObject.ToRotateZ(Rotate);
            axes.Object = CurrentObject;
            axes.ToShiftX(-4 * Offset["X"]);
            axes.ToShiftY(300 * Offset["Z"]);
            axes.ToShiftZ(Canvas.ActualHeight / 10 / 15 * Offset["Y"]);


            Canvas.Children.Add(axes.Horizon.Line);
            List<Line> lines = axes.GetLinesAxes();
            foreach (Line line in lines) Canvas.Children.Add(line);
            lines = axes.GetLinesObject();
            foreach (Line line in lines) Canvas.Children.Add(line);
        }
        private void BuildObjects(int numSegments)
        {
            string CurrentObjectKey = Objects.FirstOrDefault(x => x.Value == CurrentObject).Key;
            Objects.Clear();
            Objects.Add("Окружность", new Сircle(numSegments));
            Objects.Add("Дуга параболы", new ParabolicArc(numSegments));
            Objects.Add("Логарифмическая спираль", new LogarithmicSpiral(numSegments));
            if (CurrentObjectKey != null)
                CurrentObject = Objects[CurrentObjectKey];
        }
        private void Canvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateCanvas();
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            ComboBoxItem comboBoxItem = comboBox.SelectedItem as ComboBoxItem;
            TextBlock textBlock = comboBoxItem.Content as TextBlock;
            if (textBlock != null) BuildObjects(Convert.ToInt32(textBlock.Text));
            UpdateCanvas();
        }
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Rotate = e.NewValue;
            UpdateCanvas();
        }
        private void Slider_ValueChanged_X(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Offset["X"] = e.NewValue;
            UpdateCanvas();
        }
        private void Slider_ValueChanged_Y(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Offset["Y"] = e.NewValue;
            UpdateCanvas();
        }
        private void Slider_ValueChanged_Z(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Offset["Z"] = e.NewValue;
            UpdateCanvas();
        }
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabControl tabControl = sender as TabControl;
            CurrentTab = tabControl.SelectedIndex;
            UpdateCanvas();
        }
        private void RadioButton_Checked_Type3dObject(object sender, RoutedEventArgs e)
        {
            RadioButton pressed = sender as RadioButton;
            if (pressed.Content != null)
            {
                CurrentObject = Objects[pressed.Content.ToString()];
            }
            UpdateCanvas();
        }
        private void CheckBox_Touch(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            Frontally = checkBox.IsChecked.Value;
            UpdateCanvas();
        }
        private void ComboBox_SelectionChanged_View(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            ComboBoxItem comboBoxItem = comboBox.SelectedItem as ComboBoxItem;
            TextBlock textBlock = comboBoxItem.Content as TextBlock;
            if (textBlock != null) View = textBlock.Text;
            UpdateCanvas();
        }
        private void CheckBox_Back(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            Back = checkBox.IsChecked.Value;
            UpdateCanvas();
        }
    }
}