using CefSharp;
using CefSharp.DevTools.Autofill;
using CefSharp.WinForms;
using CefsharpSandbox;
using Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Device.Location;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Phoenix_Browser
{
    public partial class Main_Browser_UI : Form
    {
        public bool isloading = false;
       
        public Main_Browser_UI()
        {
            InitializeComponent();
           
        }
        public string adress = "";
        private void Main_Browser_UI_Load(object sender, EventArgs e)
        {
            location_mocking();
            if(isPiPActive == true)
            {
                pIPToolStripMenuItem1.Text = "PIP is On";
            }
            else
            {
                pIPToolStripMenuItem1.Text = "PIP is Off";
            }
            string search_engine_html = $"{Application.StartupPath}\\Egale Eye Search.html";
            // Get the synchronization context for the UI thread
            chromiumWebBrowser1.Load(search_engine_html);
            chromiumWebBrowser1.DownloadHandler = new MyCustomDownloadHandler(new Form3(), this);
            chromiumWebBrowser1.RequestHandler = new CustomRequestHandler();


        }




        //____________________________________________________________________________________________________________________________________________________________
        private void ImplementLazyLoadingOfImages()
        {
            // Construct the JavaScript code to implement lazy loading of images
            string lazyLoadingScript = @"
        document.addEventListener('DOMContentLoaded', function() {
            var lazyImages = document.querySelectorAll('img[data-src]');
            
            // IntersectionObserver to check when images enter the viewport
            var observer = new IntersectionObserver(function(entries, observer) {
                entries.forEach(function(entry) {
                    if (entry.isIntersecting) {
                        var lazyImage = entry.target;
                        lazyImage.src = lazyImage.dataset.src;
                        lazyImage.removeAttribute('data-src');
                        observer.unobserve(lazyImage);
                    }
                });
            }, { rootMargin: '0px 0px 100px 0px' }); // Adjust rootMargin as needed
            
            lazyImages.forEach(function(lazyImage) {
                observer.observe(lazyImage);
            });
        });
    ";

            // Execute the lazy loading script in the CefSharp browser control
            chromiumWebBrowser1.ExecuteScriptAsync(lazyLoadingScript);
        }
        //__________________________________________________________________________________________________________________________________________________________


        private void ApplyLightTheme()
        {
            // Construct the JavaScript code to apply the light theme
            string lightThemeScript = @"
        // Define CSS rules for light theme
        var lightStyles = '
            body {
                background-color: #ffffff;
                color: #000000;
            }
            /* Add more CSS rules for light theme as needed */
        ';

        // Inject the light theme styles into a <style> element in the <head> of the document
        var styleElement = document.createElement('style');
        styleElement.setAttribute('id', 'light-theme-style');
        styleElement.innerHTML = lightStyles;

        // Remove any existing light theme style element
        var existingStyleElement = document.getElementById('light-theme-style');
        if (existingStyleElement) {
            existingStyleElement.remove();
        }

        // Append the light theme style element to the <head> of the document
        document.head.appendChild(styleElement);
    ";

            // Execute the light theme script in the CefSharp browser control
            chromiumWebBrowser1.ExecuteScriptAsync(lightThemeScript);
        }



        private void ApplyDarkTheme()
        {
            // Construct the JavaScript code to apply the dark theme
            string darkThemeScript = @"
        // Define CSS rules for dark theme
        var darkStyles = '
            body {
                background-color: #1f1f1f;
                color: #ffffff;
            }
            /* Add more CSS rules for dark theme as needed */
        ';

        // Inject the dark theme styles into a <style> element in the <head> of the document
        var styleElement = document.createElement('style');
        styleElement.setAttribute('id', 'dark-theme-style');
        styleElement.innerHTML = darkStyles;

        // Remove any existing dark theme style element
        var existingStyleElement = document.getElementById('dark-theme-style');
        if (existingStyleElement) {
            existingStyleElement.remove();
        }

        // Append the dark theme style element to the <head> of the document
        document.head.appendChild(styleElement);
    ";

            // Execute the dark theme script in the CefSharp browser control
            chromiumWebBrowser1.ExecuteScriptAsync(darkThemeScript);
        }













        //_______________________________________________________________________________________________________________________________________________________________________

        private void location_mocking()
        {
            if (Properties.Settings.Default.Geo_Location_Switch == true)
            {
                double Longitude = Properties.Settings.Default.Longitude;
                double Latitude = Properties.Settings.Default.Latitude;
                // Inject JavaScript code to mock Geolocation APIs into the existing chromiumWebBrowser1 instance
                string script = $@"
        navigator.permissions.query = options => {{
            return Promise.resolve({{
                state: 'granted'
            }});
        }};
        navigator.geolocation.getCurrentPosition = (success, error, options) => {{
            success({{
                coords: {{
                    latitude: {Latitude},
                    longitude: {Longitude},
                    accuracy: 10,
                    // Remove altitude
                    altitudeAccuracy: null,
                    heading: null,
                    speed: null
                }},
                timestamp: Date.now()
            }});
        }};
        navigator.geolocation.watchPosition = (success, error, options) => {{
            success({{
                coords: {{
                    latitude: {Latitude},
                    longitude: {Longitude},
                    accuracy: 49,
                    // Remove altitude
                    altitudeAccuracy: null,
                    heading: null,
                    speed: null
                }},
                timestamp: Date.now()
            }});
        }};
    ";

                chromiumWebBrowser1.ExecuteScriptAsyncWhenPageLoaded(script, oneTime: false);
            }
            else
            {
                // Inject JavaScript code to mock Geolocation APIs into the existing chromiumWebBrowser1 instance
                string script = $@"
    navigator.permissions.query = options => {{
        return Promise.resolve({{
            state: 'denied'
        }});
    }};
    navigator.geolocation.getCurrentPosition = (success, error, options) => {{
        error({{
            code: 1, // PERMISSION_DENIED
            message: 'User denied Geolocation'
        }});
    }};
    navigator.geolocation.watchPosition = (success, error, options) => {{
        error({{
            code: 1, // PERMISSION_DENIED
            message: 'User denied Geolocation'
        }});
    }};
";
                chromiumWebBrowser1.ExecuteScriptAsyncWhenPageLoaded(script, oneTime: false);
            }
        }
//________________________________________________________________________________________________________________________________________________
        private void pip()
        {
            // Inject JavaScript code to enable PiP mode
            string script = @"
                // Check if Picture-in-Picture is supported
                if (document.pictureInPictureEnabled) {
                    // Request Picture-in-Picture mode
                    document.querySelector('video').requestPictureInPicture();
                }
            ";

            // Execute the JavaScript code in the WebBrowser control
            chromiumWebBrowser1.ExecuteScriptAsyncWhenPageLoaded(script, oneTime: false);
        }

        private void EnterPiPMode()
        {
            // Inject JavaScript code to enter PiP mode
            string script = @"
        // Check if Picture-in-Picture is supported
        if (document.pictureInPictureEnabled) {
            // Request Picture-in-Picture mode
            document.querySelector('video').requestPictureInPicture();
        }
    ";

            // Execute the JavaScript code in the ChromiumWebBrowser control
            chromiumWebBrowser1.ExecuteScriptAsyncWhenPageLoaded(script, oneTime: false);

            // Update PiP state
            isPiPActive = true;
        }

        private void ExitPiPMode()
        {
            // Inject JavaScript code to exit PiP mode
            string script = @"
        // Check if Picture-in-Picture is supported and currently active
        if (document.exitPictureInPicture) {
            // Exit Picture-in-Picture mode
            document.exitPictureInPicture();
        }
    ";

            // Execute the JavaScript code in the ChromiumWebBrowser control
            chromiumWebBrowser1.ExecuteScriptAsyncWhenPageLoaded(script, oneTime: false);

            // Update PiP state
            isPiPActive = false;
        }
//________________________________________________________________________________________________________________________________________________

        private void chromiumWebBrowser1_LoadingStateChanged(object sender, CefSharp.LoadingStateChangedEventArgs e)
        {
            if (chromiumWebBrowser1.InvokeRequired)
            {
                chromiumWebBrowser1.BeginInvoke(new Action(() =>
                {
                    if (chromiumWebBrowser1.IsLoading)
                    {
                        toolStripDropDownButton1.Enabled = false;
                        toolStripButton2.Image = Properties.Resources.x;
                    }
                    else
                    {
                        toolStripDropDownButton1.Enabled = true;
                        toolStripButton2.Image = Properties.Resources.Reload;
                    }
                }));
            }
            else
            {
                if (chromiumWebBrowser1.IsLoading)
                {
                    toolStripDropDownButton1.Enabled = false;
                    toolStripButton2.Image = Properties.Resources.x;
                }
                else
                {
                    toolStripDropDownButton1.Enabled = true;
                    toolStripButton2.Image = Properties.Resources.Reload;
                }
            }
        }
        
       

       

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            menu1 mnu = new menu1();
            mnu.Show();
        }
        private void chromiumWebBrowser1_FrameLoadStart(object sender, CefSharp.FrameLoadStartEventArgs e)
        {
            
        }

        private void ApplyTheme(int theme)
        {
            // Check the parameter value to determine the theme
            if (theme == 1)
            {
                // Apply light theme if the parameter value is 1
                ApplyLightTheme();
            }
            else if (theme == 2)
            {
                // Apply dark theme for any other parameter value
                ApplyDarkTheme();
            }
        }

       





        private void chromiumWebBrowser1_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            string address = e.Url;

            if (address.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
            {
                DisplayHttpWarning();
            }
            else if (address.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                DisplayHttpsMessage();
            }

            if (Properties.Settings.Default.force_dark_mode == true)
            {
                ApplyTheme(2);//Dark
            }
            else
            {
                ApplyTheme(1);//Light
            }
           
        }
        private void DisplayHttpWarning()
        {
            string warningScript = @"
        // Create a <style> element to apply custom CSS styles
        var styleElement = document.createElement('style');
        styleElement.textContent = `
            /* Define CSS styles for the warning message */
            .warning-message {
                position: fixed;
                top: 0;
                left: 0;
                width: 100%;
                background-color: #ffcc00; /* Yellow background color */
                color: #333; /* Dark text color */
                padding: 10px;
                text-align: center;
                font-size: 16px;
                font-weight: bold;
                z-index: 9999; /* Ensure the warning appears on top of other content */
            }
        `;

    // Create a <div> element to display the warning message
    var warningDiv = document.createElement('div');
    warningDiv.className = 'warning-message';
    warningDiv.textContent = 'Warning: This site is not secure. Proceed with caution.';

    // Append the <style> element and <div> element to the <head> of the document
    document.head.appendChild(styleElement);
    document.body.appendChild(warningDiv);
";

            chromiumWebBrowser1.ExecuteScriptAsync(warningScript);
        }

        private void DisplayHttpsMessage()
        {
            string script = @"
        // Create a <style> element to apply custom CSS styles
        var styleElement = document.createElement('style');
        styleElement.textContent = `
            /* Define CSS styles for the safe message */
            .message {
                position: fixed;
                top: 0;
                left: 0;
                width: 100%;
                background-color: #00cc00; /* Green background color */
                color: #333; /* Dark text color */
                padding: 10px;
                text-align: center;
                font-size: 16px;
                font-weight: bold;
                z-index: 9999; /* Ensure the message appears on top of other content */
            }
        `;

    // Create a <div> element to display the safe message
    var safeDiv = document.createElement('div');
    safeDiv.className = 'message';
    safeDiv.textContent = 'You are safe!';

    // Append the <style> element and safe message <div> to the <head> of the document
    document.head.appendChild(styleElement);
    document.body.appendChild(safeDiv);

    // After 3 seconds, remove the safe message
    setTimeout(function() {
        safeDiv.remove();
    }, 2000);
";

            chromiumWebBrowser1.ExecuteScriptAsync(script);
        }






        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            chromiumWebBrowser1.Back();
        }

        private void toolStripButton2_Click_1(object sender, EventArgs e)
        {
            if (chromiumWebBrowser1.IsLoading)
            {
                chromiumWebBrowser1.Stop();
            }
            else
            {
                chromiumWebBrowser1.Reload();
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
               chromiumWebBrowser1.LoadUrl(textBox1.Text);

                // Optionally, prevent the TextBox from processing the Enter key further
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        

        private void closeBRowserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chromiumWebBrowser1.Dispose();
            this.Close();
        }

        private void chromiumWebBrowser1_AddressChanged(object sender, AddressChangedEventArgs e)
        {
            adress = e.Address;
            if(Properties.Settings.Default.ad_blocker_switch == true)
            {
                // Create an instance of AdBlocker
                AdBlocker adBlocker = new AdBlocker();

                // Create an instance of MyRequestHandler and pass the AdBlocker instance to it
                MyRequestHandler requestHandler = new MyRequestHandler(adBlocker);

                // Set the request handler for the chromiumWebBrowser1 control
                chromiumWebBrowser1.RequestHandler = requestHandler;
            }
            else
            {
                //BLANK
            }
            // Check if invocation is required
            if (textBox1.InvokeRequired)
            {
                // If it is required, invoke this method on the UI thread
                textBox1.Invoke(new MethodInvoker(delegate {
                    textBox1.Text = e.Address;
                }));
            }
            else
            {
                // If invocation is not required, update directly
                textBox1.Text = e.Address;
            }
        }

        private void chromiumWebBrowser1_TitleChanged(object sender, TitleChangedEventArgs e)
        {
            this.Name = e.Title;
        }

        private bool isPiPActive = false;

        private void pIPToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // Check if PiP mode is currently active
            if (isPiPActive)
            {
                // Exit PiP mode
                ExitPiPMode();
                pIPToolStripMenuItem1.Text = "PIP IS OFF";
            }
            else
            {
                // Enter PiP mode
                EnterPiPMode();
                pIPToolStripMenuItem1.Text = "PIP IS ON";
            }
        }

        private void openAdvanceSettingsMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menu1 menu1 = new menu1();
            menu1.ShowDialog();
        }

        // Import the necessary WinAPI functions
        [DllImport("user32.dll")]
        private static extern bool ClipCursor(ref RECT lpRect);

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        public static void LockCursorToBrowser(ChromiumWebBrowser browser)
        {
            Cursor.Hide(); // Hide the mouse cursor
            RECT rect;
            if (GetWindowRect(browser.Handle, out rect))
            {
                // Get the client area of the browser control
                var clientRect = browser.ClientRectangle;

                // Convert client coordinates to screen coordinates
                var clientPoint = browser.PointToScreen(clientRect.Location);

                // Set the rectangle to cover the entire client area of the browser control
                rect.Left = clientPoint.X;
                rect.Top = clientPoint.Y;
                rect.Right = clientPoint.X + clientRect.Width;
                rect.Bottom = clientPoint.Y + clientRect.Height;

                ClipCursor(ref rect);
            }
        }


        public static void UnlockCursor()
        {
            Cursor.Show(); // Show the mouse cursor
            RECT rect = new RECT(); // Initialize an empty RECT struct
            ClipCursor(ref rect); // Pass an empty RECT to release the cursor
        }

        private void lockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LockCursorToBrowser(chromiumWebBrowser1); // Assuming 'this' refers to an instance of Main_Browser_UI
        }

        private void chromiumWebBrowser1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                UnlockCursor();
            }
        }

        private void chromiumWebBrowser1_Enter(object sender, EventArgs e)
        {
            
        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
