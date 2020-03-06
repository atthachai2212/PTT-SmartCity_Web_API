using PTT_SmartCity_Web_API.Entity;
using PTT_SmartCity_Web_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTT_SmartCity_Web_API.Interfaces
{
    interface IEnvironmentService
    {
        IEnumerable<tbEnvironmentSensor> environmentSensorItems { get; }

        void environmentSensorData(LorawanServiceModel model);

        void environmentSensorDataInsert(LorawanServiceModel model);
    }
}
