using CefSharp;
using CefSharp.Handler;
using Phoenix_Browser.Properties;
using System.Windows.Forms;

public class MyRequestHandler : RequestHandler
{
    private AdBlocker adBlocker;

    public MyRequestHandler(AdBlocker adBlocker)
    {
        this.adBlocker = adBlocker;
    }

    protected override bool OnBeforeBrowse(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, bool userGesture, bool isRedirect)
    {
        string url = request.Url;
        if (adBlocker.ShouldBlockRequest(url))
        {
            // Cancel the request if it should be blocked
            return true;
        }
        return false;
    }

    protected override bool OnCertificateError(IWebBrowser browserControl, IBrowser browser, CefErrorCode errorCode, string requestUrl, ISslInfo sslInfo, IRequestCallback callback)
    {
        var propertiesSettings = Settings.Default;

        // Display a message box to ask the user whether to continue or not
        DialogResult result = MessageBox.Show("Error With Certificate. Do you want to continue?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

        // Ensure that the message box is displayed
        // Consider adding debugging statements to verify this

        // Depending on the user's choice, return the appropriate value
        if (result == DialogResult.Yes)
        {
            // If the user chooses to continue, allow the website to load
            callback.Continue(true);
            return true;
        }
        else
        {
            // If the user chooses not to continue, block the website from loading
            callback.Continue(false);
            return true;
        }
    }
}
