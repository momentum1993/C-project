using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Shisen_Sho {
    public partial class FrmMain : Form {
        public const int NUM_HIGH_SCORES = 10;
        private int num_tiles_h = 24;
        private int num_tiles_v = 12;
        public FrmMain() {
            InitializeComponent();
            //���̵� ����
            if (Properties.Settings.Default.difficulty == 0.2) {
                adjustDifficulty(mnuDifficulty20);
            } else if (Properties.Settings.Default.difficulty == 0.5) {
                adjustDifficulty(mnuDifficulty50);
            } else if (Properties.Settings.Default.difficulty == 1) {
                adjustDifficulty(mnuDifficulty100);
            } else if (Properties.Settings.Default.difficulty == 1.5) {
                adjustDifficulty(mnuDifficulty150);
            }
            //Grid ������ ����
            if (Properties.Settings.Default.dim == "14x6") {
                adjustDimensions(mnuDim14x6);
            } else if (Properties.Settings.Default.dim == "18x8") {
                adjustDimensions(mnuDim18x8);
            } else if (Properties.Settings.Default.dim == "24x12") {
                adjustDimensions(mnuDim24x12);
            } else if (Properties.Settings.Default.dim == "26x14") {
                adjustDimensions(mnuDim26x14);
            } else if (Properties.Settings.Default.dim == "30x16") {
                adjustDimensions(mnuDim30x16);
            }
            //�ְ����� ���� ������ ���� ~
            if (File.Exists(Application.StartupPath + "\\HighScores.xml")) {
                dsScores.ReadXml(Application.StartupPath + "\\HighScores.xml");
            }
            tssTime.Text = "�ð�: 0";
            board.init(num_tiles_v, num_tiles_h);

            board.tmrGame.Tick += new EventHandler(tmrGame_Tick);
        }
        //�ð����
        void tmrGame_Tick(object sender, EventArgs e) {
            stMain.Items[0].Text = "�ð�: " + board.incGameTime();
        }
        //��Ʈ��ư Ŭ����
        private void btnHint_Click(object sender, EventArgs e) {
            if (!board.drawHint())
                MessageBox.Show("�� �̻� ������ ��찡 �����ϴ�.", "���� ����", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        //������ ����� ��ġ ������
        private void FrmMain_Resize(object sender, EventArgs e) {
            btnExit.Left = this.Width - 116;
        }
        //�� ���� ���� ��ư ���� ��
        private void btnNewGame_Click(object sender, EventArgs e) {
            if (confirmNewGame())
                newGame();
        }
        //�� ���� ���� ��ư Ȯ��
        private bool confirmNewGame() {
            return !board.isGameStarted() ||
                    MessageBox.Show("���� ������ �����ϰ� �� ������ ���۵˴ϴ�. �� ������ �����Ͻðڽ��ϱ�?", "�� ���� ���� Ȯ��",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }
        //�� ���� ����
        private void newGame() {
            stMain.Items[0].Text = "�ð�: 0";
            this.Cursor = Cursors.WaitCursor;
            board.setDimensions(num_tiles_v, num_tiles_h);
            board.reset();
            this.Cursor = Cursors.Default;
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e) {
            Properties.Settings.Default.Save();
            dsScores.WriteXml(Application.StartupPath + "\\HighScores.xml");
        }

        private void adjustDifficulty(ToolStripMenuItem mnu) {
            Properties.Settings.Default.difficulty = double.Parse(mnu.Tag.ToString());
            foreach (ToolStripMenuItem m in mnuDifficulty.DropDownItems) {
                m.Checked = m == mnu;
            }
            tssDiff.Text = "���̵�: " + mnu.Text;
        }

        private void mnuBestTimes_Click(object sender, EventArgs e) {
            new FrmHighScores(dsScores).ShowDialog();
        }

        private void board_OnGameWon(object sender, EventArgs e) {
            if (!board.isCheated()) {
                int time = board.getGameTime();
                /*DataRow[] betterScores = dsScores.Tables["Scores"].Select("GridSize = " +
                    Properties.Settings.Default.dim + " and Difficulty = " +
                    Properties.Settings.Default.difficulty + " and Time <= " + time, "Time ASC");

                if (betterScores.Length < NUM_HIGH_SCORES) {
                    string player = FrmNamePrompt.showNamePrompt(this, dsScores, time);
                    if (player != null)
                        new FrmHighScores(dsScores).ShowDialog();
                }*/
            }
        }

        private void btnExit_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void mnuNewGame_Click(object sender, EventArgs e) {
            btnNewGame_Click(btnNewGame, EventArgs.Empty);
        }

        private void mnuGetHint_Click(object sender, EventArgs e) {
            btnHint_Click(btnHint, EventArgs.Empty);
        }

        private void mnuExit_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void board_OnGameLost(object sender, EventArgs e) {
            MessageBox.Show("�� �̻� ������ ������ �����ϴ�.", "���� ����", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void adjustDimensions(ToolStripMenuItem mnu) {
            string gridSize = mnu.Tag.ToString();
            int xLoc = gridSize.IndexOf('x');
            num_tiles_h = int.Parse(gridSize.Substring(0, xLoc));
            num_tiles_v = int.Parse(gridSize.Substring(xLoc + 1));
            Properties.Settings.Default.dim = gridSize;
            foreach (ToolStripMenuItem m in mnuGridSize.DropDownItems) {
                m.Checked = m == mnu;                    
            }
            tssGridSize.Text = "ũ��: " + mnu.Text;
        }

        private void board_Resize(object sender, EventArgs e) {
            this.Width = board.Right + 20;
            this.Height = board.Bottom + 40 + pnButtons.Height + stMain.Height;
        }

        private void mnuAbout_Click(object sender, EventArgs e) {
            MessageBox.Show("1. ������ ����� �� 2���� ������ ���� ��\n2.������ ����� �� 2�� ���̿� �ٸ� �а� ���� ���, ���� �Ǵ� ������ �������� �����Ͽ� �� ���� ���η����� Ƚ���� 2ȸ �̳� �� ��(��, 3�� �̳��� ������ �Ǵ� ������ �������� 2���� �и� ���� ��\n�� ���Ű� �����մϴ�.", "��õ�� �ϴ� ���", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void shisenBoard_OnCheatedChanged(object sender, EventArgs e) {
            tssCheated.Text = "��Ʈ���: " + (board.isCheated() ? "O" : "X");
        }

        private void mnuDiff_Click(object sender, EventArgs e) {
            if (confirmNewGame()) {
                adjustDifficulty((ToolStripMenuItem)sender);
                newGame();
            }
        }

        private void mnuGridSize_Click(object sender, EventArgs e) {
            if (confirmNewGame()) {
                adjustDimensions((ToolStripMenuItem)sender);
                newGame();
            }
        }

        private void board_Load(object sender, EventArgs e)
        {

        }

        private void FrmMain_Load(object sender, EventArgs e)
        {

        }

        private void mnuHelp_Click(object sender, EventArgs e)
        {

        }

        private void tssTime_Click(object sender, EventArgs e)
        {

        }

        private void mnuDifficulty_Click(object sender, EventArgs e)
        {

        }
    }
}