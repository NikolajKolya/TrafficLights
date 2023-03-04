namespace TrafficLights.BlinkingControl.Abstract
{
    /// <summary>
    /// Делегат, вызываемый при изменении состояния огня
    /// </summary>
    public delegate void OnLightStateChanges(LightName name, bool isOn);

    public interface IBlinker
    {
        /// <summary>
        /// Запомнить делегат, который нужно вызывать при изменении состояния огня
        /// </summary>
        void SetupDelegate(OnLightStateChanges lightStateChangedDelegate);

        /// <summary>
        /// Установить состояние огня
        /// </summary>
        void SetLightState(LightName light, LightState state);
    }
}
