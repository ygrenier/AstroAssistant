using System;
namespace AstroAssistant.ViewModels
{
    public interface INatalChartDefinitionViewModel
    {
        int BirthDateDay { get; set; }
        Astro.DayLightDefinition BirthDateDayLight { get; set; }
        int BirthDateHour { get; set; }
        int BirthDateMilliSecond { get; set; }
        int BirthDateMinute { get; set; }
        int BirthDateMonth { get; set; }
        int BirthDateSecond { get; set; }
        TimeZoneInfo BirthDateTimeZone { get; set; }
        double BirthDateUtcOffset { get; set; }
        int BirthDateYear { get; set; }
        string BirthPlaceName { get; set; }
        Astro.GeoPosition BirthPlacePosition { get; set; }
        Astro.Longitude BirthPlaceLongitude { get; set; }
        Astro.Latitude BirthPlaceLatitude { get; set; }
        double BirthPlaceAltitude { get; set; }
        Astro.Gender Gender { get; set; }
        Astro.HouseSystem HouseSystem { get; set; }
        string Name { get; set; }
        Astro.PositionCenter PositionCenter { get; set; }
    }
}
