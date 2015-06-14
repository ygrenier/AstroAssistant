using System;
namespace AstroAssistant.ViewModels
{
    public interface INatalChartViewModel
    {
        AstroAssistant.Services.IAstroService AstroService { get; }
        INatalChartDefinitionViewModel Definition { get; }
        string FileName { get; }
        bool IsBusy { get; }
        bool IsDirty { get; set; }
        System.Threading.Tasks.Task<bool> LoadFromFile();
        System.Threading.Tasks.Task<bool> LoadFromFile(string fileName);
        Astro.NatalChart NatalChart { get; }
        void Reset();
        System.Threading.Tasks.Task<bool> Save();
        System.Threading.Tasks.Task<bool> SaveAs();
        System.Threading.Tasks.Task<bool> SaveAs(string fileName);
    }
}
