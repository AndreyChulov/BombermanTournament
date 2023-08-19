using System.ComponentModel;
using System.Data;
using System.Windows;

namespace TournamentServer;

public partial class TournamentServerForm : Window
{
    public TournamentServerForm()
    {
        InitializeComponent();
    }

    private void TournamentServerForm_OnClosing(object? sender, CancelEventArgs e)
    {
        var dataContext = DataContext as ITournamentServerFormContext;

        if (dataContext == null)
        {
            throw new InvalidConstraintException($"{nameof(ITournamentServerFormContext)} should be initialized at this stage");
        }
        
        dataContext.TournamentServer.StopServer();
    }
}