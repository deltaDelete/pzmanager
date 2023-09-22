using Avalonia.Controls;
using ProjectManager.ViewModels;

namespace ProjectManager;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
    }
}