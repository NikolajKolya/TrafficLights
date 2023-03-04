using Avalonia;
using Avalonia.Controls;

namespace TrafficLights.Controls
{
    public partial class Svetofor : UserControl
    {

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
    }
}
