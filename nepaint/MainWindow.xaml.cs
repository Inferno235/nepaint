using nepaint.Tools;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace nepaint
{
    public partial class MainWindow : Window
    {
        
        private Tool _currentTool;
        private bool _isDrawing;

        
        private readonly Tool _pencil = new PencilTool();
        private readonly Tool _line = new LineTool();
        private readonly Tool _rectangle = new RectangleTool();
        private readonly Tool _ellipse = new EllipseTool();

        public MainWindow()
        {
            InitializeComponent();
            UpdateActiveTool();
        }

        
        private void DrawingCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                _isDrawing = true;
                System.Windows.Point pos = e.GetPosition(DrawingCanvas);

                
                _currentTool.CurrentColor = GetSelectedColor();
                _currentTool.Thickness = SliderThickness.Value;

                _currentTool.OnMouseDown(pos, DrawingCanvas);
                DrawingCanvas.CaptureMouse();
            }
        }

        private void DrawingCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDrawing)
            {
                System.Windows.Point pos = e.GetPosition(DrawingCanvas);
                _currentTool.OnMouseMove(pos, DrawingCanvas);
            }
        }

        private void DrawingCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (_isDrawing)
            {
                System.Windows.Point pos = e.GetPosition(DrawingCanvas);
                _currentTool.OnMouseUp(pos, DrawingCanvas);
                _isDrawing = false;
                DrawingCanvas.ReleaseMouseCapture();
            }
        }

        
        private void Tool_Checked(object sender, RoutedEventArgs e)
        {
            UpdateActiveTool();
        }

        private void UpdateActiveTool()
        {
            if (RbPencil == null) return; 

            if (RbPencil.IsChecked == true) _currentTool = _pencil;
            else if (RbLine.IsChecked == true) _currentTool = _line;
            else if (RbRectangle.IsChecked == true) _currentTool = _rectangle;
            else if (RbEllipse.IsChecked == true) _currentTool = _ellipse;
        }

        
        private System.Windows.Media.Color GetSelectedColor()
        {
            if (CbColor?.SelectedItem is ComboBoxItem item && item.Tag != null)
            {
                string colorStr = item.Tag.ToString();
                return (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(colorStr);
            }
            return System.Windows.Media.Colors.Black;
        }

        private void SliderThickness_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_currentTool != null) _currentTool.Thickness = e.NewValue;
        }

        private void CbColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_currentTool != null) _currentTool.CurrentColor = GetSelectedColor();
        }

        
        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            DrawingCanvas.Children.Clear();
        }

        
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PNG Image (*.png)|*.png|JPEG Image (*.jpg)|*.jpg",
                DefaultExt = "png",
                FileName = "Drawing"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    
                    System.Windows.Size size = new System.Windows.Size(DrawingCanvas.ActualWidth, DrawingCanvas.ActualHeight);

                    
                    DrawingCanvas.Measure(size);
                    DrawingCanvas.Arrange(new Rect(size));

                    RenderTargetBitmap renderBitmap = new RenderTargetBitmap(
                        (int)size.Width, (int)size.Height, 96d, 96d, PixelFormats.Pbgra32);

                    renderBitmap.Render(DrawingCanvas);

                    BitmapEncoder encoder;
                    if (System.IO.Path.GetExtension(saveFileDialog.FileName).ToLower() == ".jpg")
                    {
                        encoder = new JpegBitmapEncoder();
                    }
                    else
                    {
                        encoder = new PngBitmapEncoder();
                    }

                    encoder.Frames.Add(BitmapFrame.Create(renderBitmap));

                    using (FileStream fileStream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                    {
                        encoder.Save(fileStream);
                    }

                    MessageBox.Show("Изображение успешно сохранено!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
