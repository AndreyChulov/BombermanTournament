namespace TournamentServer;

public static class TournamentServerFormDesignContextFactory
{
    public static TournamentServerFormDesignContext TournamentServerFormContext { get; }
    
    static TournamentServerFormDesignContextFactory()
    {
        TournamentServerFormContext = new TournamentServerFormDesignContext();
    }
    
    
}