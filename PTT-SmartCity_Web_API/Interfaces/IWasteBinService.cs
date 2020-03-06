using PTT_SmartCity_Web_API.Entity;
using PTT_SmartCity_Web_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTT_SmartCity_Web_API.Interfaces
{
    interface IWasteBinService
    {
        IEnumerable<tbWasteBinSensor> WasteBinSensorItems { get; }

        void WasteBinSensorData(LorawanServiceModel model);

        void WasteBinSensorDataInsert(LorawanServiceModel model);
    }
}
