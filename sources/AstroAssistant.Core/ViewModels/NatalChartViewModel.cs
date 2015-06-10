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
    public class NatalChartViewModel : ViewModel
    {
        IFileService _FileService;

        /// <summary>
        /// Création d'un nouveau ViewModel de thème
        /// </summary>
        public NatalChartViewModel(IFileService fileService)
        {
            Definition = new NatalChartDefinition();
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
            _FileService = fileService;
            FileName = null;
            IsDirty = false;
        }

        /// <summary>
        /// Réinitialisation du thème
        /// </summary>
        public void Reset()
        {
            FileName = null;
            IsDirty = false;
        }

        /// <summary>
        /// Provoque le chargement du thème depuis un sélecteur de fichier
        /// </summary>
        public void LoadFromFile()
        {

        }

        /// <summary>
        /// Provoque le chargement du thème depuis un nom de fichier
        /// </summary>
        public void LoadFromFile(String fileName)
        {

        }

        /// <summary>
        /// Enregistre le thème dans le fichier en cours
        /// </summary>
        public Task Save()
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
                    NatalChartSerializer ser = new NatalChartSerializer();
                    await ser.Serialize(Definition, fileInfo.Stream);
                    FileName = fileInfo.FileName;
                    IsDirty = false;
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
            if (String.IsNullOrWhiteSpace(fileName)) return await SaveAs();
            if (IsBusy) return false;
            IsBusy = true;
            try
            {
                using (var fileInfo = await _FileService.OpenSave(fileName))
                {
                    NatalChartSerializer ser = new NatalChartSerializer();
                    await ser.Serialize(Definition, fileInfo.Stream);
                    FileName = fileInfo.FileName;
                    IsDirty = false;
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
        public NatalChartDefinition Definition { get; private set; }

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
            set { SetProperty(ref _IsBusy, value, () => IsBusy); }
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
            private set { SetProperty(ref _IsDirty, value, () => IsDirty); }
        }
        private bool _IsDirty;

    }

}
