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
    public partial class sutdainfo : Form
    {
        sutda sut = null;
        public sutdainfo()
        {
            InitializeComponent();
        }
        public sutdainfo(sutda sut)//setting폼 생성
        {
            InitializeComponent();
            this.sut = sut;
        }

        private void sutdainfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            sut.infocount = 0;
        }
    }
}
