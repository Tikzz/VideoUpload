using System;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Generic;
using System.Security.Cryptography;
using Google.Cloud.Storage.V1;
using Google.Apis.Auth.OAuth2;

namespace VideoUpload
{
    public class Video
    {
        public const string authFilename = "lib/auth.json";
        public const string outputFilename = "output.mp4";
        public const string imageFilename = "screen.png";
        public const string baseURL = "https://lag.party/v/";

        private readonly Form1 form;
        private readonly string filePath;
        private readonly string titleText;
        private readonly decimal[] fromTime;
        private readonly decimal[] toTime;
        private string endpoint;
        private string imageURL;

        private GoogleCredential credential;
        private string bucketName = "lag-party";
        private StorageClient storage;

        public Video(Form1 form1, string path, string title, decimal[] from, decimal[] to)
        {
            form = form1;
            filePath = path;
            titleText = title;
            fromTime = from;
            toTime = to;

            credential = GoogleCredential.FromJson(File.ReadAllText(authFilename));
            storage = StorageClient.Create(credential);
        }

        public void DeleteTempFile()
        {
            if (File.Exists(outputFilename))
            {
                File.Delete(outputFilename);
            }
        }


        public async Task Start()
        {
            form.AppendToConsole("Encoding video...");
            await this.Encode();
            form.AppendToConsole("Uploading image...");
            await this.UploadImageAsync();
            form.AppendToConsole("Uploading video...");
            await this.UploadVideoAsync();
            form.AppendToConsole("All done!");
            this.DeleteTempFile();
        }

        private async Task Encode()
        {
            string fromArg;
            string toArg;

            string format(decimal n)
            {
                return String.Format("{0:00}", n);
            }

            fromArg = String.Format("{0}:{1}:{2}.0", format(fromTime[0]), format(fromTime[1]), format(fromTime[2]));
            toArg = String.Format("{0}:{1}:{2}.0", format(toTime[0]), format(toTime[1]), format(toTime[2]));

            string cmdArgs;
            cmdArgs = String.Format("-ss {0} -to {1} -i \"{2}\" -c:v libvpx-vp9 -b:v 8000k -b:a 192k " + outputFilename, fromArg, toArg, filePath);

            this.DeleteTempFile();

            await Task.Run(() =>
            {
                Process p = new Process
                {
                    StartInfo = {
                        FileName = "bin/ffmpeg",
                        Arguments = cmdArgs,
                        CreateNoWindow = true,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                    }
                };
                p.Start();

                string processOutput = null;
                while ((processOutput = p.StandardError.ReadLine()) != null)
                {
                    if (form.InvokeRequired)
                    {
                        form.Invoke(new Action(() => form.AppendToConsole(processOutput)));
                    }
                }

                p.WaitForExit();
            });
        }

        private async Task UploadImageAsync()
        {
            string imageHash = HashFile(imageFilename);
            if (FileExistsInCloud(imageHash)) {
                form.AppendToConsole("Image already exists.");
                return;
            }

            var imageObject = new Google.Apis.Storage.v1.Data.Object
            {
                Bucket = bucketName,
                Name = imageHash,
                ContentType = "image/png"
            };

            using (var stream = new System.IO.FileStream(imageFilename, System.IO.FileMode.Open))
            {
                var uploadObjectOptions = new Google.Cloud.Storage.V1.UploadObjectOptions
                {
                    ChunkSize = Google.Cloud.Storage.V1.UploadObjectOptions.MinimumChunkSize
                };

                await storage.UploadObjectAsync(
                        imageObject,
                        stream,
                        uploadObjectOptions
                    );

                form.AppendToConsole("Done!");
            }

        }

        private async Task UploadVideoAsync()
        {

            var dashTitle = DashString(titleText);

            var baseFileName = dashTitle;
            var fileName = baseFileName;

            int i = 2;
            while (FileExistsInCloud(fileName + ".webm"))
            {
                fileName = baseFileName + "-" + i;
                i++;
            }

            var videoObject = new Google.Apis.Storage.v1.Data.Object
            {
                Bucket = bucketName,
                Name = fileName + ".webm",
                ContentType = "video/webm"
            };

            var metadataDict = new Dictionary<string, string>
            {
                { "title", titleText },
                { "endpoint", fileName },
                { "image_url", HashFile(imageFilename) },
            };
            videoObject.Metadata = metadataDict;
            endpoint = fileName;

            using (var stream = new System.IO.FileStream(outputFilename, System.IO.FileMode.Open))
            {
                form.UploadProgressBar.Maximum = (int)stream.Length;

                var uploadObjectOptions = new Google.Cloud.Storage.V1.UploadObjectOptions
                {
                    ChunkSize = Google.Cloud.Storage.V1.UploadObjectOptions.MinimumChunkSize
                };

                var progressReporter = new System.Progress<Google.Apis.Upload.IUploadProgress>(OnUploadProgress);
                await storage.UploadObjectAsync(
                        videoObject,
                        stream,
                        uploadObjectOptions,
                        progress: progressReporter
                    );

            }

        }

        void OnUploadProgress(Google.Apis.Upload.IUploadProgress progress)
        {
            switch (progress.Status)
            {
                case Google.Apis.Upload.UploadStatus.Starting:
                    form.UploadProgressBar.Minimum = 0;
                    form.UploadProgressBar.Value = 0;
                    form.UploadProgressBar.Visible = true;
                    break;
                case Google.Apis.Upload.UploadStatus.Completed:
                    form.UploadProgressBar.Value = form.UploadProgressBar.Maximum;
                    form.UploadProgressBar.Visible = false;
                    form.URLtextBox.Visible = true;
                    form.URLtextBox.Text = baseURL + endpoint;
                    form.AppendToConsole("Upload complete.");
                    break;
                case Google.Apis.Upload.UploadStatus.Uploading:
                    form.UploadProgressBar.Value = (int)progress.BytesSent;
                    break;
                case Google.Apis.Upload.UploadStatus.Failed:
                    form.AppendToConsole("Uploading failed" + progress.Exception);
                    System.Windows.Forms.MessageBox.Show("Upload failed");
                    form.UploadProgressBar.Visible = false;
                    break;
            }
        }

        private string HashFile(string fileName)
        {
            string hash;
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(outputFilename))
                {
                    var hashBytes = md5.ComputeHash(stream);
                    hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
                }
            }
            return hash;
        }

        private string DashString(string text)
        {
            System.Text.RegularExpressions.Regex rgx = new System.Text.RegularExpressions.Regex("[^a-z0-9 -]");
            string dashName = rgx.Replace(text.Replace(" ", "-").ToLower(), "");
            return dashName;
        }

        private bool FileExistsInCloud(string objectName)
        {
            try
            {
                storage.GetObject(bucketName, objectName);
            }
            catch (Google.GoogleApiException)
            {
                return false;
            }
            return true;
        }

    }
}
