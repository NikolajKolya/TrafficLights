using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using System.Collections.Generic;

namespace TrafficLights.Controls
{
    public partial class Svetofor : UserControl
    {

        /// <summary>
        /// Коэффициент, на который умножается радиус огней
        /// </summary>
        private const double LightRadiusFactor = 0.9;

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

        /// <summary>
        /// Это метод рисования содержимого контрола
        /// </summary>
        /// <param name="context"></param>
        public override void Render(DrawingContext context)
        {
            base.Render(context); // Обратиться к предку и дать предку нарисовать свои части

            // Расчитываем координаты центров кругов светофора
            var centerX = _width / 2.0;
            var centersY = new List<double>();
            centersY.Add(_height / 4.0);
            centersY.Add(_height / 4.0 * 2);
            centersY.Add(_height / 4.0 * 3);

            // Рисуем огни
            foreach (var centerY in centersY)
            {
                DrawLight(context, new Point(centerX, centerY), _height / 8.0 * LightRadiusFactor);
            }
        }

        /// <summary>
        /// Метод рисует огонь светафора
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
