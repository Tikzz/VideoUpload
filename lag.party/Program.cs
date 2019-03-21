using System;
using System.IO;
using System.Windows.Forms;

namespace VideoUpload
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
            if (CheckFiles())
            {
                Application.Run(new Form1());
            }
        }

        static bool CheckFiles()
        {
            if (!File.Exists("screen.png"))
            {
                MessageBox.Show("File not found: screen.png", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!File.Exists("lib/auth.json"))
            {
                MessageBox.Show("File not found: lib/auth.json", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!File.Exists("bin/ffmpeg.exe"))
            {
                MessageBox.Show("File not found: bin/ffmpeg.exe", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
    }
}
