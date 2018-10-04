using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
namespace teamsum
{
    public partial class sutda : Form
    {
        public int infocount = 0;
        sutdainfo info;
        public int playercoin = 10;
        public int dealercoin = 10;
        int money = 0;
        Image[] ab = new Image[20];
        Image hide;
        int p1, p2, d1, d2;
        int pRank, dRank;
        bool dealerCallState;
        Form1 main = null;
        public sutda()
        {
            InitializeComponent();
        }
        public sutda(Form1 main)//섯다폼 생성
        {
            InitializeComponent();
            this.main = main;
            btnCall.Enabled = false;
            btnCheck.Enabled = false;
            btnDie.Enabled = false;
            start.Enabled = false;
            printcoin();
        }
        private void sutda_Load(object sender, EventArgs e)
        {

        }
        public void printcoin()
        {
            tbPcoin.Text = playercoin.ToString();
            tbDcoin.Text = dealercoin.ToString();
            tbMoney.Text = money.ToString();
        }
        public void micoin()
        {
            playercoin--;
            dealercoin--;
            money = money + 2;
        }

        public void die()
        {
            btnDie.Enabled = false;
            btnCall.Enabled = false;
            btnCheck.Enabled = false;
            printcoin();
            dpb2.Image = hide;
            start.Enabled = true;
            tbPlayer.Text = checkCard(p1, p2);
            tbDealer.Text = checkCard(d1, d2);
        }
        public void dealerCall()
        {
            if (dealercoin == 0)
            {
                dealerCheck();
            }
            else if (dealercoin == 1)
            {
                dealerCheck();
            }
            else
            {
                tbDealer.Text = "콜";
                dealercoin -= 2;
                money += 2;
                printcoin();
                btnCall.Enabled = true;
                btnCheck.Enabled = true;
                btnDie.Enabled = true;
                dealerCallState = true;
            }

        }
        public void dealerCheck()
        {
            if (dealercoin == 0)
            { }
            else
            {
                dealercoin--;
                money++;

            }
            tbDealer.Text = "체크";
            dealerCallState = false;
            btnCheck.Enabled = true;
            btnDie.Enabled = true;
            printcoin();
        }
        public void dealerDie()
        {
            MessageBox.Show("Dealer Die");
            tbWinner.Text = "Player";
            playercoin += money;
            money = 0;
            die();
        }
        public void dealerBatting(int drank)
        {
            Thread.Sleep(2000);
            Random ra = new Random();

            int dealerbet;
            dealerbet = ra.Next(0, 19);
            //0~12까지 떙, 13~18까지 족보, 19~28까지 끗

            if (drank == 100)
            {
                dealerCall();
            }
            else if (drank < 3)//광땡
            {
                dealerCall();
            }
            else if (drank < 8)//장땡~6땡
            {
                if (dealerbet < 18)
                    dealerCall();
                else
                    dealerCheck();
            }
            else if (drank < 13)//12가 1땡 - 5땡~1땡
            {
                if (p1 == 0 || p1 == 1 || p1 == 1 || p1 == 4 || p1 == 5 || p1 == 6 || p1 == 7 || p1 == 14 || p1 == 15 || p1 == 16 || p1 == 17 || p1 == 18 || p1 == 19)
                {//플레이어 1번째 패가 1,3,4,8,9,10 월일때
                    if (dealerbet < 15)
                        dealerCall();
                    else
                        dealerCheck();
                }
                else//나머지 월일때
                {
                    if (dealerbet < 16)
                        dealerCall();
                    else
                        dealerCheck();
                }
            }
            else if (drank < 17)//알리~장삥
            {
                if (p1 == 0 || p1 == 1 || p1 == 6 || p1 == 7 || p1 == 16 || p1 == 17 || p1 == 18 || p1 == 19)
                {
                    if (dealerbet < 12)
                        dealerCall();
                    else if (dealerbet < 17)
                        dealerCheck();
                    else
                        dealerDie();
                }
                else if (p1 == 4 || p1 == 5 || p1 == 14 || p1 == 15 || p1 == 2 || p1 == 3)
                {
                    if (dealerbet < 12)
                        dealerCall();
                    else if (dealerbet < 18)
                        dealerCheck();
                    else
                        dealerDie();
                }
                else
                {
                    if (dealerbet < 14)
                        dealerCall();
                    else if (dealerbet < 19)
                        dealerCheck();
                    else
                        dealerDie();
                }
            }
            else if (drank < 20)//~갑오
            {
                if (p1 == 0 || p1 == 1 || p1 == 6 || p1 == 7 || p1 == 16 || p1 == 17 || p1 == 18 || p1 == 19)
                {
                    if (dealerbet < 10)
                        dealerCall();
                    else if (dealerbet < 15)
                        dealerCheck();
                    else
                        dealerDie();
                }
                else if (p1 == 4 || p1 == 5 || p1 == 14 || p1 == 15 || p1 == 2 || p1 == 3)
                {
                    if (dealerbet < 10)
                        dealerCall();
                    else if (dealerbet < 16)
                        dealerCheck();
                    else
                        dealerDie();
                }
                else
                {
                    if (dealerbet < 11)
                        dealerCall();
                    else if (dealerbet < 17)
                        dealerCheck();
                    else
                        dealerDie();
                }
            }
            else if (drank < 25)//~4끗
            {
                if (p1 == 0 || p1 == 1 || p1 == 6 || p1 == 7 || p1 == 16 || p1 == 17 || p1 == 18 || p1 == 19)
                {
                    if (dealerbet < 4)
                        dealerCall();
                    else if (dealerbet < 11)
                        dealerCheck();
                    else
                        dealerDie();
                }
                else if (p1 == 4 || p1 == 5 || p1 == 14 || p1 == 15 || p1 == 2 || p1 == 3)
                {
                    if (dealerbet < 5)
                        dealerCall();
                    else if (dealerbet < 12)
                        dealerCheck();
                    else
                        dealerDie();
                }
                else
                {
                    if (dealerbet < 6)
                        dealerCall();
                    else if (dealerbet < 13)
                        dealerCheck();
                    else
                        dealerDie();
                }
            }
            else//~망통
            {
                if (p1 == 0 || p1 == 1 || p1 == 6 || p1 == 7 || p1 == 16 || p1 == 17 || p1 == 18 || p1 == 19)
                {
                    if (dealerbet < 2)
                        dealerCall();
                    else if (dealerbet < 6)
                        dealerCheck();
                    else
                        dealerDie();
                }
                else if (p1 == 4 || p1 == 5 || p1 == 14 || p1 == 15 || p1 == 2 || p1 == 3)
                {
                    if (dealerbet < 2)
                        dealerCall();
                    else if (dealerbet < 7)
                        dealerCheck();
                    else
                        dealerDie();
                }
                else
                {
                    if (dealerbet < 3)
                        dealerCall();
                    else if (dealerbet < 8)
                        dealerCheck();
                    else
                        dealerDie();
                }
            }
        }
        public void result()
        {
            Thread.Sleep(300);
            dpb2.Image = hide;
            tbPlayer.Text = checkCard(p1, p2);
            tbDealer.Text = checkCard(d1, d2);
            if (pRank == 100 || dRank == 100)
            {
                MessageBox.Show("49 재경기!");
                playercoin++;
                dealercoin++;
                money -= 2;
                cardSetting();
            }
            else if (pRank < dRank)
            {
                tbWinner.Text = "Player!";
                playercoin += money;
                money = 0;
            }
            else if (dRank < pRank)
            {
                tbWinner.Text = "Dealer";
                dealercoin += money;
                money = 0;
            }
            else
            {
                MessageBox.Show("Draw 재경기!");
                playercoin++;
                dealercoin++;
                money -= 2;
                cardSetting();
            }

            printcoin();
            start.Enabled = true;
        }
        public void cardSetting()
        {
            if (playercoin == 0)
                MessageBox.Show("코인이 없습니다. YOU LOSE!");
            else if (dealercoin == 0)
                MessageBox.Show("딜러가 가진 코인이 없습니다 YOU WIN!");
            else
            {
                start.Enabled = false;
                dealerCallState = false;
                tbDealer.Clear();
                tbPlayer.Clear();
                tbWinner.Clear();
                btnCall.Enabled = true;
                btnCheck.Enabled = true;
                btnDie.Enabled = true;
                playercoin--;
                dealercoin--;
                money += 2;
                printcoin();
                Random r = new Random();
                int[] randomcard = new int[4];
                for (int i = 0; i < 4; i++)
                {
                    randomcard[i] = r.Next(0, 19);
                    for (int j = 0; j < i; j++) //중복제거를 위한 for문 
                    {
                        if (randomcard[i] == randomcard[j])
                        {
                            i--;
                        }
                    }
                }
                //1월
                ab[0] = Properties.Resources.aa;
                ab[1] = Properties.Resources.ab;
                //2월
                ab[2] = Properties.Resources.ba;
                ab[3] = Properties.Resources.bb;
                //3월
                ab[4] = Properties.Resources.ca;
                ab[5] = Properties.Resources.cb;
                //4월
                ab[6] = Properties.Resources.da;
                ab[7] = Properties.Resources.db;
                //5월
                ab[8] = Properties.Resources.ea;
                ab[9] = Properties.Resources.eb;
                //6월
                ab[10] = Properties.Resources.fa;
                ab[11] = Properties.Resources.fb;
                //7월
                ab[12] = Properties.Resources.ga;
                ab[13] = Properties.Resources.gb;
                //8월
                ab[14] = Properties.Resources.ha;
                ab[15] = Properties.Resources.hb;
                //9월
                ab[16] = Properties.Resources.ia;
                ab[17] = Properties.Resources.ib;
                //10월
                ab[18] = Properties.Resources.ja;
                ab[19] = Properties.Resources.jb;


                ppb1.Image = ab[randomcard[0]];
                ppb2.Image = ab[randomcard[1]];
                dpb1.Image = ab[randomcard[2]];
                dpb2.Image = Properties.Resources.back;
                hide = ab[randomcard[3]];
                //start.Enabled = false;
                p1 = randomcard[0];
                p2 = randomcard[1];
                d1 = randomcard[2];
                d2 = randomcard[3];
                pRank = checkRank(p1, p2);
                dRank = checkRank(d1, d2);
            }
        }
        public string checkCard(int p1, int p2)//플레이어 족보 체크
        {
            string grade = "";
            if ((p1 == 4 && p2 == 14) || (p1 == 14 && p2 == 4))
            {
                grade = "38광땡";
            }
            else if ((p1 == 0 && p2 == 14) || (p1 == 14 && p2 == 0))
            {
                grade = "18광땡";
            }
            else if ((p1 == 0 && p2 == 4) || (p1 == 4 && p2 == 0))
            {
                grade = "13광땡";
            }
            else if ((p1 == 0 && p2 == 1) || (p1 == 1 && p2 == 0))
            {
                grade = "1땡";
            }
            else if ((p1 == 2 && p2 == 3) || (p1 == 3 && p2 == 2))
            {
                grade = "2땡";
            }
            else if ((p1 == 4 && p2 == 5) || (p1 == 5 && p2 == 4))
            {
                grade = "3땡";
            }
            else if ((p1 == 6 && p2 == 7) || (p1 == 7 && p2 == 6))
            {
                grade = "4땡";
            }
            else if ((p1 == 8 && p2 == 9) || (p1 == 9 && p2 == 8))
            {
                grade = "5땡";
            }
            else if ((p1 == 10 && p2 == 11) || (p1 == 11 && p2 == 10))
            {
                grade = "6땡";
            }
            else if ((p1 == 12 && p2 == 13) || (p1 == 13 && p2 == 12))
            {
                grade = "7땡";
            }
            else if ((p1 == 14 && p2 == 15) || (p1 == 15 && p2 == 14))
            {
                grade = "8땡";
            }
            else if ((p1 == 16 && p2 == 17) || (p1 == 17 && p2 == 16))
            {
                grade = "9땡";
            }
            else if ((p1 == 18 && p2 == 19) || (p1 == 19 && p2 == 18))
            {
                grade = "장땡";
            }
            else if ((p1 == 0 && p2 == 2) || (p1 == 0 && p2 == 3) || (p1 == 1 && p2 == 2) || (p1 == 1 && p2 == 3) || (p1 == 2 && p2 == 0) || (p1 == 3 && p2 == 0) || (p1 == 2 && p2 == 1) || (p1 == 3 && p2 == 1))
            {
                grade = "알리";
            }
            else if ((p1 == 0 && p2 == 6) || (p1 == 0 && p2 == 7) || (p1 == 1 && p2 == 6) || (p1 == 1 && p2 == 7) || (p1 == 6 && p2 == 0) || (p1 == 7 && p2 == 0) || (p1 == 6 && p2 == 1) || (p1 == 7 && p2 == 1))
            {
                grade = "독사";
            }
            else if ((p1 == 0 && p2 == 16) || (p1 == 0 && p2 == 17) || (p1 == 1 && p2 == 16) || (p1 == 1 && p2 == 17) || (p1 == 16 && p2 == 0) || (p1 == 17 && p2 == 0) || (p1 == 16 && p2 == 1) || (p1 == 17 && p2 == 1))
            {
                grade = "구삥";
            }
            else if ((p1 == 0 && p2 == 18) || (p1 == 0 && p2 == 19) || (p1 == 1 && p2 == 18) || (p1 == 1 && p2 == 19) || (p1 == 18 && p2 == 0) || (p1 == 19 && p2 == 0) || (p1 == 2 && p2 == 1) || (p1 == 18 && p2 == 19))
            {
                grade = "장삥";
            }
            else if ((p1 == 6 && p2 == 18) || (p1 == 6 && p2 == 19) || (p1 == 7 && p2 == 18) || (p1 == 7 && p2 == 19) || (p1 == 18 && p2 == 6) || (p1 == 18 && p2 == 7) || (p1 == 19 && p2 == 6) || (p1 == 19 && p2 == 7))
            {
                grade = "장사";
            }
            else if ((p1 == 6 && p2 == 10) || (p1 == 7 && p2 == 10) || (p1 == 6 && p2 == 11) || (p1 == 7 && p2 == 11) || (p1 == 10 && p2 == 6) || (p1 == 11 && p2 == 6) || (p1 == 10 && p2 == 7) || (p1 == 11 && p2 == 7))
            {
                grade = "세륙";
            }
            else if ((p1 == 0 && p2 == 15) || (p1 == 1 && p2 == 15) || (p1 == 1 && p2 == 14) || (p1 == 15 && p2 == 0) || (p1 == 14 && p2 == 1) || (p1 == 15 && p2 == 1) || (p1 == 2 && p2 == 12) || (p1 == 2 && p2 == 13) || (p1 == 3 && p2 == 12) || (p1 == 3 && p2 == 13) || (p1 == 12 && p2 == 2) || (p1 == 13 && p2 == 2) || (p1 == 12 && p2 == 3) || (p1 == 13 && p2 == 3) || (p1 == 4 && p2 == 10) || (p1 == 4 && p2 == 11) || (p1 == 5 && p2 == 10) || (p1 == 5 && p2 == 11) || (p1 == 10 && p2 == 4) || (p1 == 11 && p2 == 4) || (p1 == 10 && p2 == 5) || (p1 == 11 && p2 == 5) || (p1 == 6 && p2 == 8) || (p1 == 6 && p2 == 9) || (p1 == 7 && p2 == 8) || (p1 == 7 && p2 == 9) || (p1 == 8 && p2 == 6) || (p1 == 8 && p2 == 7) || (p1 == 9 && p2 == 6) || (p1 == 9 && p2 == 7) || (p1 == 16 && p2 == 18) || (p1 == 16 && p2 == 19) || (p1 == 17 && p2 == 18) || (p1 == 17 && p2 == 19) || (p1 == 18 && p2 == 16) || (p1 == 19 && p2 == 16) || (p1 == 18 && p2 == 14) || (p1 == 19 && p2 == 17))
            {
                grade = "갑오";
            }
            else if ((p1 == 0 && p2 == 12) || (p1 == 1 && p2 == 12) || (p1 == 1 && p2 == 13) || (p1 == 0 && p2 == 13) || (p1 == 12 && p2 == 0) || (p1 == 12 && p2 == 1) || (p1 == 13 && p2 == 1) || (p1 == 13 && p2 == 0) || (p1 == 2 && p2 == 10) || (p1 == 2 && p2 == 11) || (p1 == 3 && p2 == 10) || (p1 == 3 && p2 == 11) || (p1 == 10 && p2 == 2) || (p1 == 11 && p2 == 2) || (p1 == 10 && p2 == 3) || (p1 == 11 && p2 == 3) || (p1 == 4 && p2 == 8) || (p1 == 4 && p2 == 9) || (p1 == 5 && p2 == 8) || (p1 == 5 && p2 == 9) || (p1 == 8 && p2 == 4) || (p1 == 8 && p2 == 5) || (p1 == 9 && p2 == 4) || (p1 == 9 && p2 == 5) || (p1 == 14 && p2 == 18) || (p1 == 14 && p2 == 19) || (p1 == 15 && p2 == 18) || (p1 == 15 && p2 == 19) || (p1 == 18 && p2 == 14) || (p1 == 19 && p2 == 14) || (p1 == 18 && p2 == 15) || (p1 == 19 && p2 == 15))
            {
                grade = "8끗";
            }
            else if ((p1 == 0 && p2 == 10) || (p1 == 0 && p2 == 11) || (p1 == 1 && p2 == 10) || (p1 == 1 && p2 == 11) || (p1 == 10 && p2 == 0) || (p1 == 10 && p2 == 1) || (p1 == 11 && p2 == 1) || (p1 == 11 && p2 == 0) || (p1 == 2 && p2 == 8) || (p1 == 2 && p2 == 9) || (p1 == 3 && p2 == 8) || (p1 == 3 && p2 == 9) || (p1 == 8 && p2 == 2) || (p1 == 9 && p2 == 2) || (p1 == 8 && p2 == 3) || (p1 == 9 && p2 == 3) || (p1 == 4 && p2 == 6) || (p1 == 4 && p2 == 7) || (p1 == 5 && p2 == 6) || (p1 == 5 && p2 == 7) || (p1 == 6 && p2 == 4) || (p1 == 6 && p2 == 5) || (p1 == 7 && p2 == 4) || (p1 == 7 && p2 == 5) || (p1 == 12 && p2 == 18) || (p1 == 12 && p2 == 19) || (p1 == 13 && p2 == 18) || (p1 == 13 && p2 == 19) || (p1 == 18 && p2 == 12) || (p1 == 19 && p2 == 12) || (p1 == 18 && p2 == 13) || (p1 == 19 && p2 == 13) || (p1 == 14 && p2 == 16) || (p1 == 14 && p2 == 17) || (p1 == 15 && p2 == 16) || (p1 == 15 && p2 == 17) || (p1 == 16 && p2 == 14) || (p1 == 16 && p2 == 15) || (p1 == 17 && p2 == 14) || (p1 == 17 && p2 == 15))
            {
                grade = "7끗";
            }
            else if ((p1 == 0 && p2 == 8) || (p1 == 1 && p2 == 8) || (p1 == 1 && p2 == 9) || (p1 == 0 && p2 == 9) || (p1 == 8 && p2 == 0) || (p1 == 8 && p2 == 1) || (p1 == 9 && p2 == 1) || (p1 == 9 && p2 == 0) || (p1 == 2 && p2 == 6) || (p1 == 2 && p2 == 7) || (p1 == 3 && p2 == 6) || (p1 == 3 && p2 == 7) || (p1 == 6 && p2 == 2) || (p1 == 7 && p2 == 2) || (p1 == 6 && p2 == 3) || (p1 == 7 && p2 == 3) || (p1 == 10 && p2 == 18) || (p1 == 10 && p2 == 19) || (p1 == 11 && p2 == 18) || (p1 == 11 && p2 == 19) || (p1 == 18 && p2 == 10) || (p1 == 18 && p2 == 11) || (p1 == 19 && p2 == 10) || (p1 == 19 && p2 == 11) || (p1 == 12 && p2 == 16) || (p1 == 12 && p2 == 17) || (p1 == 13 && p2 == 16) || (p1 == 13 && p2 == 17) || (p1 == 16 && p2 == 12) || (p1 == 16 && p2 == 13) || (p1 == 17 && p2 == 12) || (p1 == 17 && p2 == 13))
            {
                grade = "6끗";
            }
            else if ((p1 == 2 && p2 == 4) || (p1 == 2 && p2 == 5) || (p1 == 3 && p2 == 4) || (p1 == 3 && p2 == 5) || (p1 == 4 && p2 == 2) || (p1 == 4 && p2 == 3) || (p1 == 5 && p2 == 2) || (p1 == 5 && p2 == 3) || (p1 == 8 && p2 == 18) || (p1 == 8 && p2 == 19) || (p1 == 9 && p2 == 18) || (p1 == 9 && p2 == 19) || (p1 == 18 && p2 == 8) || (p1 == 18 && p2 == 9) || (p1 == 19 && p2 == 8) || (p1 == 19 && p2 == 9) || (p1 == 10 && p2 == 16) || (p1 == 10 && p2 == 17) || (p1 == 11 && p2 == 16) || (p1 == 11 && p2 == 17) || (p1 == 16 && p2 == 10) || (p1 == 16 && p2 == 11) || (p1 == 17 && p2 == 10) || (p1 == 17 && p2 == 11) || (p1 == 12 && p2 == 14) || (p1 == 12 && p2 == 15) || (p1 == 13 && p2 == 14) || (p1 == 13 && p2 == 15) || (p1 == 14 && p2 == 12) || (p1 == 14 && p2 == 13) || (p1 == 15 && p2 == 12) || (p1 == 15 && p2 == 13))
            {
                grade = "5끗";
            }
            else if ((p1 == 0 && p2 == 5) || (p1 == 1 && p2 == 4) || (p1 == 1 && p2 == 5) || (p1 == 5 && p2 == 0) || (p1 == 4 && p2 == 1) || (p1 == 5 && p2 == 1) || (p1 == 8 && p2 == 16) || (p1 == 8 && p2 == 17) || (p1 == 9 && p2 == 16) || (p1 == 9 && p2 == 17) || (p1 == 16 && p2 == 8) || (p1 == 16 && p2 == 9) || (p1 == 17 && p2 == 8) || (p1 == 17 && p2 == 9) || (p1 == 10 && p2 == 14) || (p1 == 10 && p2 == 15) || (p1 == 11 && p2 == 14) || (p1 == 11 && p2 == 15) || (p1 == 14 && p2 == 10) || (p1 == 14 && p2 == 11) || (p1 == 15 && p2 == 10) || (p1 == 15 && p2 == 11))
            {
                grade = "4끗";
            }
            else if ((p1 == 4 && p2 == 18) || (p1 == 4 && p2 == 19) || (p1 == 5 && p2 == 18) || (p1 == 5 && p2 == 19) || (p1 == 18 && p2 == 4) || (p1 == 18 && p2 == 5) || (p1 == 19 && p2 == 4) || (p1 == 19 && p2 == 5) || (p1 == 8 && p2 == 14) || (p1 == 8 && p2 == 15) || (p1 == 9 && p2 == 14) || (p1 == 9 && p2 == 15) || (p1 == 14 && p2 == 8) || (p1 == 14 && p2 == 9) || (p1 == 15 && p2 == 8) || (p1 == 15 && p2 == 9) || (p1 == 10 && p2 == 12) || (p1 == 10 && p2 == 13) || (p1 == 11 && p2 == 12) || (p1 == 11 && p2 == 13) || (p1 == 12 && p2 == 10) || (p1 == 12 && p2 == 11) || (p1 == 13 && p2 == 10) || (p1 == 13 && p2 == 11))
            {
                grade = "3끗";
            }
            else if ((p1 == 2 && p2 == 18) || (p1 == 2 && p2 == 19) || (p1 == 3 && p2 == 18) || (p1 == 3 && p2 == 19) || (p1 == 18 && p2 == 2) || (p1 == 18 && p2 == 3) || (p1 == 19 && p2 == 2) || (p1 == 19 && p2 == 3) || (p1 == 4 && p2 == 16) || (p1 == 4 && p2 == 17) || (p1 == 5 && p2 == 16) || (p1 == 5 && p2 == 17) || (p1 == 16 && p2 == 4) || (p1 == 16 && p2 == 5) || (p1 == 17 && p2 == 4) || (p1 == 17 && p2 == 5) || (p1 == 6 && p2 == 14) || (p1 == 6 && p2 == 15) || (p1 == 7 && p2 == 14) || (p1 == 7 && p2 == 15) || (p1 == 14 && p2 == 6) || (p1 == 14 && p2 == 7) || (p1 == 15 && p2 == 6) || (p1 == 15 && p2 == 7) || (p1 == 8 && p2 == 12) || (p1 == 8 && p2 == 13) || (p1 == 9 && p2 == 12) || (p1 == 9 && p2 == 13) || (p1 == 12 && p2 == 8) || (p1 == 12 && p2 == 9) || (p1 == 13 && p2 == 8) || (p1 == 13 && p2 == 9))
            {
                grade = "2끗";
            }
            else if ((p1 == 2 && p2 == 16) || (p1 == 2 && p2 == 17) || (p1 == 3 && p2 == 16) || (p1 == 3 && p2 == 17) || (p1 == 16 && p2 == 2) || (p1 == 16 && p2 == 3) || (p1 == 17 && p2 == 2) || (p1 == 17 && p2 == 3) || (p1 == 4 && p2 == 14) || (p1 == 4 && p2 == 15) || (p1 == 5 && p2 == 14) || (p1 == 5 && p2 == 15) || (p1 == 14 && p2 == 4) || (p1 == 14 && p2 == 5) || (p1 == 15 && p2 == 4) || (p1 == 15 && p2 == 5) || (p1 == 6 && p2 == 12) || (p1 == 6 && p2 == 13) || (p1 == 7 && p2 == 12) || (p1 == 7 && p2 == 13) || (p1 == 12 && p2 == 6) || (p1 == 12 && p2 == 7) || (p1 == 13 && p2 == 6) || (p1 == 13 && p2 == 7) || (p1 == 8 && p2 == 10) || (p1 == 8 && p2 == 11) || (p1 == 9 && p2 == 10) || (p1 == 9 && p2 == 11) || (p1 == 10 && p2 == 8) || (p1 == 10 && p2 == 9) || (p1 == 11 && p2 == 8) || (p1 == 11 && p2 == 9))
            {
                grade = "1끗";
            }
            else if ((p1 == 6 && p2 == 16) || (p1 == 6 && p2 == 17) || (p1 == 7 && p2 == 16) || (p1 == 7 && p2 == 17) || (p1 == 16 && p2 == 6) || (p1 == 16 && p2 == 7) || (p1 == 17 && p2 == 6) || (p1 == 17 && p2 == 7))
            {
                grade = "49 멍텅구리!";
            }
            else
            {
                grade = "망통";
            }
            return grade;
        }
        public int checkRank(int p1, int p2)//플레이어 족보 체크
        {
            int rank;
            if ((p1 == 4 && p2 == 14) || (p1 == 14 && p2 == 4))
                rank = 0;
            else if ((p1 == 0 && p2 == 14) || (p1 == 14 && p2 == 0))
                rank = 1;
            else if ((p1 == 0 && p2 == 4) || (p1 == 4 && p2 == 0))
                rank = 2;
            else if ((p1 == 0 && p2 == 1) || (p1 == 1 && p2 == 0))
                rank = 12;
            else if ((p1 == 2 && p2 == 3) || (p1 == 3 && p2 == 2))
                rank = 11;
            else if ((p1 == 4 && p2 == 5) || (p1 == 5 && p2 == 4))
                rank = 10;
            else if ((p1 == 6 && p2 == 7) || (p1 == 7 && p2 == 6))
                rank = 9;
            else if ((p1 == 8 && p2 == 9) || (p1 == 9 && p2 == 8))
                rank = 8;
            else if ((p1 == 10 && p2 == 11) || (p1 == 11 && p2 == 10))
                rank = 7;
            else if ((p1 == 12 && p2 == 13) || (p1 == 13 && p2 == 12))
                rank = 6;
            else if ((p1 == 14 && p2 == 15) || (p1 == 15 && p2 == 14))
                rank = 5;
            else if ((p1 == 16 && p2 == 17) || (p1 == 17 && p2 == 16))
                rank = 4;
            else if ((p1 == 18 && p2 == 19) || (p1 == 19 && p2 == 18))
                rank = 3;
            else if ((p1 == 0 && p2 == 2) || (p1 == 0 && p2 == 3) || (p1 == 1 && p2 == 2) || (p1 == 1 && p2 == 3) || (p1 == 2 && p2 == 0) || (p1 == 3 && p2 == 0) || (p1 == 2 && p2 == 1) || (p1 == 3 && p2 == 1))
                rank = 13;
            else if ((p1 == 0 && p2 == 6) || (p1 == 0 && p2 == 7) || (p1 == 1 && p2 == 6) || (p1 == 1 && p2 == 7) || (p1 == 6 && p2 == 0) || (p1 == 7 && p2 == 0) || (p1 == 6 && p2 == 1) || (p1 == 7 && p2 == 1))
                rank = 14;
            else if ((p1 == 0 && p2 == 16) || (p1 == 0 && p2 == 17) || (p1 == 1 && p2 == 16) || (p1 == 1 && p2 == 17) || (p1 == 16 && p2 == 0) || (p1 == 17 && p2 == 0) || (p1 == 16 && p2 == 1) || (p1 == 17 && p2 == 1))
                rank = 15;
            else if ((p1 == 0 && p2 == 18) || (p1 == 0 && p2 == 19) || (p1 == 1 && p2 == 18) || (p1 == 1 && p2 == 19) || (p1 == 18 && p2 == 0) || (p1 == 19 && p2 == 0) || (p1 == 2 && p2 == 1) || (p1 == 18 && p2 == 19))
                rank = 16;
            else if ((p1 == 6 && p2 == 18) || (p1 == 6 && p2 == 19) || (p1 == 7 && p2 == 18) || (p1 == 7 && p2 == 19) || (p1 == 18 && p2 == 6) || (p1 == 18 && p2 == 7) || (p1 == 19 && p2 == 6) || (p1 == 19 && p2 == 7))
                rank = 17;
            else if ((p1 == 6 && p2 == 10) || (p1 == 7 && p2 == 10) || (p1 == 6 && p2 == 11) || (p1 == 7 && p2 == 11) || (p1 == 10 && p2 == 6) || (p1 == 11 && p2 == 6) || (p1 == 10 && p2 == 7) || (p1 == 11 && p2 == 7))
                rank = 18;
            else if ((p1 == 0 && p2 == 15) || (p1 == 1 && p2 == 15) || (p1 == 1 && p2 == 14) || (p1 == 15 && p2 == 0) || (p1 == 14 && p2 == 1) || (p1 == 15 && p2 == 1) || (p1 == 2 && p2 == 12) || (p1 == 2 && p2 == 13) || (p1 == 3 && p2 == 12) || (p1 == 3 && p2 == 13) || (p1 == 12 && p2 == 2) || (p1 == 13 && p2 == 2) || (p1 == 12 && p2 == 3) || (p1 == 13 && p2 == 3) || (p1 == 4 && p2 == 10) || (p1 == 4 && p2 == 11) || (p1 == 5 && p2 == 10) || (p1 == 5 && p2 == 11) || (p1 == 10 && p2 == 4) || (p1 == 11 && p2 == 4) || (p1 == 10 && p2 == 5) || (p1 == 11 && p2 == 5) || (p1 == 6 && p2 == 8) || (p1 == 6 && p2 == 9) || (p1 == 7 && p2 == 8) || (p1 == 7 && p2 == 9) || (p1 == 8 && p2 == 6) || (p1 == 8 && p2 == 7) || (p1 == 9 && p2 == 6) || (p1 == 9 && p2 == 7) || (p1 == 16 && p2 == 18) || (p1 == 16 && p2 == 19) || (p1 == 17 && p2 == 18) || (p1 == 17 && p2 == 19) || (p1 == 18 && p2 == 16) || (p1 == 19 && p2 == 16) || (p1 == 18 && p2 == 14) || (p1 == 19 && p2 == 17))
                rank = 19;
            else if ((p1 == 0 && p2 == 12) || (p1 == 1 && p2 == 12) || (p1 == 1 && p2 == 13) || (p1 == 0 && p2 == 13) || (p1 == 12 && p2 == 0) || (p1 == 12 && p2 == 1) || (p1 == 13 && p2 == 1) || (p1 == 13 && p2 == 0) || (p1 == 2 && p2 == 10) || (p1 == 2 && p2 == 11) || (p1 == 3 && p2 == 10) || (p1 == 3 && p2 == 11) || (p1 == 10 && p2 == 2) || (p1 == 11 && p2 == 2) || (p1 == 10 && p2 == 3) || (p1 == 11 && p2 == 3) || (p1 == 4 && p2 == 8) || (p1 == 4 && p2 == 9) || (p1 == 5 && p2 == 8) || (p1 == 5 && p2 == 9) || (p1 == 8 && p2 == 4) || (p1 == 8 && p2 == 5) || (p1 == 9 && p2 == 4) || (p1 == 9 && p2 == 5) || (p1 == 14 && p2 == 18) || (p1 == 14 && p2 == 19) || (p1 == 15 && p2 == 18) || (p1 == 15 && p2 == 19) || (p1 == 18 && p2 == 14) || (p1 == 19 && p2 == 14) || (p1 == 18 && p2 == 15) || (p1 == 19 && p2 == 15))
                rank = 20;
            else if ((p1 == 0 && p2 == 10) || (p1 == 0 && p2 == 11) || (p1 == 1 && p2 == 10) || (p1 == 1 && p2 == 11) || (p1 == 10 && p2 == 0) || (p1 == 10 && p2 == 1) || (p1 == 11 && p2 == 1) || (p1 == 11 && p2 == 0) || (p1 == 2 && p2 == 8) || (p1 == 2 && p2 == 9) || (p1 == 3 && p2 == 8) || (p1 == 3 && p2 == 9) || (p1 == 8 && p2 == 2) || (p1 == 9 && p2 == 2) || (p1 == 8 && p2 == 3) || (p1 == 9 && p2 == 3) || (p1 == 4 && p2 == 6) || (p1 == 4 && p2 == 7) || (p1 == 5 && p2 == 6) || (p1 == 5 && p2 == 7) || (p1 == 6 && p2 == 4) || (p1 == 6 && p2 == 5) || (p1 == 7 && p2 == 4) || (p1 == 7 && p2 == 5) || (p1 == 12 && p2 == 18) || (p1 == 12 && p2 == 19) || (p1 == 13 && p2 == 18) || (p1 == 13 && p2 == 19) || (p1 == 18 && p2 == 12) || (p1 == 19 && p2 == 12) || (p1 == 18 && p2 == 13) || (p1 == 19 && p2 == 13) || (p1 == 14 && p2 == 16) || (p1 == 14 && p2 == 17) || (p1 == 15 && p2 == 16) || (p1 == 15 && p2 == 17) || (p1 == 16 && p2 == 14) || (p1 == 16 && p2 == 15) || (p1 == 17 && p2 == 14) || (p1 == 17 && p2 == 15))
                rank = 21;
            else if ((p1 == 0 && p2 == 8) || (p1 == 1 && p2 == 8) || (p1 == 1 && p2 == 9) || (p1 == 0 && p2 == 9) || (p1 == 8 && p2 == 0) || (p1 == 8 && p2 == 1) || (p1 == 9 && p2 == 1) || (p1 == 9 && p2 == 0) || (p1 == 2 && p2 == 6) || (p1 == 2 && p2 == 7) || (p1 == 3 && p2 == 6) || (p1 == 3 && p2 == 7) || (p1 == 6 && p2 == 2) || (p1 == 7 && p2 == 2) || (p1 == 6 && p2 == 3) || (p1 == 7 && p2 == 3) || (p1 == 10 && p2 == 18) || (p1 == 10 && p2 == 19) || (p1 == 11 && p2 == 18) || (p1 == 11 && p2 == 19) || (p1 == 18 && p2 == 10) || (p1 == 18 && p2 == 11) || (p1 == 19 && p2 == 10) || (p1 == 19 && p2 == 11) || (p1 == 12 && p2 == 16) || (p1 == 12 && p2 == 17) || (p1 == 13 && p2 == 16) || (p1 == 13 && p2 == 17) || (p1 == 16 && p2 == 12) || (p1 == 16 && p2 == 13) || (p1 == 17 && p2 == 12) || (p1 == 17 && p2 == 13))
                rank = 22;
            else if ((p1 == 2 && p2 == 4) || (p1 == 2 && p2 == 5) || (p1 == 3 && p2 == 4) || (p1 == 3 && p2 == 5) || (p1 == 4 && p2 == 2) || (p1 == 4 && p2 == 3) || (p1 == 5 && p2 == 2) || (p1 == 5 && p2 == 3) || (p1 == 8 && p2 == 18) || (p1 == 8 && p2 == 19) || (p1 == 9 && p2 == 18) || (p1 == 9 && p2 == 19) || (p1 == 18 && p2 == 8) || (p1 == 18 && p2 == 9) || (p1 == 19 && p2 == 8) || (p1 == 19 && p2 == 9) || (p1 == 10 && p2 == 16) || (p1 == 10 && p2 == 17) || (p1 == 11 && p2 == 16) || (p1 == 11 && p2 == 17) || (p1 == 16 && p2 == 10) || (p1 == 16 && p2 == 11) || (p1 == 17 && p2 == 10) || (p1 == 17 && p2 == 11) || (p1 == 12 && p2 == 14) || (p1 == 12 && p2 == 15) || (p1 == 13 && p2 == 14) || (p1 == 13 && p2 == 15) || (p1 == 14 && p2 == 12) || (p1 == 14 && p2 == 13) || (p1 == 15 && p2 == 12) || (p1 == 15 && p2 == 13))
                rank = 23;
            else if ((p1 == 0 && p2 == 5) || (p1 == 1 && p2 == 4) || (p1 == 1 && p2 == 5) || (p1 == 5 && p2 == 0) || (p1 == 4 && p2 == 1) || (p1 == 5 && p2 == 1) || (p1 == 8 && p2 == 16) || (p1 == 8 && p2 == 17) || (p1 == 9 && p2 == 16) || (p1 == 9 && p2 == 17) || (p1 == 16 && p2 == 8) || (p1 == 16 && p2 == 9) || (p1 == 17 && p2 == 8) || (p1 == 17 && p2 == 9) || (p1 == 10 && p2 == 14) || (p1 == 10 && p2 == 15) || (p1 == 11 && p2 == 14) || (p1 == 11 && p2 == 15) || (p1 == 14 && p2 == 10) || (p1 == 14 && p2 == 11) || (p1 == 15 && p2 == 10) || (p1 == 15 && p2 == 11))
                rank = 24;
            else if ((p1 == 4 && p2 == 18) || (p1 == 4 && p2 == 19) || (p1 == 5 && p2 == 18) || (p1 == 5 && p2 == 19) || (p1 == 18 && p2 == 4) || (p1 == 18 && p2 == 5) || (p1 == 19 && p2 == 4) || (p1 == 19 && p2 == 5) || (p1 == 8 && p2 == 14) || (p1 == 8 && p2 == 15) || (p1 == 9 && p2 == 14) || (p1 == 9 && p2 == 15) || (p1 == 14 && p2 == 8) || (p1 == 14 && p2 == 9) || (p1 == 15 && p2 == 8) || (p1 == 15 && p2 == 9) || (p1 == 10 && p2 == 12) || (p1 == 10 && p2 == 13) || (p1 == 11 && p2 == 12) || (p1 == 11 && p2 == 13) || (p1 == 12 && p2 == 10) || (p1 == 12 && p2 == 11) || (p1 == 13 && p2 == 10) || (p1 == 13 && p2 == 11))
                rank = 25;
            else if ((p1 == 2 && p2 == 18) || (p1 == 2 && p2 == 19) || (p1 == 3 && p2 == 18) || (p1 == 3 && p2 == 19) || (p1 == 18 && p2 == 2) || (p1 == 18 && p2 == 3) || (p1 == 19 && p2 == 2) || (p1 == 19 && p2 == 3) || (p1 == 4 && p2 == 16) || (p1 == 4 && p2 == 17) || (p1 == 5 && p2 == 16) || (p1 == 5 && p2 == 17) || (p1 == 16 && p2 == 4) || (p1 == 16 && p2 == 5) || (p1 == 17 && p2 == 4) || (p1 == 17 && p2 == 5) || (p1 == 6 && p2 == 14) || (p1 == 6 && p2 == 15) || (p1 == 7 && p2 == 14) || (p1 == 7 && p2 == 15) || (p1 == 14 && p2 == 6) || (p1 == 14 && p2 == 7) || (p1 == 15 && p2 == 6) || (p1 == 15 && p2 == 7) || (p1 == 8 && p2 == 12) || (p1 == 8 && p2 == 13) || (p1 == 9 && p2 == 12) || (p1 == 9 && p2 == 13) || (p1 == 12 && p2 == 8) || (p1 == 12 && p2 == 9) || (p1 == 13 && p2 == 8) || (p1 == 13 && p2 == 9))
                rank = 26;
            else if ((p1 == 2 && p2 == 16) || (p1 == 2 && p2 == 17) || (p1 == 3 && p2 == 16) || (p1 == 3 && p2 == 17) || (p1 == 16 && p2 == 2) || (p1 == 16 && p2 == 3) || (p1 == 17 && p2 == 2) || (p1 == 17 && p2 == 3) || (p1 == 4 && p2 == 14) || (p1 == 4 && p2 == 15) || (p1 == 5 && p2 == 14) || (p1 == 5 && p2 == 15) || (p1 == 14 && p2 == 4) || (p1 == 14 && p2 == 5) || (p1 == 15 && p2 == 4) || (p1 == 15 && p2 == 5) || (p1 == 6 && p2 == 12) || (p1 == 6 && p2 == 13) || (p1 == 7 && p2 == 12) || (p1 == 7 && p2 == 13) || (p1 == 12 && p2 == 6) || (p1 == 12 && p2 == 7) || (p1 == 13 && p2 == 6) || (p1 == 13 && p2 == 7) || (p1 == 8 && p2 == 10) || (p1 == 8 && p2 == 11) || (p1 == 9 && p2 == 10) || (p1 == 9 && p2 == 11) || (p1 == 10 && p2 == 8) || (p1 == 10 && p2 == 9) || (p1 == 11 && p2 == 8) || (p1 == 11 && p2 == 9))
                rank = 27;
            else if ((p1 == 6 && p2 == 16) || (p1 == 6 && p2 == 17) || (p1 == 7 && p2 == 16) || (p1 == 7 && p2 == 17) || (p1 == 16 && p2 == 6) || (p1 == 16 && p2 == 7) || (p1 == 17 && p2 == 6) || (p1 == 17 && p2 == 7))
                rank = 100;
            else
                rank = 28;

            return rank;
        }
        //버튼작업
        private void sutda_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(infocount==1)
            info.Close();
            main.Visible = true;
        }

