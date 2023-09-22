using Avalonia.Controls.ApplicationLifetimes;
using ProjectManager.Views;

namespace ProjectManager.ViewModels;

public class MainWindowViewModel
{
    public Command OpenMembersCommand { get; }

    public MainWindowViewModel()
    {
        OpenMembersCommand = new Command(OpenMembers);
    }

    private void OpenMembers()
    {
        MembersWindow window = new MembersWindow();
        window.Show((App.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime).MainWindow);
    }
}