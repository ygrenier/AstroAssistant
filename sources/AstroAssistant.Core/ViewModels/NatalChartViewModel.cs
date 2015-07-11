using Astro;
using AstroAssistant.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstroAssistant.ViewModels
{

    /// <summary>
    /// ViewModel d'un thème astral
    /// </summary>
    public class NatalChartViewModel : ViewModel, AstroAssistant.ViewModels.INatalChartViewModel
    {
        IFileService _FileService;
        ITimeZoneProvider _TimeZoneProvider;

        /// <summary>
        /// Création d'un nouveau ViewModel de thème
        /// </summary>
        public NatalChartViewModel(
            IFileService fileService, 
            ITimeZoneProvider tzProvider, 
            IAstroService astroService
            )
        {
            //DateUT = new DateUT(DateTime.Now);
            //Longitude = new SweNet.Longitude(5, 20, 0, LongitudePolarity.East);
            //Latitude = new SweNet.Latitude(47, 52, 0, LatitudePolarity.North);
            //HouseSystem = HouseSystem.Placidus;
            //Planets.AddRange(new Planet[] { 
            //    Planet.Sun, Planet.Moon, Planet.Mercury, Planet.Venus, Planet.Mars, Planet.Jupiter, 
            //    Planet.Saturn, Planet.Uranus, Planet.Neptune, Planet.Pluto,
            //    Planet.MeanNode, Planet.TrueNode,
            //    Planet.MeanApog, Planet.OscuApog, Planet.Earth
            //});
            //Planets.AddRange(new Planet[] { Planet.AsAsteroid(433), Planet.AsAsteroid(3045), Planet.AsAsteroid(7066) });
            this.AstroService = astroService;
            _FileService = fileService;
            _TimeZoneProvider = tzProvider;
            FileName = null;
            IsDirty = false;
            Definition = new NatalChartDefinitionViewModel(_TimeZoneProvider);
        }

        /// <summary>
        /// Réinitialisation du thème
        /// </summary>
        public void Reset()
        {
            Definition = new NatalChartDefinitionViewModel(_TimeZoneProvider);
            NatalChart = null;
            RaisePropertyChanged(() => Definition);
            RaisePropertyChanged(() => NatalChart);
            FileName = null;
            IsDirty = false;
        }

        async Task LoadFromFile(FileInformation fileInfos)
        {
            var ser = new NatalChartSerializer(_TimeZoneProvider);
            Definition.Initialize(await ser.Deserialize(fileInfos.Stream));
            NatalChart = new Astro.NatalChart();
            RaisePropertyChanged(() => Definition);
            RaisePropertyChanged(() => NatalChart);
            FileName = fileInfos.FileName;
            IsDirty = false;
        }

        /// <summary>
        /// Provoque le chargement du thème depuis un sélecteur de fichier
        /// </summary>
        public async Task<bool> LoadFromFile()
        {
            if (IsBusy) return false;
            IsBusy = true;
            try
            {
                using (var fileInfos = await _FileService.OpenLoadAsNatalChart())
                {
                    if (fileInfos == null) return false;
                    await LoadFromFile(fileInfos);
                }
                return true;
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// Provoque le chargement du thème depuis un nom de fichier
        /// </summary>
        public async Task<bool> LoadFromFile(String fileName)
        {
            if (IsBusy) return false;
            IsBusy = true;
            try
            {
                using (var fileInfos = await _FileService.OpenLoadNatalChart(fileName))
                {
                    if (fileInfos == null) return false;
                    await LoadFromFile(fileInfos);
                }
                return true;
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task SaveToFile(FileInformation fileInfo)
        {
            NatalChartSerializer ser = new NatalChartSerializer(_TimeZoneProvider);
            await ser.Serialize(Definition.Definition, fileInfo.Stream);
            FileName = fileInfo.FileName;
            IsDirty = false;
        }

        /// <summary>
        /// Enregistre le thème dans le fichier en cours
        /// </summary>
        public Task<bool> Save()
        {
            return String.IsNullOrWhiteSpace(FileName) ? SaveAs() : SaveAs(FileName);
        }

        /// <summary>
        /// Enregistre le thème sous un autre nom depuis un sélecteur de fichier
        /// </summary>
        public async Task<bool> SaveAs()
        {
            if (IsBusy) return false;
            IsBusy = true;
            try
            {
                using (var fileInfo = await _FileService.OpenSaveAsNatalChart())
                {
                    if (fileInfo == null) return false;
                    await SaveToFile(fileInfo);
                }
                return true;
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// Enregistre le thème sous un autre nom de fichier
        /// </summary>
        public async Task<bool> SaveAs(String fileName)
        {
            if (IsBusy) return false;
            if (String.IsNullOrWhiteSpace(fileName)) return await SaveAs();
            IsBusy = true;
            try
            {
                using (var fileInfo = await _FileService.OpenSaveNatalChart(fileName))
                {
                    if (fileInfo == null) return false;
                    await SaveToFile(fileInfo);
                }
                return true;
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// Définition du thème
        /// </summary>
        public NatalChartDefinitionViewModel Definition { get; private set; }
        INatalChartDefinitionViewModel INatalChartViewModel.Definition { get { return Definition; } }

        /// <summary>
        /// Thème calculé
        /// </summary>
        public NatalChart NatalChart { get; private set; }

        /// <summary>
        /// Indique si le viewmodel est occupé
        /// </summary>
        public bool IsBusy
        {
            get { return _IsBusy; }
            private set { SetProperty(ref _IsBusy, value, () => IsBusy); }
        }
        private bool _IsBusy;

        /// <summary>
        /// Nom du fichier
        /// </summary>
        public String FileName
        {
            get { return _FileName; }
            private set { SetProperty(ref _FileName, value, () => FileName); }
        }
        private String _FileName;

        /// <summary>
        /// Indique si le modèle doit être enregistré
        /// </summary>
        public bool IsDirty
        {
            get { return _IsDirty; }
            set { SetProperty(ref _IsDirty, value, () => IsDirty); }
        }
        private bool _IsDirty;

        /// <summary>
        /// Service Astro
        /// </summary>
        public IAstroService AstroService { get; private set; }
    }

}
