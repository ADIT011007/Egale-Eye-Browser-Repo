using CefSharp;
using Phoenix_Browser;
using Phoenix_Browser.Properties;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CefsharpSandbox
{
    class MyCustomDownloadHandler : IDownloadHandler
    {
        private DateTime startTime;
        Form3 frm3 = new Form3();
        Main_Browser_UI browser_UI = new Main_Browser_UI();

        public event EventHandler<DownloadItem> OnBeforeDownloadFired;
        public event EventHandler<DownloadItem> OnDownloadUpdatedFired;

        public bool CanDownload(IWebBrowser chromiumWebBrowser, IBrowser browser, string url, string requestMethod)
        {
            return true;
        }
        public event EventHandler MyEvent;
        private Form3 _form;


        public MyCustomDownloadHandler(Form3 form3, Main_Browser_UI browser_UI)
        {
            _form = form3; //store the form passed in
            this.browser_UI = browser_UI;
        }


        public void OnBeforeDownload(IWebBrowser chromiumWebBrowser, IBrowser browser, DownloadItem downloadItem, IBeforeDownloadCallback callback)
        {
            if (downloadItem.IsValid)
            {
                var defaultSettings = Settings.Default;
                defaultSettings.File_Name = "Download Name :" + downloadItem.SuggestedFileName; //File name
                frm3.Show();
                long totalsize = downloadItem.TotalBytes / 1000 / 1000 / 1000; // Converts bytes to GB
                long totalsizeinMB = downloadItem.TotalBytes / 1024 / 1024; // Converts bytes to MB
                if (totalsize >= 1)
                {
                    defaultSettings.Total_Size_Unit = "Download Size " + totalsize + " Gb";
                }
                else if (totalsize < 1)
                {
                    defaultSettings.Total_Size_Unit = "Download Size " + totalsizeinMB + " Mb";
                }
                else
                {
                    defaultSettings.Total_Size_Unit = "Download Size " + downloadItem.CurrentSpeed + " Bytes";
                }

                defaultSettings.dwn_type = downloadItem.MimeType;
            }

            OnBeforeDownloadFired?.Invoke(this, downloadItem);

            if (!callback.IsDisposed)
            {
                using (callback)
                {
                    callback.Continue(
                        downloadItem.SuggestedFileName,
                        showDialog: true
                    );
                }
            }
        }

        /// https://cefsharp.github.io/api/51.0.0/html/T_CefSharp_DownloadItem.htm
        public async void OnDownloadUpdated(IWebBrowser chromiumWebBrowser, IBrowser browser, DownloadItem downloadItem, IDownloadItemCallback callback)
        {
            OnDownloadUpdatedFired?.Invoke(this, downloadItem);

            if (downloadItem.IsValid)
            {
                // Show progress of the download
                if (downloadItem.IsInProgress) // if downloading and its not 0 %
                {
                    var defaultSettings = Settings.Default;
                    defaultSettings.progress_bar_dwn = downloadItem.PercentComplete;
                    defaultSettings.Download_URL = downloadItem.Url;
                    defaultSettings.Download_Saved_Loc = downloadItem.FullPath;
                    long speedMbps = downloadItem.CurrentSpeed / 1024 / 1024; //Bytes to Mega Bytes
                    long speedKbps = downloadItem.CurrentSpeed / 1024; // Bytes to KiloBytes 
                    if (speedMbps >= 1)
                    {
                        defaultSettings.Speed_unit = "Speed " + speedMbps + " Mbps";
                    }
                    else
                    {
                        defaultSettings.Speed_unit = "Speed " + speedKbps + " Kbps";
                    }
                    if (speedKbps == 0)
                    {
                        defaultSettings.Bytes = "speed " + downloadItem.CurrentSpeed + "Bytes";
                    }





                    long recivedinmb = downloadItem.ReceivedBytes / 1024 / 1024; // onverts bytes to MB
                    long recivedingb = downloadItem.ReceivedBytes / 1000 / 1000 / 1000; // Converts bytes to GB
                    if (recivedingb >= 1)
                    {
                        defaultSettings.Data_Recived_Size = "Downloaded " + recivedingb + " Gb";
                    }
                    else if (recivedingb < 1)
                    {
                        defaultSettings.Data_Recived_Size = "Downloaded " + recivedinmb + " Mb";
                    }
                    else
                    {
                        defaultSettings.Data_Recived_Size = "Downloaded " + downloadItem.ReceivedBytes + " Bytes";
                    }




                    if (downloadItem.ReceivedBytes > 0)
                    {
                        // Calculate the elapsed time in seconds
                        var elapsedTime = (DateTime.Now - startTime).TotalSeconds;

                        // Calculate the download speed in bytes per second
                        var downloadSpeed = downloadItem.ReceivedBytes / elapsedTime;

                        // Calculate the estimated time remaining in seconds
                        var estimatedTimeRemaining = (downloadItem.TotalBytes - downloadItem.ReceivedBytes) / downloadSpeed;

                        // Format the estimated time remaining
                        var timeRemaining = TimeSpan.FromSeconds(estimatedTimeRemaining).ToString(@"hh\:mm\:ss");
                        // Update the label with the estimated time remaining
                        defaultSettings.Time_Remaining = $"Estimated time remaining : {timeRemaining}";
                    }
                    else
                    {
                        // Set the start time when the download starts
                        startTime = DateTime.Now;
                    }

                }

                if (downloadItem.IsCancelled)
                {
                    var defaultSettings = Settings.Default;
                    NotifyIcon notifyIcon = new NotifyIcon();
                    notifyIcon.Icon = SystemIcons.Information;
                    notifyIcon.Visible = true;
                    notifyIcon.BalloonTipTitle = "Egale Eye Downloader";
                    notifyIcon.BalloonTipText = "Download Failed : " + defaultSettings.File_Name;
                    notifyIcon.ShowBalloonTip(5000);
                    notifyIcon.BalloonTipClicked += new EventHandler(notifyIcon_BalloonTipClicked);
                    frm3.Hide();
                }



                frm3.button1.Click += delegate
                {
                    callback.Cancel(); //cancle the current download
                    frm3.Hide();
                };

                frm3.button2.Click += delegate
                {
                    callback.Pause(); //Pause the current download
                };

                frm3.button3.Click += delegate
                {
                    callback.Resume(); //resume the current download
                };

                if (downloadItem.IsComplete)
                {
                    var defaultSettings = Settings.Default;
                    NotifyIcon notifyIcon = new NotifyIcon();
                    notifyIcon.Icon = SystemIcons.Information;
                    notifyIcon.Visible = true;
                    notifyIcon.BalloonTipTitle = "Egale Eye Downloader";
                    notifyIcon.BalloonTipText = "Download Completed : " + defaultSettings.File_Name;
                    notifyIcon.ShowBalloonTip(5000);
                    notifyIcon.BalloonTipClicked += new EventHandler(notifyIcon_BalloonTipClicked);
                    await Task.Delay(10000);
                    // Call the method using the Invoke method of the form
                    frm3.Invoke((MethodInvoker)delegate
                    {
                        // Hide and refresh the form
                        frm3.Hide();
                        frm3.Refresh();
                    });
                }

            }

        }
        private void notifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            frm3.Show();
        }
    }
}
