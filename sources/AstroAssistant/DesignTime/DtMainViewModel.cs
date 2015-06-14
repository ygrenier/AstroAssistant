using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstroAssistant.DesignTime
{
#if DEBUG
    public class DtMainViewModel : ViewModels.IMainViewModel
    {
        public DtMainViewModel()
        {

        }

        public void Initialize()
        {
        }

        public Task<bool> LoadNatalChart()
        {
            return Task.FromResult(true);
        }

        public Task<bool> NewNatalChart()
        {
            return Task.FromResult(true);
        }

        public Task<bool> SaveAsNatalChart()
        {
            return Task.FromResult(true);
        }

        public Task<bool> SaveNatalChart()
        {
            return Task.FromResult(true);
        }

        public ViewModels.RelayCommand LoadNatalChartCommand { get; set; }

        public ViewModels.RelayCommand NewNatalChartCommand { get; set; }

        public ViewModels.RelayCommand SaveAsNatalChartCommand { get; set; }

        public ViewModels.RelayCommand SaveNatalChartCommand { get; set; }

        public Services.IAstroService AstroService { get; set; }

        public ViewModels.INatalChartViewModel CurrentNatalChart { get; set; }

        public Services.IDialogService DialogService { get; set; }
    }
#endif
}
