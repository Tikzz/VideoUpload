using System;
using System.Linq;
using System.Windows.Forms;

namespace VideoUpload
{
    public partial class Form1 : Form
    {
        public void AppendToConsole(string text)
        {
            outputConsole.AppendText(text + "\r\n");
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void browsebutton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    fileTextBox.Text = dialog.FileName;
                }
            }
        }

        private void fileTextBox_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (files != null && files.Any())
                fileTextBox.Text = files.First();

        }

        private void fileTextBox_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link;
            else
                e.Effect = DragDropEffects.None;
        }

        private async void cutUploadButton_Click(object sender, EventArgs e)
        {
            var totalFromSeconds = fromHours.Value * 60 * 60 + fromMinutes.Value * 60 + fromSeconds.Value;
            var totalToSeconds = toHours.Value * 60 * 60 + toMinutes.Value * 60 + toSeconds.Value;

            if (fileTextBox.Text == "")
            {
                System.Windows.Forms.MessageBox.Show("Select a video file.", "Error");
                return;
            }

            if (titleTextBox.Text == "")
            {
                System.Windows.Forms.MessageBox.Show("Enter a video title.", "Error");
                return;
            }

            if (totalToSeconds < totalFromSeconds) {
                System.Windows.Forms.MessageBox.Show("The start timestamp can't be higher than the end.", "Error");
                return;
            }

            if (totalFromSeconds == totalToSeconds) {
                System.Windows.Forms.MessageBox.Show("The total video duration can't be 0. Check your start and end times.", "Error");
                return;
            }

            URLtextBox.Visible = false;
            browseButton.Enabled = false;
            cutUploadButton.Enabled = false;

            decimal[] from = new decimal[] { fromHours.Value, fromMinutes.Value, fromSeconds.Value };
            decimal[] to = new decimal[] { toHours.Value, toMinutes.Value, toSeconds.Value };

            Video video;
            video = new Video(this, fileTextBox.Text, titleTextBox.Text,  from, to);
            await video.Start();

            browseButton.Enabled = true;
            cutUploadButton.Enabled = true;
            FlashWindow.Flash(this);
        }

        private void URLtextBox_Click(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;
            textBox.SelectAll();
            textBox.Focus();
        }
    }
}
