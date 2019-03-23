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
            codecComboBox.SelectedIndex = 0;
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
            var cut = true;

            if (fileTextBox.Text == "")
            {
                MessageBox.Show("Select a video file.", "Error");
                return;
            }

            if (titleTextBox.Text == "")
            {
                MessageBox.Show("Enter a video title.", "Error");
                return;
            }

            if (totalToSeconds < totalFromSeconds)
            {
                MessageBox.Show("The start timestamp can't be higher than the end.", "Error");
                return;
            }

            if (cutCheckBox.Checked && totalFromSeconds == totalToSeconds)
            {
                MessageBox.Show("The total cut duration can't be 0. Check your start and end times.", "Error");
                return;
            }

            URLtextBox.Visible = false;
            browseButton.Enabled = false;
            cutUploadButton.Enabled = false;

            decimal[] from = new decimal[] { fromHours.Value, fromMinutes.Value, fromSeconds.Value };
            decimal[] to = new decimal[] { toHours.Value, toMinutes.Value, toSeconds.Value };

            Video video;
            video = new Video(this, fileTextBox.Text, titleTextBox.Text, from, to, codecComboBox.SelectedIndex, upscaleCheckBox.Checked, cut);
            this.AppendToConsole("Encoding video...");
            await video.Encode();

            if (uploadCheckBox.Checked)
            {
                this.AppendToConsole("Uploading image...");
                await video.UploadImageAsync();
                this.AppendToConsole("Uploading video...");
                await video.UploadVideoAsync();
            }
            this.AppendToConsole("All done!");
            if (deleteCheckBox.Checked)
            {
                video.DeleteTempFile();
            }

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

        private void numeric_SelectAll(object sender, EventArgs e)
        {
            NumericUpDown field = (NumericUpDown)sender;
            field.Select(0, 2);
        }

        private void fileTextBox_TextChanged(object sender, EventArgs e)
        {
            if (fileTextBox.Text != "")
            {
                cutUploadButton.Enabled = true;
            }
            else
            {
                cutUploadButton.Enabled = false;
            }
        }

        private void uploadCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (uploadCheckBox.Checked)
            {
                deleteCheckBox.Enabled = true;
                if (cutCheckBox.Checked)
                {
                    cutUploadButton.Text = "Cut and Upload";
                } else
                {
                    cutUploadButton.Text = "Encode and Upload";
                }
            }
            else
            {
                deleteCheckBox.Checked = false;
                deleteCheckBox.Enabled = false;
                if (cutCheckBox.Checked)
                {
                    cutUploadButton.Text = "Cut and Save";
                }
                else
                {
                    cutUploadButton.Text = "Save";
                }
            }
        }

        private void codecComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (codecComboBox.SelectedIndex == 2)
            {
                deleteCheckBox.Checked = false;
                uploadCheckBox.Checked = false;
                upscaleCheckBox.Checked = true;

                deleteCheckBox.Enabled = false;
                uploadCheckBox.Enabled = false;
                upscaleCheckBox.Enabled = false;
            }
            else
            {
                deleteCheckBox.Checked = true;
                uploadCheckBox.Checked = true;
                upscaleCheckBox.Checked = false;

                deleteCheckBox.Enabled = true;
                uploadCheckBox.Enabled = true;
                upscaleCheckBox.Enabled = true;
            }
        }

        private void cutCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (cutCheckBox.Checked)
            {
                fromHours.Enabled = true;
                fromMinutes.Enabled = true;
                fromSeconds.Enabled = true;
                toHours.Enabled = true;
                toMinutes.Enabled = true;
                toSeconds.Enabled = true;
                if (uploadCheckBox.Checked)
                {
                    cutUploadButton.Text = "Cut and Upload";
                } else
                {
                    cutUploadButton.Text = "Save";
                }
            } else
            {
                fromHours.Enabled = false;
                fromMinutes.Enabled = false;
                fromSeconds.Enabled = false;
                toHours.Enabled = false;
                toMinutes.Enabled = false;
                toSeconds.Enabled = false;
                if (uploadCheckBox.Checked)
                {
                    cutUploadButton.Text = "Encode and Upload";
                }
                else
                {
                    cutUploadButton.Text = "Encode and Save";
                }
            }
        }
    }
}
