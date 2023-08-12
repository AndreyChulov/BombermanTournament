using System;
using System.IO;
using System.Windows.Forms;
using ImageChat.Server;
using ImageChat.Shared;

namespace ImageChat.Server
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Logger.Initialize(
                $@"[{DateTime.Now.ToLongTimeString().Replace(':', '.')}]server_console.log");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ImageChatServerForm());

            Logger.FreeUpResources();

        }
    }
}