using System;
namespace AstroAssistant.ViewModels
{
    public interface IMainViewModel : IAppViewModel
    {
        RelayCommand LoadNatalChartCommand { get; }
        RelayCommand NewNatalChartCommand { get; }
        RelayCommand SaveAsNatalChartCommand { get; }
        RelayCommand SaveNatalChartCommand { get; }
    }
}
