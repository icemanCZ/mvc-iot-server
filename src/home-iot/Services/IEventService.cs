namespace HomeIot.Services
{
    public interface IEventService
    {
        void NewSensorRegistered(int sensorId);
        void SensorConnectionLost(int sensorId);
        void NotifySensorConnected(int sensorId);
    }
}