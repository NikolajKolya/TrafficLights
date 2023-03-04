using Avalonia;
using Avalonia.Media;

namespace TrafficLights.Models
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
