using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms; // Required for MessageBox

public class AdBlocker : IDisposable
{
    private HashSet<string> blockedDomains;

    public AdBlocker()
    {
  
        string blockListFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "siteblockerlist.txt");

        if (string.IsNullOrWhiteSpace(blockListFilePath) || !File.Exists(blockListFilePath))
        {
            throw new FileNotFoundException("Ad blocker list file not found or invalid path.");
        }

        blockedDomains = ReadBlockedDomainsFromFile(blockListFilePath);
    }

    private HashSet<string> ReadBlockedDomainsFromFile(string filePath)
    {
        HashSet<string> domains = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        try
        {
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                string domain = line.Trim();
                if (!string.IsNullOrWhiteSpace(domain))
                {
                    domains.Add(domain);
                }
            }
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"Error reading blocked domains file: {ex.Message}");
            throw; // Rethrow to indicate a critical error
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading blocked domains file: {ex.Message}");
        }

        return domains;
    }

    public bool ShouldBlockRequest(string url)
    {
        if (!string.IsNullOrEmpty(url) && Uri.TryCreate(url, UriKind.Absolute, out Uri uri))
        {
            string domain = uri.Host;
            if (blockedDomains.Contains(domain))
            {
                DisplayBlockedMessage(domain);
                return true;
            }
        }

        return false;
    }

    private void DisplayBlockedMessage(string blockedDomain)
    {
        MessageBox.Show($"Access to the site '{blockedDomain}' has been blocked because it has been identified as potentially harmful or malicious. Please ensure your safety by not attempting to bypass this restriction.", "Blocked Site", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }


    public void Dispose()
    {
        // You may add cleanup logic here if needed
    }
}
