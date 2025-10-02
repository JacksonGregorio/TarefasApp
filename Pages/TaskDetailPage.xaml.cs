using TarefasApp.Models;
using TarefasApp.Services;

namespace TarefasApp.Pages;

[QueryProperty(nameof(Task), "Task")]
public partial class TaskDetailPage : ContentPage
{
    private TaskItem _task = new();

    public TaskItem Task
    {
        get => _task;
        set { _task = value; BindingContext = _task; }
    }

    public TaskDetailPage()
    {
        InitializeComponent();
    }

    private async void OnEditClicked(object sender, EventArgs e)
    {
        var modal = new AddEditTaskPage(Task);
        await Navigation.PushModalAsync(new NavigationPage(modal));
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        var confirm = await DisplayAlert("Excluir", "Deseja realmente excluir esta tarefa?", "Sim", "Cancelar");
        if (confirm)
        {
            TaskRepository.Instance.Remove(Task);
            await Shell.Current.GoToAsync("..");
        }
    }
}
