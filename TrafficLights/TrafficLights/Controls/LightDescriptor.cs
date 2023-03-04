using Avalonia;
using Avalonia.Media;

namespace TrafficLights.Controls
{
    /// <summary>
    /// Описатель огня светофора
    /// </summary>
    public class LightDescriptor
    {
        public Point Center;

        public Color OnColor;

        public Color OffColor;

        public bool IsLightOn;
    }
}
