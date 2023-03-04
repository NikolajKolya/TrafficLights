using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using System;
using System.Collections.Generic;

namespace TrafficLights.Controls
{
    public partial class Svetofor : UserControl
    {

        /// <summary>
        /// �����������, �� ������� ���������� ������ �����
        /// </summary>
        private const double LightRadiusFactor = 0.9;

        /// <summary>
        /// ������ ����������
        /// </summary>
        private const double LedRadius = 2;

        /// <summary>
        /// ���������� ����� ������������
        /// </summary>
        private const double LedSpacing = 1.5;

        private double _scaling;

        private int _width;
        private int _height;

        private Dictionary<LightName, LightDescriptor> LightsDescriptors 
            = new Dictionary<LightName, LightDescriptor>();

        /// <summary>
        /// �������� ���������� ������� ����
        /// </summary>
        public static readonly AttachedProperty<bool> IsRedOnProperty
            = AvaloniaProperty.RegisterAttached<Svetofor, Interactive, bool>(nameof(IsRedOn));

        /// <summary>
        /// �����-�� ������� �����
        /// </summary>
        public bool IsRedOn
        {
            get { return GetValue(IsRedOnProperty); }
            set { SetValue(IsRedOnProperty, value); }
        }
        /// <summary>
        /// �������� ���������� ������ ����
        /// </summary>
        public static readonly AttachedProperty<bool> IsYellowOnProperty
            = AvaloniaProperty.RegisterAttached<Svetofor, Interactive, bool>(nameof(IsYellowOn));

        /// <summary>
        /// �����-�� ������ �����
        /// </summary>
        public bool IsYellowOn
        {
            get { return GetValue(IsYellowOnProperty); }
            set { SetValue(IsYellowOnProperty, value); }
        }
        /// <summary>
        /// �������� ���������� ������� ����
        /// </summary>
        public static readonly AttachedProperty<bool> IsGreenOnProperty
            = AvaloniaProperty.RegisterAttached<Svetofor, Interactive, bool>(nameof(IsGreenOn));

        /// <summary>
        /// �����-�� ������� �����
        /// </summary>
        public bool IsGreenOn
        {
            get { return GetValue(IsGreenOnProperty); }
            set { SetValue(IsGreenOnProperty, value); }
        }
        public Svetofor()
        {
            InitializeComponent();

            // ��������� ������� ���������� �����
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

            // ������������� �� ��������� ������� ����
            PropertyChanged += OnPropertyChangedListener;

            // ������������� �� ��������� ��������� �����
            IsRedOnProperty.Changed.Subscribe(x => HandleIsRedOnChanged(x.Sender, x.NewValue.GetValueOrDefault<bool>()));
            IsYellowOnProperty.Changed.Subscribe(x => HandleIsYellowOnChanged(x.Sender, x.NewValue.GetValueOrDefault<bool>()));
            IsGreenOnProperty.Changed.Subscribe(x => HandleIsGreenOnChanged(x.Sender, x.NewValue.GetValueOrDefault<bool>()));
        }

        /// <summary>
        /// ��������� ��������� ��������� �������� ����
        /// </summary>
        private void HandleIsRedOnChanged(IAvaloniaObject sender, bool v)
        {
            InvalidateVisual();
        }

        /// <summary>
        /// ��������� ��������� ��������� ������� ����
        /// </summary>
        private void HandleIsYellowOnChanged(IAvaloniaObject sender, bool v)
        {
            InvalidateVisual();
        }

        /// <summary>
        /// ��������� ��������� ��������� �������� ����
        /// </summary>
        private void HandleIsGreenOnChanged(IAvaloniaObject sender, bool v)
        {
            InvalidateVisual();
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
            LightsDescriptors[LightName.Red].Center = new Point(_width / 2.0, _height / 4.0);
            LightsDescriptors[LightName.Red].IsLightOn = IsRedOn;

            LightsDescriptors[LightName.Yellow].Center = new Point(_width / 2.0, _height / 4.0 * 2);
            LightsDescriptors[LightName.Yellow].IsLightOn = IsYellowOn;

            LightsDescriptors[LightName.Green].Center = new Point(_width / 2.0, _height / 4.0 * 3);
            LightsDescriptors[LightName.Green].IsLightOn = IsGreenOn;

            // ������ ����
            foreach (var lightDescriptorPair in LightsDescriptors)
            {
                DrawLight(context, lightDescriptorPair.Value, _height / 8.0 * LightRadiusFactor);
            }
        }

        /// <summary>
        /// ����� ������ ����� ���������
        /// </summary>
        /// <param name="context"></param>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        private void DrawLight(DrawingContext context, LightDescriptor descriptor, double radius)
        {
            context.DrawEllipse(new SolidColorBrush(descriptor.OffColor),
                new Pen(new SolidColorBrush(Colors.Black), 1),
                descriptor.Center,
                radius,
                radius);

            // R^2 > X^2 + Y^2
            for (var y = descriptor.Center.Y - radius; y < descriptor.Center.Y + radius; y += LedSpacing + 2 * LedRadius)
            {
                for (var x = descriptor.Center.X - radius; x < descriptor.Center.X + radius; x += LedSpacing + 2 * LedRadius)
                {
                    if (Math.Pow(radius - LedRadius, 2) > Math.Pow(x - descriptor.Center.X, 2) + Math.Pow(y - descriptor.Center.Y, 2))
                    {
                        DrawLed(context, new Point(x, y), descriptor.OffColor, descriptor.OnColor, descriptor.IsLightOn);
                    }
                }
            }
        }

        /// <summary>
        /// ������ ���� ���������
        /// </summary>
        private void DrawLed(DrawingContext context, Point center, Color offColor, Color onColor, bool isOn)
        {
            var brush = new SolidColorBrush(isOn ? onColor : offColor);

            context.DrawEllipse(brush,
                new Pen(brush, 1),
                center,
                LedRadius,
                LedRadius);
        }
    }
}
