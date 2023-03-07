using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Timers;
using System.Xml.Linq;
using TrafficLights.BlinkingControl.Abstract;
using TrafficLights.ViewModels;

namespace TrafficLights.BlinkingControl.Inmplementaitons
{
    public class Blinker : IBlinker
    {
        /// <summary>
        /// Делегат, который надо вызывать при изменении состояния огня
        /// </summary>
        private OnLightStateChanges _onLightStateChanges;

        /// <summary>
        /// Словарь с состояниями огней
        /// </summary>
        private Dictionary<LightName, LightState> _lightStates
            = new Dictionary<LightName, LightState>();

        /// <summary>
        /// Горят-ли огни в текущее время?
        /// </summary>
        private Dictionary<LightName, bool> _isLightsOn = new Dictionary<LightName, bool>();

        private Timer _blinkTimer;

        public Blinker()
        {
            _lightStates.Add(LightName.Red, LightState.Off);
            _isLightsOn.Add(LightName.Red, false);

            _lightStates.Add(LightName.Yellow, LightState.Off);
            _isLightsOn.Add(LightName.Yellow, false);

            _lightStates.Add(LightName.Green, LightState.Off);
            _isLightsOn.Add(LightName.Green, false);

            _blinkTimer = new Timer(500);
            _blinkTimer.AutoReset = true;
            _blinkTimer.Enabled = true;
            _blinkTimer.Elapsed += OnBlinkTimer;
        }

        private void OnBlinkTimer(object? sender, ElapsedEventArgs e)
        {
            var toBlinkList = _lightStates
                .Where(l => l.Value == LightState.Blinking)
                .ToList();

            foreach (var toBlink in toBlinkList)
            {
                _isLightsOn[toBlink.Key] = !_isLightsOn[toBlink.Key];
                _onLightStateChanges(toBlink.Key, _isLightsOn[toBlink.Key]);
            }
        }

        public void SetLightState(LightName light, LightState state)
        {
            _lightStates[light] = state;
            
            switch (state)
            {
                case LightState.Off:
                    _isLightsOn[light] = false;
                    _onLightStateChanges(light, false);
                    break;

                case LightState.On:
                    _isLightsOn[light] = true;
                    _onLightStateChanges(light, true);
                    break;

                case LightState.Blinking:
                    _isLightsOn[light] = false;
                    _onLightStateChanges(light, false);
                    break;
            }
        }

        public void SetupDelegate(OnLightStateChanges lightStateChangedDelegate)
        {
            _onLightStateChanges = lightStateChangedDelegate;
        }
    }
}
