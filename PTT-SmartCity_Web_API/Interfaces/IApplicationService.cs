using PTT_SmartCity_Web_API.Entity;
using PTT_SmartCity_Web_API.Models;
using System;
using System.Collections.Generic;

namespace PTT_SmartCity_Web_API.Interfaces
{
    interface IApplicationService
    {
        IEnumerable<tbEnvironmentSensor> environmentSensorItems { get; }
        IEnumerable<tbGPS> GspTrackingItems { get; }
        IEnumerable<tbSensorHub> sensorHubItems { get; }
        IEnumerable<tbWasteBinSensor> wasteBinSensorItems { get; }
        IEnumerable<tbWaterLevelSensor> waterLevelSensorItems { get; }
        IEnumerable<tbWaterQualitySensor> waterQualitySensorItems { get; }
        IEnumerable<tbWeatherSensor> weatherSensorItems { get; }

    }
}
