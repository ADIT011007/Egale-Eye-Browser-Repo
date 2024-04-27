using CefSharp;
using MaterialSkin.Controls;
using System;
using System.ComponentModel;
using System.Data;
using System.Device.Location;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Phoenix_Browser
{
    public partial class menu1 : MaterialForm
    {
        readonly MaterialSkin.MaterialSkinManager materialSkinManager;
        public menu1()
        {
            InitializeComponent();
            materialSkinManager = MaterialSkin.MaterialSkinManager.Instance;
            materialSkinManager.EnforceBackcolorOnAllComponents = true;
            materialSkinManager.AddFormToManage(this);

            materialSkinManager.ColorScheme = new MaterialSkin.ColorScheme(MaterialSkin.Primary.Indigo700, MaterialSkin.Primary.Blue500, MaterialSkin.Primary.Indigo700, MaterialSkin.Accent.Blue100, MaterialSkin.TextShade.WHITE);
        }

        private void menu1_Load(object sender, EventArgs e)
        {
            timer2.Start();
            timer2.Interval = 2000;
            textBox1.Text = "Longitude = " + Properties.Settings.Default.Longitude;
            textBox2.Text = "Latitude = " + Properties.Settings.Default.Latitude;
            if (Properties.Settings.Default.User_Prefs_Switch == false && Properties.Settings.Default.Cookies_session_Switch == false)
            {
                materialSwitch15.Checked = true;
            }
            else
            {
                materialSwitch15.Checked = false;
            }
            if (Properties.Settings.Default.low_resource_mode == true)
            {
                materialSwitch10.Checked = true;
            }
            else
            {
                materialSwitch10.Checked = false;
            }
            if (Properties.Settings.Default.RWU == "0")
            {
                materialTextBox1.Text = "Enter A value In terms Of Mb to set Ram usage Warning";
            }
            else
            {
                materialTextBox1.Text = "Ram Usage Limit Is Set To " + Properties.Settings.Default.RWU + "Mb";
            }


            if (Properties.Settings.Default.ignore_certificate_error_switch == true)
            {
                materialSwitch14.Checked = true;
            }
            else
            {
                materialSwitch14.Checked = false;
            }
            if (Properties.Settings.Default.print_preview_switch == true)
            {
                materialSwitch13.Checked = true;
            }
            else
            {
                materialSwitch13.Checked = false;
            }
            if (Properties.Settings.Default.javascrpts_switch == true)
            {
                materialSwitch12.Checked = true;
            }
            else
            {
                materialSwitch12.Checked = false;
            }
            if (Properties.Settings.Default.off_screen_rendering == true)
            {
                materialSwitch11.Checked = true;
            }
            else
            {
                materialSwitch11.Checked = false;
            }

            if (Properties.Settings.Default.ad_blocker_switch == true)
            {
                materialSwitch9.Checked = true;
            }
            else
            {
                materialSwitch9.Checked = false;
            }
            // Set the timer interval to 2 seconds (2000 milliseconds)
            timer1.Interval = 2000;
            // Attach the event handler for the timer
            timer1.Tick += timer1_Tick;
            // Start the timer
            timer1.Start();
            if (Properties.Settings.Default.settings_dark == true)
            {
                materialSwitch8.Checked = true;
                materialSkinManager.Theme = MaterialSkin.MaterialSkinManager.Themes.DARK;
            }
            else
            {
                materialSkinManager.Theme = MaterialSkin.MaterialSkinManager.Themes.LIGHT;
                materialSwitch8.Checked = false;

            }
            if (Properties.Settings.Default.Cookies_session_Switch == true)
            {
                materialSwitch7.Checked = true;
            }
            else
            {
                materialSwitch7.Checked = false;
            }
            if (Properties.Settings.Default.User_Prefs_Switch == true)
            {
                materialSwitch6.Checked = true;
            }
            else
            {
                materialSwitch6.Checked = false;
            }
            if (Properties.Settings.Default.Geo_Location_Switch == true)
            {
                materialSwitch5.Checked = true;
            }
            else
            {
                materialSwitch5.Checked = false;
            }
            if (Properties.Settings.Default.hardware_gpu_composting == true)
            {
                materialSwitch4.Checked = true;
            }
            else
            {
                materialSwitch4.Checked = false;
            }

            if (Properties.Settings.Default.hardware_v_sync == true)
            {
                materialSwitch3.Checked = true;
            }
            else
            {
                materialSwitch3.Checked = false;
            }
            if (Properties.Settings.Default.hardware_frame_Sceduling == true)
            {
                materialSwitch2.Checked = true;
            }
            else
            {
                materialSwitch2.Checked = false;
            }
            if (Properties.Settings.Default.hardware_acc_switch == true)
            {
                materialSwitch1.Checked = true;
            }
            else
            {
                materialSwitch1.Checked = false;
            }
            materialLabel1.Text = "CEF Commnit Hash :" + " " + Cef.CefCommitHash;
            materialLabel2.Text = "CEF Sharp Version :" + " " + Cef.CefSharpVersion;
            materialLabel3.Text = "CEF Version :" + " " + Cef.CefVersion;
            materialLabel4.Text = "Chromium Version :" + " " + Cef.ChromiumVersion;
        }

        private void materialSwitch1_CheckedChanged(object sender, EventArgs e)
        {
            if (materialSwitch1.Checked)
            {
                Properties.Settings.Default.hardware_acc_switch = true;
                Properties.Settings.Default.Save();
                materialSwitch4.Checked = true;
                Properties.Settings.Default.hardware_gpu_composting = true;
                Properties.Settings.Default.Save();
                materialSwitch2.Checked = true;
                materialSwitch3.Checked = true;
                Properties.Settings.Default.hardware_v_sync = true;
                Properties.Settings.Default.hardware_frame_Sceduling = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.hardware_acc_switch = false;
                Properties.Settings.Default.Save();
            }
        }

        private void materialSwitch2_CheckedChanged(object sender, EventArgs e)
        {
            if (materialSwitch2.Checked)
            {
                Properties.Settings.Default.hardware_frame_Sceduling = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.hardware_frame_Sceduling = false;
                Properties.Settings.Default.Save();
            }

        }

        private void materialSwitch3_CheckedChanged(object sender, EventArgs e)
        {
            if (materialSwitch3.Checked)
            {
                Properties.Settings.Default.hardware_v_sync = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.hardware_v_sync = false;
                Properties.Settings.Default.Save();
            }
        }

        private void materialSwitch4_CheckedChanged(object sender, EventArgs e)
        {
            if (materialSwitch4.Checked)
            {
                Properties.Settings.Default.hardware_gpu_composting = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.hardware_gpu_composting = false;
                Properties.Settings.Default.Save();
            }
        }

        private void materialSwitch5_CheckedChanged(object sender, EventArgs e)
        {
            if (materialSwitch5.Checked)
            {
                Properties.Settings.Default.Geo_Location_Switch = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.Geo_Location_Switch = false;
                Properties.Settings.Default.Save();
            }
        }

        private void materialSwitch6_CheckedChanged(object sender, EventArgs e)
        {
            if (materialSwitch6.Checked)
            {
                Properties.Settings.Default.User_Prefs_Switch = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.User_Prefs_Switch = false;
                Properties.Settings.Default.Save();
            }
        }

        private void materialSwitch7_CheckedChanged(object sender, EventArgs e)
        {
            if (materialSwitch7.Checked)
            {
                Properties.Settings.Default.Cookies_session_Switch = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.Cookies_session_Switch = false;
                Properties.Settings.Default.Save();
            }
        }
        private long GetFolderSize(string folderPath)
        {
            if (Directory.Exists(folderPath))
            {
                return Directory.GetFiles(folderPath, "*", SearchOption.AllDirectories)
                    .Select(file => new FileInfo(file).Length)
                    .Sum();
            }
            else
            {
                return 0;
            }
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            string folderName = "Egale Eye Browser"; // Replace with the actual folder name you want to work with
            string localAppDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string folderPath = Path.Combine(localAppDataFolder, folderName);

            if (Directory.Exists(folderPath))
            {
                long folderSizeInBytes = Directory.GetFiles(folderPath, "*", SearchOption.AllDirectories).Sum(file => new FileInfo(file).Length);
                long folderSizeInKilobytes = folderSizeInBytes / 1024;
                long folderSizeInMegabytes = folderSizeInKilobytes / 1024;

                // Display folder size in bytes, kilobytes, and megabytes
                MessageBox.Show($"Folder Size:\nBytes: {folderSizeInBytes} B\nKilobytes: {folderSizeInKilobytes} KB\nMegabytes: {folderSizeInMegabytes} MB");
            }
            else
            {
                MessageBox.Show("Folder not found in the AppData\\Local folder.");
            }
        }

        private string GetUserLocalAppDataFolder(string subfolder)
        {
            string localAppDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return Path.Combine(localAppDataFolder, subfolder);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            // Periodically update the sizes on the labels
            DisplayFolderSize("Egale Eye Browser\\Browser Data\\Session Storage", label1);
            DisplayFolderSize("Egale Eye Browser\\Browser Data\\Network", label2);
            DisplayFolderSize("Egale Eye Browser\\Browser Data\\IndexedDB", label3);
            DisplayFolderSize("Egale Eye Browser\\Browser Data\\Cache\\Cache_Data", label4);
            DisplayFolderSize("Egale Eye Browser\\Browser Data\\GPUCache", label5);
            DisplayFolderSize("Egale Eye Browser\\Browser Data", label6);
        }
        private void DisplayFolderSize(string subfolder, Label label)
        {
            string folderPath = GetUserLocalAppDataFolder(subfolder);

            // Check if the folder exists
            if (Directory.Exists(folderPath))
            {
                // Get the folder size in bytes
                long folderSize = Directory.GetFiles(folderPath, "*", SearchOption.AllDirectories)
                    .Select(file => new FileInfo(file).Length)
                    .Sum();

                // Convert bytes to a human-readable format (e.g., KB, MB, GB)
                string formattedSize = FormatSize(folderSize);
                label.Text = $"Size: {formattedSize}";
                timer1.Stop();
            }
            else
            {
                label.Text = "Folder not found.";
                timer1.Stop();
            }
        }

        private string FormatSize(long sizeInBytes)
        {
            string[] sizeSuffixes = { "B", "KB", "MB", "GB", "TB" };
            int i = 0;
            double size = sizeInBytes;

            while (size >= 1024 && i < sizeSuffixes.Length - 1)
            {
                size /= 1024;
                i++;
            }

            return $"{size:0.##} {sizeSuffixes[i]}";
        }

        private void DeleteFolder(string subfolder)
        {
            string folderPath = GetUserLocalAppDataFolder(subfolder);

            // Check if the folder exists
            if (Directory.Exists(folderPath))
            {
                Directory.Delete(folderPath, true);
                MessageBox.Show("Folder deleted successfully.");
            }
            else
            {
                MessageBox.Show("Folder not found.");
            }
        }

        private void materialButton1_Click_1(object sender, EventArgs e)
        {
            // Delete the first folder and update the label
            DeleteFolder("Egale Eye Browser\\Browser Data\\Session Storage");
            label1.Text = "Size: 0 B";
        }

        private void materialButton2_Click(object sender, EventArgs e)
        {
            // Delete the second folder and update the label
            DeleteFolder("Egale Eye Browser\\Browser Data\\Network");
            label2.Text = "Size: 0 B";
        }

        private void materialButton3_Click(object sender, EventArgs e)
        {
            // Delete the third folder and update the label
            DeleteFolder("Egale Eye Browser\\Browser Data\\IndexedDB");
            label3.Text = "Size: 0 B";
        }

        private void materialButton4_Click(object sender, EventArgs e)
        {
            // Delete the "Cache_Data" folder and update the label
            DeleteFolder("Egale Eye Browser\\Browser Data\\Cache\\Cache_Data");
            label4.Text = "Size: 0 B";
        }

        private void materialButton5_Click(object sender, EventArgs e)
        {
            // Delete the "Cache_Data" folder and update the label
            DeleteFolder("Egale Eye Browser\\Browser Data\\GPUCache");
            label4.Text = "Size: 0 B";
        }

        private void menu1_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Stop();
            timer2.Stop();
        }

        private void materialSwitch9_CheckedChanged(object sender, EventArgs e)
        {
            if (materialSwitch9.Checked)
            {
                Properties.Settings.Default.ad_blocker_switch = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.ad_blocker_switch = false;
                Properties.Settings.Default.Save();
            }
        }

        private void materialSwitch11_CheckedChanged(object sender, EventArgs e)
        {
            if (materialSwitch11.Checked)
            {
                Properties.Settings.Default.off_screen_rendering = true;
                Properties.Settings.Default.Save();

            }
            else
            {
                Properties.Settings.Default.off_screen_rendering = false;
                Properties.Settings.Default.Save();
            }
        }

        private void materialSwitch12_CheckedChanged(object sender, EventArgs e)
        {
            if (materialSwitch12.Checked)
            {
                Properties.Settings.Default.javascrpts_switch = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.javascrpts_switch = false;
                Properties.Settings.Default.Save();
            }
        }



        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void materialSwitch14_CheckedChanged(object sender, EventArgs e)
        {
            if (materialSwitch14.Checked)
            {
                Properties.Settings.Default.ignore_certificate_error_switch = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.ignore_certificate_error_switch = false;
                Properties.Settings.Default.Save();
            }
        }

        private void materialSwitch13_CheckedChanged(object sender, EventArgs e)
        {
            if (materialSwitch13.Checked)
            {
                Properties.Settings.Default.print_preview_switch = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.print_preview_switch = false;
                Properties.Settings.Default.Save();
            }
        }
        private void materialSwitch8_CheckedChanged_1(object sender, EventArgs e)
        {
            if (materialSwitch8.Checked)
            {
                Properties.Settings.Default.settings_dark = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.settings_dark = false;
                Properties.Settings.Default.Save();
            }
        }

        private void materialButton6_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.disposed_start_bool = true;
            Properties.Settings.Default.Save();
            Application.ExitThread();
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.first_start = true;
            Properties.Settings.Default.Save();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void materialButton6_Click_1(object sender, EventArgs e)
        {
            licenses lm = new licenses();
            lm.ShowDialog();
        }

        private void materialTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                int number;
                bool isNumeric = int.TryParse(materialTextBox1.Text, out number);

                // Check the result
                if (isNumeric)
                {
                    Properties.Settings.Default.RWU = materialTextBox1.Text;
                    Properties.Settings.Default.Save();
                }
                else
                {
                    MessageBox.Show("Please Enter A valid Number , Example : 1000");

                }

            }
        }

        private void materialButton6_Click_2(object sender, EventArgs e)
        {
            licenses licenses = new licenses();
            licenses.ShowDialog();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Attempt to parse the text to a double
                if (double.TryParse(textBox1.Text, out double latitude))
                {
                    // If parsing is successful, assign the parsed value to the setting
                    Properties.Settings.Default.Latitude = latitude;
                    Properties.Settings.Default.Save();
                    MessageBox.Show("Latitude saved: " + Properties.Settings.Default.Latitude);
                }
                else
                {
                    // Handle the case where parsing fails (e.g., invalid input)
                    MessageBox.Show("Invalid latitude value");
                }
            }
        }


        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Attempt to parse the text to a double
                if (double.TryParse(textBox2.Text, out double longitude))
                {
                    // If parsing is successful, assign the parsed value to the setting
                    Properties.Settings.Default.Longitude = longitude;
                    Properties.Settings.Default.Save();
                    MessageBox.Show("Longitude saved: " + longitude.ToString());
                }
                else
                {
                    // Handle the case where parsing fails (e.g., invalid input)
                    MessageBox.Show("Invalid Longitude value");
                }
            }
        }

        private async void timer2_Tick(object sender, EventArgs e)
        {
            textBox1.Text = Properties.Settings.Default.Longitude.ToString();
            textBox2.Text = Properties.Settings.Default.Latitude.ToString();
            await Task.Delay(3000);
            timer2.Stop();
        }

        private void materialButton7_Click(object sender, EventArgs e)
        {
            GeoCoordinateWatcher watcher = new GeoCoordinateWatcher();

            watcher.PositionChanged += (s, args) =>
            {
                // Retrieve the new position from the event arguments
                GeoCoordinate geoCoordinate = args.Position.Location;
                double latitude = geoCoordinate.Latitude;
                double longitude = geoCoordinate.Longitude;

                // Update application settings with new coordinates
                Properties.Settings.Default.Latitude = latitude;
                Properties.Settings.Default.Longitude = longitude;
                Properties.Settings.Default.Save();

                // Display confirmation message
                MessageBox.Show("Location updated successfully!", "Confirmation");
            };

            // Start the watcher to begin receiving position updates
            watcher.Start();
        }

        private void materialSwitch10_CheckedChanged(object sender, EventArgs e)
        {
            if (materialSwitch10.Checked)
            {
                Properties.Settings.Default.low_resource_mode = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.low_resource_mode = false;
                Properties.Settings.Default.Save();
            }
        }

        private void materialSwitch15_CheckedChanged(object sender, EventArgs e)
        {
            if (materialSwitch15.Checked)
            {
                Properties.Settings.Default.Cookies_session_Switch = false;
                Properties.Settings.Default.User_Prefs_Switch = false;
                Properties.Settings.Default.Save();
                materialSwitch6.Checked = false;
                materialSwitch7.Checked = false;
            }
            else
            {

                Properties.Settings.Default.Cookies_session_Switch = true;
                Properties.Settings.Default.User_Prefs_Switch = true;
                Properties.Settings.Default.Save();
                materialSwitch6.Checked = true;
                materialSwitch7.Checked = true;
            }
        }

    }
}
