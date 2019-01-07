using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class PopUp3 : Form
    {
        public PopUp3()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            popForm2.ActiveForm.Disposed += new EventHandler(closeForm2);
            this.Visible = false;
        }

        private void closeForm2(object sender, EventArgs e)
        {
            try
            {
                popForm2.ActiveForm.Dispose();              
            }
            catch
            {
                //throw new NotImplementedException();
            }
        }
    }
}
