namespace Core.Network.Client
{
    public partial class ImageChatClientForm
    {
        

        private void Form1_Load(object sender, EventArgs e)
        {
            Task
                .Run((Action)(() => _serverLocatorService.Start()))
                .ContinueWith(_ => SetStatus("Determining image chat servers"))
                .ContinueWith(_ => Task.Delay(TimeSpan.FromSeconds(10)).Wait())
                .ContinueWith(_ => _serverLocatorService.Stop())
                .ContinueWith(_ => SetStatus($"{_serverLocatorService.Servers.Count} servers online found"));
        }

        private void Form1_FormClosing(object sender, object e)
        {
            _serverLocatorService.Stop();
            _serverLocatorService.Dispose();
        }

        private void SetStatus(string status)
        {
            /*if (statusBar.InvokeRequired)
            {
                statusBar.Invoke((Action<string>)SetStatus, status);
            }
            else
            {
                statusBar.Text = status;
            }*/
        }
    }
}