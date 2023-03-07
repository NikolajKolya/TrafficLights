using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficLights.Automat.Abstract
{
    /// <summary>
    /// Список состояний светофора
    /// </summary>
    public enum TrafficLightsState
    {
        GreenOn,

        GreenBlinking,

        RedOn,

        RedAndYellowOn
    }
}
