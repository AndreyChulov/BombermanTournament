using System.Reflection;
using BombermanGame;
using BombermanGame.Shared.Interfaces;

namespace Tournament
{
    public partial class TournamentForm : Form
    {
        private List<IPlayer> _players = new();
        private List<Type> _playerTypes = new();
        private List<Type> _loadedPlayersTypes = new();
        
        public TournamentForm()
        {
            InitializeComponent();
        }

        private void btnStartTournament_Click(object sender, EventArgs e)
        {
            BombermanGameForm gameForm = new(
                CreatePlayer(0),
                CreatePlayer(1),
                CreatePlayer(2),
                CreatePlayer(3)
            );
            try
            {
                gameForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Exception while game processing is caused [{ex.Message}]",
                    "Exception caused",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private IPlayer? CreatePlayer(int playerIndex)
        {
            return lstbxLoadedAi.Items.Count > playerIndex ? 
                _loadedPlayersTypes.First(x=> IsPlayerOnLoadedListItem(playerIndex, x)).CreatePlayer() : null;
        }
        
        private bool IsPlayerOnLoadedListItem(int playerIndex, Type playerType)
        {
            return playerType.FullName == (string)lstbxLoadedAi.Items[playerIndex];
        }

        private void TournamentForm_Load(object sender, EventArgs e)
        {
            var executingFileLocation = Assembly.GetExecutingAssembly().Location;
            var executingFolderLocation = new FileInfo(executingFileLocation).DirectoryName;
            var files = GetDllFilesFromFolder(executingFolderLocation);
            var fileAssemblies = LoadFilesAssemblies(files);

            foreach (var assembly in fileAssemblies)
            {
                var assemblyTypes = assembly.GetTypes();
                var playerTypes = GetPlayerTypes(assemblyTypes);

                _playerTypes.AddRange(playerTypes);
                _players.AddRange(CreatePlayers(playerTypes));
            }

            lstbxAvailableAi.Items.Clear();
            lstbxAvailableAi.Items.AddRange(GetPlayerTypesFullNames(_players));
            lstbxAvailableAi.SelectedIndex = 0;
            btnLoadToGame.Enabled = true;
        }

        private static object[] GetPlayerTypesFullNames(List<IPlayer> players)
        {
            return (string[])players.Select(x=>x.GetType().FullName ?? "Unknown type").ToArray();
        }

        private static IEnumerable<IPlayer> CreatePlayers(Type[] playerTypes)
        {
            return playerTypes
                .Select(x=>x.CreatePlayer());
        }

        private static Type[] GetPlayerTypes(Type[] assemblyTypes)
        {
            return assemblyTypes
                .Where(
                    x => x.IsInterfaceImplemented(typeof(IPlayer))
                )
                .ToArray();
        }

        private static Assembly[] LoadFilesAssemblies(string[] files)
        {
            return files.Select(Assembly.LoadFile).ToArray();
        }

        private static string[] GetDllFilesFromFolder(string? executingFolderLocation)
        {
            return Directory
                .GetFiles(executingFolderLocation ?? "")
                .Where(x=>x.EndsWith(".dll"))
                .ToArray();
        }

        private void lstbxAvailableAi_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedPlayer = _players[lstbxAvailableAi.SelectedIndex];

            txtNickname.Text = selectedPlayer.Nickname;
            rtxtStrategyDescription.Text = selectedPlayer.StrategyDescription;
        }

        private void btnLoadToGame_Click(object sender, EventArgs e)
        {
            lstbxLoadedAi.Items.Add(lstbxAvailableAi.Items[lstbxAvailableAi.SelectedIndex]);
            _loadedPlayersTypes.Add(_playerTypes[lstbxAvailableAi.SelectedIndex]);

            if (lstbxLoadedAi.Items.Count == 1)
                lstbxLoadedAi.SelectedIndex = 0;

            btnUnloadFromGame.Enabled = true;
        
            if (lstbxLoadedAi.Items.Count == 4)
                btnLoadToGame.Enabled = false;

        }

        private void btnUnloadFromGame_Click(object sender, EventArgs e)
        {
            _loadedPlayersTypes.RemoveAt(lstbxLoadedAi.SelectedIndex);
            lstbxLoadedAi.Items.Remove(lstbxLoadedAi.Items[lstbxLoadedAi.SelectedIndex]);

            if ((lstbxLoadedAi.Items.Count == 0) && (lstbxAvailableAi.SelectedIndex == -1))
            {
                btnUnloadFromGame.Enabled = false;
            }
            
            btnLoadToGame.Enabled = true;
        }

    }
    
}

