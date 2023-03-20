using System.Windows;
using System.Windows.Controls;

namespace TournamentServer.Controls;

public partial class ClientInfo : UserControl
{
    public static readonly DependencyProperty TitleProperty = 
        DependencyProperty.Register(nameof(Title), typeof(string), 
            typeof(ClientInfo), new PropertyMetadata(ClientInfoDesignContext.Title));

    public ClientInfo()
    {
        InitializeComponent();
    }

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }
}