using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using TrafficLights.Automat.Abstract;
using TrafficLights.BlinkingControl.Abstract;

namespace TrafficLights.Automat.Implementations
{
    public class Automat : IAutomat
    {
        #region Константы

        /// <summary>
        /// Время горения зелёного
        /// </summary>
        private const double GreenLightDuration = 10000;

        /// <summary>
        /// Время мигания зелёного
        /// </summary>
        private const double GreenBlinkDuration = 3000;

        /// <summary>
        /// Время горения красного
        /// </summary>
        private const double RedLightDuration = 10000;

        /// <summary>
        /// Время горения красного и желтого
        /// </summary>
        private const double RedAndYellowLightDuration = 3000;

        #endregion

        #region Внедрение зависимостей

        private readonly IBlinker _blinker;

        #endregion

        private TrafficLightsState _state = TrafficLightsState.GreenOn;

        private Timer _automatTimer;

        public Automat(IBlinker blinker)
        {
            _blinker = blinker;

            _automatTimer = new Timer(GreenLightDuration);
            _automatTimer.AutoReset = true;
            _automatTimer.Enabled = true;
            _automatTimer.Elapsed += OnAutomatTimer;
        }

        private void OnAutomatTimer(object? sender, ElapsedEventArgs e)
        {
            Step();
        }

        /// <summary>
        /// Шаг автомата
        /// </summary>
        private void Step()
        {
            switch(_state)
            {
                // Сейчас горит зелёный
                case TrafficLightsState.GreenOn:
                    _state = TrafficLightsState.GreenBlinking; // Переходим в состояние "Зелёный мигает"

                    _blinker.SetLightState(LightName.Green, LightState.Blinking);

                    _automatTimer.Interval = GreenBlinkDuration;

                    return;

                // Сейчас мигает зеленый
                case TrafficLightsState.GreenBlinking:
                    _state = TrafficLightsState.RedOn; // Переходим в состояние "Горит красный"

                    _blinker.SetLightState(LightName.Green, LightState.Off);
                    _blinker.SetLightState(LightName.Red, LightState.On);

                    _automatTimer.Interval = RedLightDuration;

                    return;

                // Сейчас горит красный
                case TrafficLightsState.RedOn:
                    _state = TrafficLightsState.RedAndYellowOn; // Переходим в состояние "Горит жёлтый и красный"

                    _blinker.SetLightState(LightName.Yellow, LightState.On);

                    _automatTimer.Interval = RedAndYellowLightDuration;

                    return;

                // Сейчас горит жёлтый и красный
                case TrafficLightsState.RedAndYellowOn:
                    _state = TrafficLightsState.GreenOn; // Переходим в состояние "Горит зеленый"

                    _blinker.SetLightState(LightName.Green, LightState.On);
                    _blinker.SetLightState(LightName.Red, LightState.Off);
                    _blinker.SetLightState(LightName.Yellow, LightState.Off);

                    _automatTimer.Interval = GreenLightDuration;

                    return;
            }
        }
    }
}
