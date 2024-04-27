using MaterialSkin.Controls;
using System;
using System.Windows.Forms;

namespace Phoenix_Browser
{
    public partial class Form3 : MaterialForm
    {
        readonly MaterialSkin.MaterialSkinManager materialSkinManager;
        public Form3()
        {
            materialSkinManager = MaterialSkin.MaterialSkinManager.Instance;
            materialSkinManager.EnforceBackcolorOnAllComponents = true;
            materialSkinManager.AddFormToManage(this);
            InitializeComponent();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;
            button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            button3.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.settings_dark == true)
            {
                materialSkinManager.Theme = MaterialSkin.MaterialSkinManager.Themes.DARK;
                materialSkinManager.ColorScheme = new MaterialSkin.ColorScheme(MaterialSkin.Primary.Grey900, MaterialSkin.Primary.Grey800, MaterialSkin.Primary.Grey700, MaterialSkin.Accent.Orange700, MaterialSkin.TextShade.WHITE);
            }
            else
            {
                materialSkinManager.Theme = MaterialSkin.MaterialSkinManager.Themes.LIGHT;
                materialSkinManager.ColorScheme = new MaterialSkin.ColorScheme(MaterialSkin.Primary.Indigo700, MaterialSkin.Primary.Blue500, MaterialSkin.Primary.Indigo700, MaterialSkin.Accent.Blue100, MaterialSkin.TextShade.WHITE);
            }
            button3.Enabled = false;
            Fast_Timer.Start();
            SLow_Timer.Start();
            SLow_Timer.Interval = 5000; // 5 seconds in milliseconds
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.WindowState = FormWindowState.Minimized;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Fast_Timer_Tick(object sender, EventArgs e)
        {
            progressBar1.Value = Properties.Settings.Default.progress_bar_dwn;
            label12.Text = Properties.Settings.Default.Time_Remaining;
            label6.Text = Properties.Settings.Default.Speed_unit;
            label9.Text = Properties.Settings.Default.Data_Recived_Size;
        }

        private void SLow_Timer_Tick(object sender, EventArgs e)
        {
            label1.Text = Properties.Settings.Default.File_Name;
            label3.Text = Properties.Settings.Default.Total_Size_Unit;
            textBox1.Text = Properties.Settings.Default.Download_Saved_Loc;
            textBox2.Text = Properties.Settings.Default.Download_URL;


            string labelText = Properties.Settings.Default.dwn_type;
            string[] parts = labelText.Split('/');

            if (parts.Length == 2)
            {
                string beforeSlash = parts[0];
                string afterSlash = parts[1];

                // Assuming you have two labels named label1 and label2
                label4.Text = "Download Type : " + beforeSlash;
                label2.Text = "Download Format : " + afterSlash;
            }
        }
    }
}
