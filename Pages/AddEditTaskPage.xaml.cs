using System.ComponentModel;
using System.Runtime.CompilerServices;
using TarefasApp.Models;
using TarefasApp.Services;

namespace TarefasApp.Pages;

public partial class AddEditTaskPage : ContentPage
{
    public AddEditTaskViewModel VM { get; }

    public AddEditTaskPage()
    {
        InitializeComponent();
        VM = new AddEditTaskViewModel(null);
        BindingContext = VM;
    }

    public AddEditTaskPage(TaskItem toEdit)
    {
        InitializeComponent();
        VM = new AddEditTaskViewModel(toEdit);
        BindingContext = VM;
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(VM.Editable.Title))
        {
            await DisplayAlert("Atenção", "Informe um título.", "OK");
            return;
        }

        VM.Save();
        await Navigation.PopModalAsync();
    }
}

public class AddEditTaskViewModel : INotifyPropertyChanged
{
    private readonly TaskItem? _original;

    public string ModalTitle => _original is null ? "Nova tarefa" : "Editar tarefa";
    public TaskItem Editable { get; set; }
    public int PriorityIndex
    {
        get => Editable.Priority switch
        {
            TaskPriority.Baixa => 0,
            TaskPriority.Media => 1,
            TaskPriority.Alta => 2,
            _ => 1
        };
        set
        {
            Editable.Priority = value switch
            {
                0 => TaskPriority.Baixa,
                1 => TaskPriority.Media,
                2 => TaskPriority.Alta,
                _ => TaskPriority.Media
            };
            OnPropertyChanged();
        }
    }

    public AddEditTaskViewModel(TaskItem? original)
    {
        _original = original;
        Editable = original is null
            ? new TaskItem()
            : new TaskItem
            {
                Id = original.Id,
                Title = original.Title,
                Description = original.Description,
                CreatedAt = original.CreatedAt,
                Priority = original.Priority
            };
    }

    public void Save()
    {
        if (_original is null)
        {
            TaskRepository.Instance.Add(Editable);
        }
        else
        {
            TaskRepository.Instance.Update(Editable);
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string? name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
