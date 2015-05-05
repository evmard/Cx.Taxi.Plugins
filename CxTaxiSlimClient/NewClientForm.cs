using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CxTaxiSlimClient.Properties;

namespace CxTaxiSlimClient
{
    public partial class NewClientForm : Form
    {
        public NewClientForm(string phone)
        {
            InitializeComponent();
            tePhone.Text = phone;
        }

        public string GetPhone()
        {
            return tePhone.Text;
        }

        public string GetName()
        {
            return teName.Text;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tePhone.Text))
            {
                MessageBox.Show(
                    this,
                    Resources.MainForm_PhoneCantBeEmpty,
                    Resources.NewClientForm_Title_NewClient,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
