using System;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace RidoClassicApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            label1.Text = $"Application Installed {Properties.Settings.Default.InstalledOn.ToString()} \r\n";
            label1.Text += $"Opened {Properties.Settings.Default.TimesOpened} times. Last time {Properties.Settings.Default.LastOpened}\r\n";
            
            label2.Text = $"Installed in {Assembly.GetExecutingAssembly().Location} \r\n";

            try
            {
                label3.Text += $"App info {Windows.ApplicationModel.Package.Current?.DisplayName}";
            }
            catch
            {
                label3.Text += $"App info not found";
            }



        }
    }
}
