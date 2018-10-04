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
    public partial class sutdaset : Form
    {
        sutda sut = null;
        public sutdaset()
        {
            InitializeComponent();
        }
        public sutdaset(sutda sut)//setting폼 생성
        {
            InitializeComponent();
            this.sut = sut;
        }
        private void setStart_Click(object sender, EventArgs e)
        {
            if (setID.Text == "")
                MessageBox.Show("ID를 설정하지 않았습니다");
            else if (setCoin.Text == "")
                MessageBox.Show("Coin을 설정하지 않았습니다");
            else
            {
                try
                {
                    sut.label2.Text = setID.Text;
                    sut.playercoin = Int32.Parse(setCoin.Text);
                    sut.dealercoin = Int32.Parse(setCoin.Text);
                    sut.start.Enabled = true;
                    sut.printcoin();
                    this.Close();
                }
                catch (Exception)
                {
                    sut.label2.Text = "Player";
                    MessageBox.Show("입력값을 확인하세요");
                }
            }
        }
    }
}
