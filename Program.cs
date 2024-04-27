using CefSharp;
using CefSharp.DevTools;
using CefSharp.WinForms;
using System;
using System.IO;
using System.Windows.Forms;

namespace Phoenix_Browser
{
    static class Program
    {
        private static readonly Lazy<CefSettings> cefSettings = new Lazy<CefSettings>(() =>
        {
            var settings = new CefSettings
            {
                BackgroundColor = 255,
                CommandLineArgsDisabled = false,
            };
            // Enable DNT preference
            settings.CefCommandLineArgs.Add("enable-do-not-track", "1");

            settings.CefCommandLineArgs.Add("enable-experimental-web-platform-features", "1");
            settings.CefCommandLineArgs.Add("remote-debugging-port", "1024");
            settings.CefCommandLineArgs.Add("widevine-cdm-path", @"C:\Users\Temp Coding Account\AppData\Local\Egale Eye Browser\Browser Data\WidevineCdm\4.10.2710.0\_platform_specific\win_x86\widevinecdm.dll");
            settings.CefCommandLineArgs.Add("widevine-cdm-version", "4.10.2710.0");
            if(Properties.Settings.Default.low_resource_mode == true)
            {
                settings.MultiThreadedMessageLoop = false;
                settings.WindowlessRenderingEnabled = false;
                settings.DisableGpuAcceleration();
            }
            else
            {
                settings.MultiThreadedMessageLoop = true;
            }

            if (Properties.Settings.Default.first_start == true)
            {
                string appDataLocalPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                string cacheDirectory = Path.Combine(appDataLocalPath, "Egale Eye Browser", "Browser Data");

                try
                {
                    // Check if the directory exists before creating it
                    if (!Directory.Exists(cacheDirectory))
                    {
                        Directory.CreateDirectory(cacheDirectory);
                        Console.WriteLine("Folder created successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Folder already exists!");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                Properties.Settings.Default.first_start = false;
                Properties.Settings.Default.Save();
                Environment.Exit(0);
            }

            if (Properties.Settings.Default.best_osr_switch == true)
            {
                settings.SetOffScreenRenderingBestPerformanceArgs();
            }
            else
            {
                //Blank
            }
            if (Properties.Settings.Default.User_Prefs_Switch == true)
            {
                settings.PersistUserPreferences = true; // Enable persistent storage for user preferences
                string appDataLocalPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                string cacheDirectory = Path.Combine(appDataLocalPath, "Egale Eye Browser", "Browser Data");

                settings.CachePath = cacheDirectory;
                settings.CefCommandLineArgs.Add("--disk-cache-dir", settings.CachePath);
            }
            else
            {
                settings.PersistUserPreferences = false; // Disable persistent storage for user preferences
            }
            if (Properties.Settings.Default.Cookies_session_Switch == true)
            {
                settings.CefCommandLineArgs.Add("--enable-file-cookies");
                settings.PersistSessionCookies = true; // Enable persistent storage for session cookies
            }
            else
            {
                settings.CefCommandLineArgs.Remove("--enable-file-cookies");
                settings.PersistSessionCookies = false; // Disable persistent storage for session cookies
            }

            if (Properties.Settings.Default.hardware_frame_Sceduling == true)//NEW HARDWARE FRAME SCHEDULING
            {
                settings.CefCommandLineArgs.Add("enable-begin-frame-scheduling", "1");
            }
            else
            {
                settings.CefCommandLineArgs.Remove("enable-begin-frame-scheduling");
            }


            if (Properties.Settings.Default.hardware_v_sync == true)//NEW HARDWARE V SYNC
            {
                settings.CefCommandLineArgs.Add("disable-gpu-vsync", "1"); //Disable Vsync
            }
            else
            {
                settings.CefCommandLineArgs.Remove("disable-gpu-vsync"); //Disable Vsync
            }

            if (Properties.Settings.Default.hardware_gpu_composting == true)//NEW HARDWARE GPU COMPOSITING
            {
                settings.CefCommandLineArgs.Remove("disable-gpu-compositing");
            }
            else
            {
                settings.CefCommandLineArgs.Add("disable-gpu-compositing", "1");
            }
            settings.CefCommandLineArgs.Add("check-for-update-interval", "86400");

            if (Properties.Settings.Default.Geo_Location_Switch == true)
            {
                // Add this line to your CefSettings initialization
                settings.CefCommandLineArgs.Add("enable-blink-features", "Geolocation");
            }
            else
            {
                settings.CefCommandLineArgs.Add("disable-features", "Geolocation");
            }

            if (Properties.Settings.Default.hardware_acc_switch == true)//NEW HARDWARE DISABLE/ENABLE GPU
            {
                settings.CefCommandLineArgs.Add("--enable-vulkan");
            }
            else
            {
                settings.CefCommandLineArgs.Add("disable-gpu", "1");
            }
            if (Properties.Settings.Default.javascrpts_switch == true)
            {
                settings.CefCommandLineArgs.Add("enable-javascript", "1"); // Enable JavaScript
            }
            else
            {
                settings.CefCommandLineArgs.Add("disable-javascript", "1"); // Disable JavaScript
            }

            if (Properties.Settings.Default.print_preview_switch == true)
            {
                settings.EnablePrintPreview();
            }

            settings.CefCommandLineArgs.Add("enable-media-stream");
            settings.CefCommandLineArgs.Add("--enable-widevine");
            settings.CefCommandLineArgs.Add("--password-store");
            settings.MultiThreadedMessageLoop = true;
            settings.CefCommandLineArgs.Add("--component-updater=fast-update");
            return settings;
        });

        public static Lazy<CefSettings> CefSettings => cefSettings;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);

            // Initialize CEF the first time it is accessed
            Cef.Initialize(CefSettings.Value, true);

            // Create and open your main browser UI here
            Main_Browser_UI mainBrowserUi = new Main_Browser_UI();
            Application.Run(mainBrowserUi);




        }

       
    }
}
