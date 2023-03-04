using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using System.Collections.Generic;

namespace TrafficLights.Controls
{
    public partial class Svetofor : UserControl
    {

        /// <summary>
        /// �����������, �� ������� ���������� ������ �����
        /// </summary>
        private const double LightRadiusFactor = 0.9;

        private double _scaling;

        private int _width;
        private int _height;

        public Svetofor()
        {
            InitializeComponent();

            // ������������� �� ��������� ������� ����
            PropertyChanged += OnPropertyChangedListener;
        }

        /// <summary>
        /// ��������� ��������� ������� ����
        /// </summary>
        private void OnPropertyChangedListener(object sender, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.Property.Name.Equals("Bounds")) // ���� �������� ������ ����
            {
                // ���������� ��������� �������
                OnResize((Rect)e.NewValue);
            }
        }

        /// <summary>
        /// ���������� ��� ���������� �������� ����
        /// </summary>
        private void OnResize(Rect bounds)
        {
            _scaling = VisualRoot.RenderScaling;

            _width = (int)(bounds.Width * _scaling);
            _height = (int)(bounds.Height * _scaling);
        }

        /// <summary>
        /// ��� ����� ��������� ����������� ��������
        /// </summary>
        /// <param name="context"></param>
        public override void Render(DrawingContext context)
        {
            base.Render(context); // ���������� � ������ � ���� ������ ���������� ���� �����

            // ����������� ���������� ������� ������ ���������
            var centerX = _width / 2.0;
            var centersY = new List<double>();
            centersY.Add(_height / 4.0);
            centersY.Add(_height / 4.0 * 2);
            centersY.Add(_height / 4.0 * 3);

            // ������ ����
            foreach (var centerY in centersY)
            {
                DrawLight(context, new Point(centerX, centerY), _height / 8.0 * LightRadiusFactor);
            }
        }

        /// <summary>
        /// ����� ������ ����� ���������
        /// </summary>
        /// <param name="context"></param>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        private void DrawLight(DrawingContext context, Point center, double radius)
        {
            context.DrawEllipse(new SolidColorBrush(Colors.Red),
                new Pen(new SolidColorBrush(Colors.Black), 1),
                center,
                radius,
                radius);
        }
    }
}