        private void start_Click(object sender, EventArgs e)
        {
            start.Enabled = false;
            cardSetting();
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (infocount == 0)
            {
                info = new sutdainfo(this);
                info.Show();
                infocount = 1;
            }
        }

        private void btnCall_Click(object sender, EventArgs e)
        {
            if (playercoin == 0)
            {
                tbPlayer.Clear();
                if (btnCall.Enabled == true)
                    btnCall.Enabled = false;
                tbPlayer.Text = "더이상 걸 코인이 없습니다";
            }
            else if (playercoin == 1)
            {
                if (dealerCallState == false)//선콜시
                {
                    if (btnCall.Enabled == true)
                        btnCall.Enabled = false;
                    playercoin--;
                    money++;
                    printcoin();
                    Thread th = new Thread(() => dealerBatting(dRank));
                    th.Start();
                }
                else
                {
                    if (btnCall.Enabled == true)
                        btnCall.Enabled = false;
                    tbPlayer.Text = "콜할 코인이 부족합니다";
                }
            }
            else
            {
                tbPlayer.Clear();
                if (btnCall.Enabled == true)
                    btnCall.Enabled = false;
                if (btnCheck.Enabled == true)
                    btnCheck.Enabled = false;
                if (btnDie.Enabled == true)
                    btnDie.Enabled = false;
                tbPlayer.Text = "콜";
                if (dealerCallState == true)
                {
                    playercoin = playercoin - 2;
                    money += 2;
                }
                else
                {
                    playercoin--;
                    money++;
                }
                printcoin();
                Thread th = new Thread(() => dealerBatting(dRank));
                th.Start();
            }
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {

            tbPlayer.Clear();
            tbDealer.Clear();


            tbPlayer.Text = "체크";
            if (dealerCallState == true)//딜러가 콜한경우
            {
                playercoin--;
                money++;
                printcoin();
            }
            btnCall.Enabled = false;
            btnCheck.Enabled = false;
            btnDie.Enabled = false;

            result();   
        }

        private void btnDie_Click(object sender, EventArgs e)
        {
            tbWinner.Text = "Dealer";
            dealercoin = dealercoin + money;
            money = 0;
            die();
        }

        private void settingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sutdaset se = new sutdaset(this);
            se.Show();
        }
    }
}
