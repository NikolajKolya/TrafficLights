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

            // Подписываемся на изменение свойств окна
            PropertyChanged += OnPropertyChangedListener;
        }

        /// <summary>
        /// Слушатель изменения свойств окна
        /// </summary>
        private void OnPropertyChangedListener(object sender, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.Property.Name.Equals("Bounds")) // Если меняется размер окна
            {
                // Обработать изменение размера
                OnResize((Rect)e.NewValue);
            }
        }

        /// <summary>
        /// Вызывается при измененеии размеров окна
        /// </summary>
        private void OnResize(Rect bounds)
        {
            _scaling = VisualRoot.RenderScaling;

            _width = (int)(bounds.Width * _scaling);
            _height = (int)(bounds.Height * _scaling);
        }
    }
}
