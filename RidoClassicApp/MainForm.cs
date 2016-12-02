using System;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using DateTimeExtensions;
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
            ShowInstallData();

        }

        private void ShowInstallData()
        {
            if (Properties.Settings.Default.InstalledOn == DateTime.MinValue)
            {
                label1.Text = "First Run";
            }
            else
            {
                label1.Text = $"Application Installed {DateTime.Now.ToNaturalText(Properties.Settings.Default.InstalledOn)} ago\r\n";
            }
            label1.Text = $"Application Installed {DateTime.Now.ToNaturalText(Properties.Settings.Default.InstalledOn)} ago\r\n";
            label1.Text += $"Used {Properties.Settings.Default.TimesOpened} times.";
            if (Properties.Settings.Default.LastOpened > DateTime.MinValue)
            {
                label1.Text += $"{DateTime.Now.ToNaturalText(Properties.Settings.Default.LastOpened)} seconds ago\r\n";
            }
            label2.Text = $"{Assembly.GetExecutingAssembly().FullName} \r\n";

            toolTip1.SetToolTip(label2, Assembly.GetExecutingAssembly().Location);
            
            try
            {
               label3.Text = $"App info {Windows.ApplicationModel.Package.Current?.Id.FamilyName}";
            }
            catch
            {
                label3.Text = $"Classic Mode";
            }

            label4.Text = "Running in " + IntPtr.Size;
        }
    }
}
