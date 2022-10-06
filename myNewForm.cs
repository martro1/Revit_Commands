using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Moje
{
    public partial class myNewForm : Form
    {
        public myNewForm()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        public bool isHorizontal()
        {
            if(rdoHorizontal.Checked)
            {
                return true;
            }
            else
            {
                return false;
            }
         
        }

        public double getDistance()
        {
            double d;
            Double.TryParse(txtDistance.Text, out d);
            return d;
        }
    }
}
