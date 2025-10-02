using System.Collections.ObjectModel;
using TarefasApp.Models;

namespace TarefasApp.Services;

public sealed class TaskRepository
{
    public static TaskRepository Instance { get; } = new();

    public ObservableCollection<TaskItem> Tasks { get; } = new();

    private TaskRepository()
    {
        Tasks.Add(new TaskItem { Title = "Comprar mantimentos", Description = "Arroz, feijão, café", Priority = TaskPriority.Alta });
        Tasks.Add(new TaskItem { Title = "Teste Testado", Description = "navegar teste", Priority = TaskPriority.Media });
    }

    public void Add(TaskItem item) => Tasks.Add(item);

    public void Update(TaskItem updated)
    {
        var idx = Tasks.IndexOf(Tasks.First(t => t.Id == updated.Id));
        Tasks[idx] = updated;
    }

    public void Remove(TaskItem item) => Tasks.Remove(item);
}
