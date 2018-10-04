using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Shisen_Sho;

namespace teamsum
{
    public partial class Form1 : Form
    {
        string gamename = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.섯다;
            gamename = "섯다";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.스도큐;
            gamename = "스도쿠";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.사천성;
            gamename = "사천성";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.벽돌깨기;
            gamename = "벽돌";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.제목_없음;
            gamename = "";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (gamename == "섯다")
            {
                sutda sut = new sutda(this);
                sut.Show();
                this.Visible = false;
                pictureBox1.Image = Properties.Resources.제목_없음;
                gamename = "";
            }
            else if (gamename == "스도쿠")
            {
                sudoku sudo = new sudoku(this);
                sudo.Show();
                this.Visible = false;
                pictureBox1.Image = Properties.Resources.제목_없음;
                gamename = "";
            }
            else if (gamename == "사천성")
            {
                FrmMain s = new FrmMain();
                s.Show();
                this.Visible = false;
                pictureBox1.Image = Properties.Resources.제목_없음;
                gamename = "";
            }
            else if (gamename == "벽돌")
            {
                wall wal = new wall(this);
                wal.Show();

                this.Visible = false;
                pictureBox1.Image = Properties.Resources.제목_없음;
                gamename = "";
            }
        }
    }
}
