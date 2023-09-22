using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Avalonia.Controls.ApplicationLifetimes;
using ProjectManager.Models;
using ProjectManager.Views.Dialogs;

namespace ProjectManager.ViewModels;

public class MembersViewModel : ViewModelBase {
    private string _searchQuery = string.Empty;
    private ObservableCollection<Member> _members = new();
    private List<Member> _membersFull = new List<Member>();
    private int _selectedSearchColumn;
    private bool _isSortByDescending = false;

    public int SelectedSearchColumn {
        get => _selectedSearchColumn;
        set {
            RaisePropertyChanging();
            if (value == _selectedSearchColumn) return;
            _selectedSearchColumn = value;
            RaisePropertyChanged();
        }
    }

    public bool IsSortByDescending {
        get => _isSortByDescending;
        set => SetField(ref _isSortByDescending, value);
    }

    public ObservableCollection<Member> Members {
        get => _members;
        set {
            if (Equals(value, _members)) return;
            _members = value;
            RaisePropertyChanged();
        }
    }


    public string SearchQuery {
        get => _searchQuery;
        set {
            if (value == _searchQuery) return;
            _searchQuery = value;
            RaisePropertyChanged();
        }
    }

    public Command AddMemberCommand { get; }

    public MembersViewModel() {
        GetDataFromDb();
        PropertyChanged += OnSearchChanged;

        AddMemberCommand = new Command(AddMember);
    }

    private void AddMember()
    {
        var db = new Database();
        MemberAddDialog dialog = new MemberAddDialog();
        dialog.AddButton.Click += (sender, args) =>
        {
            var member = new Member()
            {
                FullName = dialog.FullName.Text
            };
            db.InsertAsync(member);
        };
        dialog.Show((App.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime).MainWindow);
        // db.InsertAsync(new Member()
        // {
        //     MemberId = 0,
        //     FullName = "Дима Бумаженко",
        //     JobId = 1
        // });
    }

    private void OnSearchChanged(object? sender, PropertyChangedEventArgs e) {
        if (e.PropertyName != nameof(SearchQuery)
            && e.PropertyName != nameof(SelectedSearchColumn)
            && e.PropertyName != nameof(IsSortByDescending)) {
            return;
        }

        IEnumerable<Member> filtered = SearchQuery == ""
            ? new ObservableCollection<Member>(_membersFull)
            : SelectedSearchColumn switch {
                1 => _membersFull
                    .Where(it => it.MemberId.ToString().Contains(SearchQuery)),
                2 => _membersFull
                    .Where(it => it.FullName.ToLower().Contains(SearchQuery.ToLower())),
                3 => _membersFull
                    .Where(it => it.Job!.Name.ToLower().Contains(SearchQuery.ToLower())),
                _ => _membersFull
                    .Where(it =>
                        it.Job!.Name.ToLower().Contains(SearchQuery.ToLower()) ||
                        it.FullName.ToLower().Contains(SearchQuery.ToLower()) ||
                        it.MemberId.ToString().Contains(SearchQuery)
                    )
            };

        Members = SelectedSearchColumn switch
        {
            2 => new(IsSortByDescending
                ? filtered.OrderByDescending(it => it.FullName)
                : filtered.OrderBy(it => it.FullName)),
            3 => new(IsSortByDescending
                ? filtered.OrderByDescending(it => it.Job!.Name)
                : filtered.OrderBy(it => it.Job!.Name)),
            _ => new(IsSortByDescending
                ? filtered.OrderByDescending(it => it.MemberId)
                : filtered.OrderBy(it => it.MemberId))
        };
    }

    private async void GetDataFromDb() {
        await using var db = new Database();
        var users = db.GetAsync<Member>();
        var list = await users.ToListAsync();
        list = list.Select(it => {
            it.Job = db.GetById<Job>(it.JobId);
            return it;
        }).ToList();
        _membersFull.AddRange(list);
        Members = new ObservableCollection<Member>(_membersFull);
    }
}