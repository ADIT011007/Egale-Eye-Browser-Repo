using CefSharp;
using EnvDTE;
using Phoenix_Browser.Properties;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

public class CustomRequestHandler : IRequestHandler
{


    public bool OnBeforeResourceLoad(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, IRequestCallback callback)
    {
        // Check if the requested resource URL matches a pattern for an ad resource
        if (IsAdResource(request.Url))
        {
            // Cancel the request for the ad resource
            callback.Cancel();
            return true; // Indicates that the request has been handled
        }

        return false; // Allow the request to proceed
    }
    private bool IsAdResource(string url)
    {
        // Implement logic to determine if the URL is for an ad resource
        // You can use regular expressions or other methods to match ad URLs
        // Example: Check if the URL contains a known ad domain
        if (url.Contains("ad.doubleclick.net") ||
            url.Contains("https://doubleclick.net/") ||
            url.Contains("https://ad.doubleclick.net/") ||
            url.Contains("https://static.doubleclick.net") ||
            url.Contains("https://m.doubleclick.net") ||
            url.Contains("https://google-analytics.com") ||
            url.Contains("https://mediavisor.doubleclick.net") ||
            url.Contains("https://advertising.microsoft.com") ||
            url.Contains("https://bid.g.doubleclick.net") ||
            url.Contains("https://securepubads.g.doubleclick.net") ||
            url.Contains("https://partner.googleadservices.com") ||
            url.Contains("https://www.gstatic.com/adsense") ||
            url.Contains("https://www.google.com/adsense") ||
            url.Contains("https://www.google.com/adsense/domains/caf.js") ||
            url.Contains("https://www.google.com/adsense/domains/caf_components.js") ||
            url.Contains("https://www.google.com/adsense/domains/caf_components_fr.js") ||
            url.Contains("https://www.google.com/adsense/domains/caf_components_it.js") ||
            url.Contains("https://adservice.google.com") ||
            url.Contains("https://pagead2.googlesyndication.com") ||
            url.Contains("https://stats.g.doubleclick.net") ||
            url.Contains("https://www.googletagmanager.com") ||
            url.Contains("https://analytics.google.com") ||
            url.Contains("https://www.google-analytics.com") ||
            url.Contains("https://www.googletagservices.com") ||
            url.Contains("https://www.google.com/pagead/") ||
            url.Contains("https://adservice.google.com") ||
            url.Contains("https://www.google.com/ads/measurement/") ||
            url.Contains("https://ads.google.com"))
        {
            MessageBox.Show("ADVERTISEMENT BLOCKED", "Ad Blocker"); // Show message box instead of console output
            return true; // It's an ad resource
        }

        return false; // It's not an ad resource
    }

    // Implement other IRequestHandler methods as needed

    #region Unimplemented IRequestHandler methods


    // Implement the OnOpenUrlFromTab method
    public bool OnOpenUrlFromTab(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, string targetUrl, WindowOpenDisposition targetDisposition, bool userGesture)
    {
        
        return false; // Return false to allow the browser to handle the URL
    }

    // Implement the OnRenderProcessTerminated method
    public void OnRenderProcessTerminated(IWebBrowser chromiumWebBrowser, IBrowser browser, CefTerminationStatus status)
    {
        // Implement logic to handle render process termination
    }

    // Implement the OnRenderViewReady method
    public void OnRenderViewReady(IWebBrowser chromiumWebBrowser, IBrowser browser)
    {
        // Implement logic to handle when the render view is ready
    }


    // Implement the OnDocumentAvailableInMainFrame method
    public void OnDocumentAvailableInMainFrame(IWebBrowser chromiumWebBrowser, IBrowser browser)
    {
        // Implement logic to handle when a document becomes available in the main frame
    }

    // Implement the GetResourceRequestHandler method
    public IResourceRequestHandler GetResourceRequestHandler(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, bool isNavigation, bool isDownload, string requestInitiator, ref bool disableDefaultHandling)
    {
        return null; // Return null to use the default resource request handler
    }

    // Implement the GetAuthCredentials method
    public bool GetAuthCredentials(IWebBrowser chromiumWebBrowser, IBrowser browser, string originUrl, bool isProxy, string host, int port, string realm, string scheme, IAuthCallback callback)
    {
        return false; // Return false to indicate that authentication credentials are not provided
    }

    // Implement the OnBeforeBrowse method
    public bool OnBeforeBrowse(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, bool isRedirect, bool isMainFrame)
    {
        return false; // Allow the browse to proceed
    }


    public bool OnSelectClientCertificate(IWebBrowser chromiumWebBrowser, IBrowser browser, bool isProxy, string host, int port, X509Certificate2Collection certificates, ISelectClientCertificateCallback callback)
    {
        // Implement logic to select a client certificate for authentication
        return false; // Placeholder return value indicating no client certificate is selected
    }

    public bool OnCertificateError(IWebBrowser chromiumWebBrowser, IBrowser browser, CefErrorCode errorCode, string requestUrl, ISslInfo sslInfo, IRequestCallback callback)
    {
        var propertiesSettings = Settings.Default;

        // Ensure that the settings are accessible and reflect the intended value
        // Consider adding debugging statements to verify this
        if (propertiesSettings.ignore_certificate_error_switch == true)
        {
            // If the user has chosen to ignore certificate errors, allow the website to load
            return false;
        }
        else
        {
            // Display a message box to ask the user whether to continue or not
            DialogResult result = MessageBox.Show("Error With Certificate. Do you want to continue?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

            // Ensure that the message box is displayed
            // Consider adding debugging statements to verify this

            // Depending on the user's choice, return the appropriate value
            if (result == DialogResult.Yes)
            {
                // If the user chooses to continue, allow the website to load
                return false;
            }
            else
            {
                // If the user chooses not to continue, block the website from loading
                return true;
            }
        }
    }

}
    #endregion
