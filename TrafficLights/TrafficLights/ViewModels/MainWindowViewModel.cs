using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;
using TrafficLights.BlinkingControl.Abstract;
using Microsoft.Extensions.DependencyInjection;

namespace TrafficLights.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private bool _isRedOn;
        private bool _isYellowOn;
        private bool _isGreenOn;

        private IBlinker _blinker;

        public bool IsRedOn
        {
            get => _isRedOn;
            set => this.RaiseAndSetIfChanged(ref _isRedOn, value);
        }

        public bool IsYellowOn
        {
            get => _isYellowOn;
            set => this.RaiseAndSetIfChanged(ref _isYellowOn, value);
        }

        public bool IsGreenOn
        {
            get => _isGreenOn;
            set => this.RaiseAndSetIfChanged(ref _isGreenOn, value);
        }

        public MainWindowViewModel()
        {
            _blinker = Program.Di.GetService<IBlinker>();

            _blinker.SetupDelegate(OnLightStateChange);

            // Мигаем жёлтым, красный горит, зелёный погашен
            _blinker.SetLightState(LightName.Red, LightState.On);
            _blinker.SetLightState(LightName.Yellow, LightState.Blinking);
            _blinker.SetLightState(LightName.Green, LightState.Off);
        }

        /// <summary>
        /// Вызывается, когда мигатель меняет состояние огня
        /// </summary>
        /// <param name="name"></param>
        /// <param name="isOn"></param>
        public void OnLightStateChange(LightName name, bool isOn)
        {
            switch(name)
            {
                case LightName.Red:
                    IsRedOn = isOn;
                    break;

                case LightName.Yellow:
                    IsYellowOn = isOn;
                    break;

                case LightName.Green:
                    IsGreenOn = isOn;
                    break;
            }
        }
    }
}
