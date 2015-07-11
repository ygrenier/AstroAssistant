using System;
using System.Collections.Generic;
namespace AstroAssistant.ViewModels
{
    public interface INatalChartDefinitionViewModel
    {
        int BirthDateDay { get; set; }
        Astro.DayLightDefinition BirthDateDayLight { get; set; }
        List<KeyValuePair<Astro.DayLightDefinition, String>> ListDayLightDefinitions { get; }
        int BirthDateHour { get; set; }
        int BirthDateMilliSecond { get; set; }
        int BirthDateMinute { get; set; }
        int BirthDateMonth { get; set; }
        int BirthDateSecond { get; set; }
        TimeZoneInfo BirthDateTimeZone { get; set; }
        List<KeyValuePair<TimeZoneInfo, String>> ListTimeZoneInfos { get; }
        double BirthDateUtcOffset { get; set; }
        double BirthDayLightOffset { get; }
        int BirthDateYear { get; set; }
        DateTimeOffset BirthDateUTC { get; }
        string BirthPlaceName { get; set; }
        Astro.GeoPosition BirthPlacePosition { get; set; }
        Astro.Longitude BirthPlaceLongitude { get; set; }
        Astro.Latitude BirthPlaceLatitude { get; set; }
        double BirthPlaceAltitude { get; set; }
        Astro.Gender Gender { get; set; }
        List<KeyValuePair<Astro.Gender, String>> ListGenders { get; }
        Astro.HouseSystem HouseSystem { get; set; }
        string Name { get; set; }
        Astro.PositionCenter PositionCenter { get; set; }
    }
}
