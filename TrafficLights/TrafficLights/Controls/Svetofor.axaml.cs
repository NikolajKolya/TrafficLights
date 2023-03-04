using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using System.Collections.Generic;
using TrafficLights.Enums;
using TrafficLights.Models;

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

        private Dictionary<LightName, LightDescriptor> LightsDescriptors 
            = new Dictionary<LightName, LightDescriptor>();

        public Svetofor()
        {
            InitializeComponent();

            // Наполняем словарь описателей огней
            LightsDescriptors.Add(LightName.Red,
                new LightDescriptor()
                {
                    OffColor = new Color(255, 30, 0, 0),
                    OnColor = Colors.Red,
                    IsLightOn = false,
                    Center = new Point(0, 0)
                });

            LightsDescriptors.Add(LightName.Yellow,
                new LightDescriptor()
                {
                    OffColor = new Color(255, 30, 30, 0),
                    OnColor = Colors.Yellow,
                    IsLightOn = false,
                    Center = new Point(0, 0)
                });

            LightsDescriptors.Add(LightName.Green,
                new LightDescriptor()
                {
                    OffColor = new Color(255, 0, 30, 0),
                    OnColor = Colors.Green,
                    IsLightOn = false,
                    Center = new Point(0, 0)
                });

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
            LightsDescriptors[LightName.Red].Center = new Point(_width / 2.0, _height / 4.0);
            LightsDescriptors[LightName.Yellow].Center = new Point(_width / 2.0, _height / 4.0 * 2);
            LightsDescriptors[LightName.Green].Center = new Point(_width / 2.0, _height / 4.0 * 3);

            // Рисуем огни
            foreach (var lightDescriptorPair in LightsDescriptors)
            {
                DrawLight(context, lightDescriptorPair.Value, _height / 8.0 * LightRadiusFactor);
            }
        }

        /// <summary>
        /// Метод рисует огонь светафора
        /// </summary>
        /// <param name="context"></param>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        private void DrawLight(DrawingContext context, LightDescriptor descriptor, double radius)
        {
            context.DrawEllipse(new SolidColorBrush(descriptor.IsLightOn ? descriptor.OnColor : descriptor.OffColor),
                new Pen(new SolidColorBrush(Colors.Black), 1),
                descriptor.Center,
                radius,
                radius);
        }
    }
}
