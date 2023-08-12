using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageChat.Client
{
    public partial class ImageChatClientForm : Form
    {
        public ImageChatClientForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Task
                .Run(() => _serverLocatorService.Start())
                .ContinueWith(_ => SetStatus("Determining image chat servers"))
                .ContinueWith(_ => Task.Delay(TimeSpan.FromSeconds(10)).Wait())
                .ContinueWith(_ => _serverLocatorService.Stop())
                .ContinueWith(_ => SetStatus($"{_serverLocatorService.Servers.Count} servers online found"));
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _serverLocatorService.Stop();
            _serverLocatorService.Dispose();
        }

        private void SetStatus(string status)
        {
            if (statusBar.InvokeRequired)
            {
                statusBar.Invoke((Action<string>)SetStatus, status);
            }
            else
            {
                statusBar.Text = status;
            }
        }
    }
}