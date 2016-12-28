using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Orders
{
    public partial class FormSettings : Form
    {
        public FormSettings()
        {
            InitializeComponent();
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
            txtServerName.Text = Properties.Settings.Default.server;
            txtDB.Text = Properties.Settings.Default.db;
            txtUser.Text = Properties.Settings.Default.user;
            txtPassword.Text = Properties.Settings.Default.password;
            checkTrustedCnn.Checked = Properties.Settings.Default.trusted_connection;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.server = txtServerName.Text;
            Properties.Settings.Default.db = txtDB.Text;
            Properties.Settings.Default.user = txtUser.Text;
            Properties.Settings.Default.password = txtPassword.Text;
            Properties.Settings.Default.trusted_connection = checkTrustedCnn.Checked;
            DB.refresh();
        }

        private void txtServerName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
