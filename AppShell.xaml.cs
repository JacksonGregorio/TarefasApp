using TarefasApp.Pages;

namespace TarefasApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(TaskDetailPage), typeof(TaskDetailPage));
        Routing.RegisterRoute(nameof(AddEditTaskPage), typeof(AddEditTaskPage));
    }
}
