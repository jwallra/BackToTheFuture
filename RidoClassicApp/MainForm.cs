using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RidoClassicApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            label1.Text = $"Application Installed {Properties.Settings.Default.InstalledOn.ToString()} \r\n";
            label1.Text += $"Opened {Properties.Settings.Default.TimesOpened}. Last time {Properties.Settings.Default.LastOpened} ";
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
