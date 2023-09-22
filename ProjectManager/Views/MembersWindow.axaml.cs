using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ProjectManager.ViewModels;

namespace ProjectManager.Views;

public partial class MembersWindow : Window
{
    public MembersWindow()
    {
        InitializeComponent();
        DataContext = new MembersViewModel();
    }
}