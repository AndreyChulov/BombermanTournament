using Core.Network.Server.Server;

namespace Core.Network.Server
{
    public partial class ImageChatServerForm
    {
        public ImageChatServerForm()
        {
            //InitializeComponent();
            
            _serverService = new ServerService(TimeSpan.FromSeconds(1));
            _serverLocatorService = new ServerLocatorService(_serverService.TcpPort);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _serverLocatorService.Start();
        }

        private void ImageChatServerForm_FormClosing(object sender, object e)
        {
            _serverLocatorService.Dispose();
        }
    }
}