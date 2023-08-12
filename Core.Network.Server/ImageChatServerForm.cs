using System;
using System.Windows.Forms;
using ImageChat.Server.Server;

namespace ImageChat.Server
{
    public partial class ImageChatServerForm : Form
    {
        public ImageChatServerForm()
        {
            InitializeComponent();
            
            _serverService = new ServerService(TimeSpan.FromSeconds(1));
            _serverLocatorService = new ServerLocatorService(_serverService.TcpPort);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _serverLocatorService.Start();
        }

        private void ImageChatServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _serverLocatorService.Dispose();
        }
    }
}