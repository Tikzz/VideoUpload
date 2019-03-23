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
        public const string authFile = "lib/auth.json";
        public const string imageFilename = "screen.png";
        public const string baseURL = "https://lag.party/v/";

        private readonly Form1 form;
        private readonly string filePath;
        private readonly string titleText;
        private readonly decimal[] fromTime;
        private readonly decimal[] toTime;
        private readonly int codecIndex;
        private string endpoint;

        private string dashTitle;
        private string batchPath;
        private string outputFile;
        private string outputExt;
        private bool upscale1440p;
        private bool cutVideo;

        private GoogleCredential credential;
        private string bucketName = "lag-party";
        private StorageClient storage;

        public Video(Form1 form1, string path, string title, decimal[] from, decimal[] to, int codec, bool upscaleFlag, bool cut)
        {
            form = form1;
            filePath = path;
            titleText = title;
            fromTime = from;
            toTime = to;
            codecIndex = codec;
            dashTitle = DashString(titleText);
            upscale1440p = upscaleFlag;
            cutVideo = cut;

            credential = GoogleCredential.FromJson(File.ReadAllText(authFile));
            storage = StorageClient.Create(credential);

            switch (codecIndex)
            {
                case 0:
                    batchPath = "bin/vp9.bat";
                    outputExt = ".webm";
                    break;
                case 1:
                    batchPath = "bin/x264.bat";
                    outputExt = ".mp4";
                    break;
                case 3:
                    batchPath = "bin/overkill.bat";
                    outputExt = ".webm";
                    break;
                default:
                    batchPath = "bin/vp9.bat";
                    outputExt = ".webm";
                    break;
            }

            outputFile = dashTitle + outputExt;
        }

        public void DeleteTempFile()
        {
            if (File.Exists(outputFile))
            {
                File.Delete(outputFile);
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

        public async Task Encode()
        {
            string fromArg;
            string toArg;

            string format(decimal n)
            {
                return String.Format("{0:00}", n);
            }

            string cmdArgs;
            if (cutVideo)
            {
                fromArg = String.Format("\"-ss {0}:{1}:{2}\"", format(fromTime[0]), format(fromTime[1]), format(fromTime[2]));
                toArg = String.Format("\"-to {0}:{1}:{2}\"", format(toTime[0]), format(toTime[1]), format(toTime[2]));

                cmdArgs = String.Format("\"{0}\" {1} {2} {3}", filePath, fromArg, toArg, outputFile);
            } else
            {
                cmdArgs = String.Format("\"{0}\" \"\" \"\" {1}", filePath, outputFile);
            }

            if (upscale1440p)
            {
                cmdArgs += " \"-vf scale=2560:1440\"";
            }

            await Task.Run(() =>
            {
                Process p = new Process
                {
                    StartInfo = {
                        FileName = batchPath,
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

        public async Task UploadImageAsync()
        {
            string imageHash = HashFile(imageFilename);
            if (FileExistsInCloud(imageHash))
            {
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

        public async Task UploadVideoAsync()
        {
            var baseFileName = dashTitle;
            var fileName = baseFileName;

            int i = 2;
            while (FileExistsInCloud(fileName + outputExt))
            {
                fileName = baseFileName + "-" + i;
                i++;
            }

            var videoObject = new Google.Apis.Storage.v1.Data.Object
            {
                Bucket = bucketName,
                Name = fileName + outputExt,
                ContentType = "video/webm"
            };

            var metadataDict = new Dictionary<string, string>
            {
                { "title", titleText },
                { "endpoint", fileName },
                { "image_endpoint", HashFile(imageFilename) },
            };
            videoObject.Metadata = metadataDict;
            endpoint = fileName;

            using (var stream = new System.IO.FileStream(outputFile, System.IO.FileMode.Open))
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
                using (var stream = File.OpenRead(outputFile))
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
