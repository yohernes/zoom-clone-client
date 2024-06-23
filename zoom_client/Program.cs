using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace zoom_client
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            { Application.Run(new loginUI()); }catch(Exception e) { }
            //Application.Run(new meetingUI());

            Process.GetCurrentProcess().Kill();
        }
    }
}
