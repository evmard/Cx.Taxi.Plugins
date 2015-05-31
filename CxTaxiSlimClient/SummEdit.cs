using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CxTaxiSlimClient
{
    public class SummEdit : TextBox
    {
        public SummEdit():base ()
        {
            MaxValue = 9999999;
        }

        public double MaxValue { get; set; }
        
        protected override void OnKeyPress(KeyPressEventArgs e)
        {

            if (!char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '.' && e.KeyChar != 8)
            {
                e.Handled = true;
                return;
            }

            if ((e.KeyChar == ',' || e.KeyChar == '.'))
            {
                if (Text.Contains(',') || Text.Contains('.'))
                {
                    e.Handled = true;
                    return;
                }
                else if (string.IsNullOrWhiteSpace(Text))
                {
                    AppendText("0");
                }

            }

            if (e.KeyChar == '.')
            {
                this.AppendText(",");
                e.Handled = true;
                return;
            }
            
            if (e.KeyChar != 8 && double.Parse(Text + e.KeyChar) > MaxValue)
                    e.Handled = true;
            
        }
    }
}
