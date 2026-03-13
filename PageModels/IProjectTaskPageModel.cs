using CommunityToolkit.Mvvm.Input;
using HealthLog.Models;

namespace HealthLog.PageModels
{
    public interface IProjectTaskPageModel
    {
        IAsyncRelayCommand<ProjectTask> NavigateToTaskCommand { get; }
        bool IsBusy { get; }
    }
}