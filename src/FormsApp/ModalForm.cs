using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormsApp
{
    public partial class ModalForm : Form
    {
        public ModalForm()
        {
            InitializeComponent();

            //custom
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;

            //START.Location = new Point(50, 150);
            var border = 50;
            START.Left = border;
            START.Width = Screen.PrimaryScreen.WorkingArea.Width - (border * 2);
        }

        private void START_Click(object sender, EventArgs e)
        {

        }
    }
}
