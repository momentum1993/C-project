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
            //난이도 조정
            if (Properties.Settings.Default.difficulty == 0.2) {
                adjustDifficulty(mnuDifficulty20);
            } else if (Properties.Settings.Default.difficulty == 0.5) {
                adjustDifficulty(mnuDifficulty50);
            } else if (Properties.Settings.Default.difficulty == 1) {
                adjustDifficulty(mnuDifficulty100);
            } else if (Properties.Settings.Default.difficulty == 1.5) {
                adjustDifficulty(mnuDifficulty150);
            }
            //Grid 사이즈 조정
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
            //최고점수 파일 있으면 열고 ~
            if (File.Exists(Application.StartupPath + "\\HighScores.xml")) {
                dsScores.ReadXml(Application.StartupPath + "\\HighScores.xml");
            }
            tssTime.Text = "시간: 0";
            board.init(num_tiles_v, num_tiles_h);

            board.tmrGame.Tick += new EventHandler(tmrGame_Tick);
        }
        //시간재기
        void tmrGame_Tick(object sender, EventArgs e) {
            stMain.Items[0].Text = "시간: " + board.incGameTime();
        }
        //힌트버튼 클릭시
        private void btnHint_Click(object sender, EventArgs e) {
            if (!board.drawHint())
                MessageBox.Show("더 이상 가능한 경우가 없습니다.", "게임 종료", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        //사이즈 변경시 위치 재조정
        private void FrmMain_Resize(object sender, EventArgs e) {
            btnExit.Left = this.Width - 116;
        }
        //새 게임 시작 버튼 누를 시
        private void btnNewGame_Click(object sender, EventArgs e) {
            if (confirmNewGame())
                newGame();
        }
        //새 게임 시작 버튼 확인
        private bool confirmNewGame() {
            return !board.isGameStarted() ||
                    MessageBox.Show("현재 게임을 종료하고 새 게임이 시작됩니다. 새 게임을 시작하시겠습니까?", "새 게임 시작 확인",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }
        //새 게임 구현
        private void newGame() {
            stMain.Items[0].Text = "시간: 0";
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
            tssDiff.Text = "난이도: " + mnu.Text;
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
            MessageBox.Show("더 이상 가능한 조합이 없습니다.", "게임 종료", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            tssGridSize.Text = "크기: " + mnu.Text;
        }

        private void board_Resize(object sender, EventArgs e) {
            this.Width = board.Right + 20;
            this.Height = board.Bottom + 40 + pnButtons.Height + stMain.Height;
        }

        private void mnuAbout_Click(object sender, EventArgs e) {
            MessageBox.Show("1. 동일한 모양의 패 2개가 인접해 있을 때\n2.동일한 모양의 패 2개 사이에 다른 패가 있을 경우, 수평 또는 수직의 직선으로 연결하여 그 선이 구부러지는 횟수가 2회 이내 일 때(즉, 3개 이내의 수직선 또는 수평선의 조합으로 2개의 패를 이을 때\n패 제거가 가능합니다.", "사천성 하는 방법", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void shisenBoard_OnCheatedChanged(object sender, EventArgs e) {
            tssCheated.Text = "힌트사용: " + (board.isCheated() ? "O" : "X");
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