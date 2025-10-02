using TarefasApp.Services;
using TarefasApp.Models;

namespace TarefasApp.Pages;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        BindingContext = TaskRepository.Instance;
    }

    private async void OnDetailsClicked(object sender, EventArgs e)
    {
        if ((sender as Button)?.CommandParameter is TaskItem item)
        {
            await Shell.Current.GoToAsync(nameof(TaskDetailPage), new Dictionary<string, object>
            {
                { "Task", item }
            });
        }
    }

    private async void OnAddClicked(object sender, EventArgs e)
    {
        var modal = new AddEditTaskPage();
        await Navigation.PushModalAsync(new NavigationPage(modal));
    }
}
