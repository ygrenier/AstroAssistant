using Astro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstroAssistant.DesignTime
{
#if DEBUG
    public class DtNatalChartViewModel : AstroAssistant.ViewModels.INatalChartViewModel
    {
        public DtNatalChartViewModel()
        {
            Definition = new Astro.NatalChartDefinition();
            Definition.BirthDate.SetDate(DateTime.Now);
            Definition.BirthPlaceName = "Besançon";
            Definition.BirthPlacePosition = new Astro.GeoPosition(new Longitude(6, 1, 19, LongitudePolarity.East), new Latitude(47, 14, 35, LatitudePolarity.North), 400);
            Definition.Gender = Gender.Male;
            Definition.HouseSystem = HouseSystem.Placidus;
            Definition.Name = "Test";
            Definition.PositionCenter = PositionCenter.Topocentric;
            NatalChart = new Astro.NatalChart();
        }

        public Services.IAstroService AstroService { get; set; }

        public Astro.NatalChartDefinition Definition { get; set; }

        public string FileName { get; set; }

        public bool IsBusy { get; set; }

        public bool IsDirty { get; set; }

        public Task<bool> LoadFromFile()
        {
            return Task.FromResult(true);
        }

        public Task<bool> LoadFromFile(string fileName)
        {
            return Task.FromResult(true);
        }

        public Astro.NatalChart NatalChart { get; set; }

        public void Reset()
        {
        }

        public Task<bool> Save()
        {
            return Task.FromResult(true);
        }

        public Task<bool> SaveAs()
        {
            return Task.FromResult(true);
        }

        public Task<bool> SaveAs(string fileName)
        {
            return Task.FromResult(true);
        }
    }
#endif
}
