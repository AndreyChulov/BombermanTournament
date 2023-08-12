using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ImageChat.Shared;

namespace ImageChat.Client
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
                $@"[{DateTime.Now.ToLongTimeString().Replace(':', '.')}]client_console.log");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ImageChatClientForm());

            Logger.FreeUpResources();
        }
    }
}