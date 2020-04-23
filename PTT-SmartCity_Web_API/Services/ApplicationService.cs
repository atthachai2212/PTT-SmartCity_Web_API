using PTT_SmartCity_Web_API.Entity;
using PTT_SmartCity_Web_API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using PTT_SmartCity_Web_API.Models;
using Newtonsoft.Json;

namespace PTT_SmartCity_Web_API.Services
{
    public class ApplicationService : IApplicationService
    {
        private dbLoRaSmartCityContext db;

        public ApplicationService()
        {
            db = new dbLoRaSmartCityContext();
        }

        public IEnumerable<tbEnvironmentSensor> environmentSensorItems => this.db.tbEnvironmentSensor.ToList();
        public IEnumerable<tbGPS> GspTrackingItems => this.db.tbGPS.ToList();

        public IEnumerable<tbSensorHub> sensorHubItems => this.db.tbSensorHub.ToList();

        public IEnumerable<tbWasteBinSensor> wasteBinSensorItems => this.db.tbWasteBinSensor.ToList();

        public IEnumerable<tbWaterLevelSensor> waterLevelSensorItems => this.db.tbWaterLevelSensor.ToList();

        public IEnumerable<tbWaterQualitySensor> waterQualitySensorItems => this.db.tbWaterQualitySensor.ToList();

        public IEnumerable<tbWeatherSensor> weatherSensorItems => this.db.tbWeatherSensor.ToList();

    }
}