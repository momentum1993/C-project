using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace teamsum
{
    public partial class sa : Form
    {
        Form1 main = null;
        public sa()
        {
            InitializeComponent();
        }
        public sa(Form1 main)//스도쿠 생성
        {
            InitializeComponent();
            this.main = main;

        }

        private void sa_FormClosed(object sender, FormClosedEventArgs e)
        {
            main.Visible = true;
        }
    }
}
