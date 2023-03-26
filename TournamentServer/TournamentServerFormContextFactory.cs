namespace TournamentServer;

public static class TournamentServerFormContextFactory
{
    public static TournamentServerFormContext TournamentServerFormContext { get; }
    
    static TournamentServerFormContextFactory()
    {
        TournamentServerFormContext = new TournamentServerFormContext();
    }
    
    
}