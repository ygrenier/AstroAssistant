using Fluent;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AstroAssistant
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow, Services.IDialogService, Services.IFileService
    {
        #region IFileService
        Task<Services.FileInformation> Services.IFileService.OpenLoadAsNatalChart()
        {
            var dlg = new Microsoft.Win32.OpenFileDialog() {
                AddExtension = true,
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = ".ncf",
                Filter = "Fichier Thème Astral (*.ncf)|*.ncf",
                Title = "Ouvrir un thème astral"
            };

            if (dlg.ShowDialog() == true)
            {
                return Task.FromResult(new Services.FileInformation(dlg.FileName, File.OpenRead(dlg.FileName)));
            }
            else
            {
                return Task.FromResult<Services.FileInformation>(null);
            }
        }

        Task<Services.FileInformation> Services.IFileService.OpenLoadNatalChart(string fileName)
        {
            return Task.FromResult(new Services.FileInformation(fileName, File.OpenRead(fileName)));
        }

        Task<Services.FileInformation> Services.IFileService.OpenSaveAsNatalChart()
        {
            var dlg = new Microsoft.Win32.OpenFileDialog() {
                AddExtension = true,
                CheckFileExists = false,
                CheckPathExists = true,
                DefaultExt = ".ncf",
                Filter = "Fichier Thème Astral (*.ncf)|*.ncf",
                Title = "Enregistrer un thème astral"
            };
            if (dlg.ShowDialog() == true)
            {
                return Task.FromResult(new Services.FileInformation(dlg.FileName, File.Create(dlg.FileName)));
            }
            else
            {
                return Task.FromResult<Services.FileInformation>(null);
            }
        }

        Task<Services.FileInformation> Services.IFileService.OpenSaveNatalChart(string fileName)
        {
            return Task.FromResult(new Services.FileInformation(fileName, File.Create(fileName)));
        }
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = AppContext.Current.CreateViewModel<ViewModels.MainViewModel>();
            ViewModel.Initialize();
        }

        public ViewModels.MainViewModel ViewModel { get { return DataContext as ViewModels.MainViewModel; } }

    }
}
