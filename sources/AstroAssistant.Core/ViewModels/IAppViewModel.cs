using System;
namespace AstroAssistant.ViewModels
{
    public interface IAppViewModel
    {
        AstroAssistant.Services.IAstroService AstroService { get; }
        INatalChartViewModel CurrentNatalChart { get; }
        AstroAssistant.Services.IDialogService DialogService { get; }
        void Initialize();
        System.Threading.Tasks.Task<bool> LoadNatalChart();
        System.Threading.Tasks.Task<bool> NewNatalChart();
        System.Threading.Tasks.Task<bool> SaveAsNatalChart();
        System.Threading.Tasks.Task<bool> SaveNatalChart();
    }
}
