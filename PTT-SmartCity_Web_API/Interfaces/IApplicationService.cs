using PTT_SmartCity_Web_API.Entity;
using PTT_SmartCity_Web_API.Models;
using System;
using System.Collections.Generic;

namespace PTT_SmartCity_Web_API.Interfaces
{
    interface IApplicationService
    {
        List<GetAppEnvironmentData> AppEnvironmentSensorItems(ApplicationDataFilterOptions filterOptions);

        List<GetAppSensorHubData> AppSensorHubItems(ApplicationDataFilterOptions filterOptions);

        List<GetAppWasteBinData> AppWasteBinSensorItems(ApplicationDataFilterOptions filterOptions);

        List<GetAppWeatherData> AppWeatherSensorItems(ApplicationDataFilterOptions filterOptions);

        List<GetAppWaterLevelData> AppWaterLevelSensorItems(ApplicationDataFilterOptions filterOptions);

        List<GetAppWaterQualityData> AppWaterQualitySensorItems(ApplicationDataFilterOptions filterOptions);

        List<GetAppGpsData> AppGspTrackingItems(ApplicationDataFilterOptions filterOptions);

    }
}
