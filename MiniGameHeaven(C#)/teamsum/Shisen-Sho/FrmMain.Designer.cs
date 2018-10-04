namespace Shisen_Sho {
    partial class FrmMain {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private Board board;
        private System.Windows.Forms.Panel pnButtons;
        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem mnuGame;
        private System.Windows.Forms.ToolStripMenuItem mnuNewGame;
        private System.Windows.Forms.ToolStripMenuItem mnuGetHint;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuExit;
        private System.Windows.Forms.ToolStripMenuItem mnuOptions;
        private System.Windows.Forms.ToolStripMenuItem mnuDifficulty;
        private System.Windows.Forms.ToolStripMenuItem mnuDifficulty20;
        private System.Windows.Forms.ToolStripMenuItem mnuDifficulty50;
        private System.Windows.Forms.ToolStripMenuItem mnuDifficulty100;
        private System.Windows.Forms.ToolStripMenuItem mnuDifficulty150;
        public System.Data.DataSet dsScores;
        private System.Data.DataTable dtScores;
        private System.Data.DataColumn colPlayer;
        private System.Data.DataColumn colDifficulty;
        //private System.Data.DataColumn colGravity;
        private System.Windows.Forms.ToolStripMenuItem mnuBestTimes;
        private System.Windows.Forms.ToolStripStatusLabel tssTime;
        private System.Windows.Forms.ToolStripStatusLabel tssDiff;
        private System.Windows.Forms.StatusStrip stMain;
        private System.Data.DataColumn dcTime;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            //Panel 부분
            this.pnButtons = new System.Windows.Forms.Panel();
            //Button 부분
            this.btnExit = new System.Windows.Forms.Button();
            this.btnHint = new System.Windows.Forms.Button();
            this.btnNewGame = new System.Windows.Forms.Button();
            //MenuStrip 게임
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.mnuGame = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNewGame = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGetHint = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator(); //나누는 선
            this.mnuBestTimes = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator(); //나누는 선
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            //MenuStrip 옵션
            this.mnuOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDifficulty = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDifficulty20 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDifficulty50 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDifficulty100 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDifficulty150 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGridSize = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDim14x6 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDim18x8 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDim24x12 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDim26x14 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDim30x16 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.dsScores = new System.Data.DataSet();
            this.dtScores = new System.Data.DataTable();
            this.dcTime = new System.Data.DataColumn();
            this.colPlayer = new System.Data.DataColumn();
            this.colDifficulty = new System.Data.DataColumn();
            this.dcGridSize = new System.Data.DataColumn();
            this.tssTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssDiff = new System.Windows.Forms.ToolStripStatusLabel();
            this.stMain = new System.Windows.Forms.StatusStrip();
            this.tssGridSize = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssCheated = new System.Windows.Forms.ToolStripStatusLabel();
            this.board = new Shisen_Sho.Board();
            this.pnButtons.SuspendLayout();
            this.mnuMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dsScores)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtScores)).BeginInit();
            this.stMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnButtons
            // 
            this.pnButtons.Controls.Add(this.btnExit);
            this.pnButtons.Controls.Add(this.btnHint);
            this.pnButtons.Controls.Add(this.btnNewGame);
            this.pnButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnButtons.Location = new System.Drawing.Point(0, 514);
            this.pnButtons.Name = "pnButtons";
            this.pnButtons.Padding = new System.Windows.Forms.Padding(12, 14, 12, 14);
            this.pnButtons.Size = new System.Drawing.Size(962, 36);
            this.pnButtons.TabIndex = 9;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(817, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(95, 28); //112, 28
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "게임 종료";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnHint
            // 
            this.btnHint.Location = new System.Drawing.Point(133, 0);
            this.btnHint.Name = "btnHint";
            this.btnHint.Size = new System.Drawing.Size(112, 28);
            this.btnHint.TabIndex = 4;
            this.btnHint.Text = "힌트";
            this.btnHint.UseVisualStyleBackColor = true;
            this.btnHint.Click += new System.EventHandler(this.btnHint_Click);
            // 
            // btnNewGame
            // 
            this.btnNewGame.Location = new System.Drawing.Point(14, 0);
            this.btnNewGame.Name = "btnNewGame";
            this.btnNewGame.Size = new System.Drawing.Size(112, 28);
            this.btnNewGame.TabIndex = 3;
            this.btnNewGame.Text = "새 게임 시작";
            this.btnNewGame.UseVisualStyleBackColor = true;
            this.btnNewGame.Click += new System.EventHandler(this.btnNewGame_Click);
            // 
            // mnuMain
            // 
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuGame,
            this.mnuOptions,
            this.mnuHelp});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.mnuMain.Size = new System.Drawing.Size(962, 24);
            this.mnuMain.TabIndex = 10;
            this.mnuMain.Text = "menuStrip1";
            // 
            // mnuGame
            // 
            this.mnuGame.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNewGame,
            this.mnuGetHint,
            this.toolStripSeparator1,
            this.mnuBestTimes,
            this.toolStripSeparator2,
            this.mnuExit});
            this.mnuGame.Name = "mnuGame";
            this.mnuGame.Size = new System.Drawing.Size(43, 20);
            this.mnuGame.Text = "게임";
            // 
            // mnuNewGame
            // 
            this.mnuNewGame.Name = "mnuNewGame";
            this.mnuNewGame.Size = new System.Drawing.Size(142, 22);
            this.mnuNewGame.Text = "새 게임 시작";
            this.mnuNewGame.Click += new System.EventHandler(this.mnuNewGame_Click);
            // 
            // mnuGetHint
            // 
            this.mnuGetHint.Name = "mnuGetHint";
            this.mnuGetHint.Size = new System.Drawing.Size(142, 22);
            this.mnuGetHint.Text = "힌트";
            this.mnuGetHint.Click += new System.EventHandler(this.mnuGetHint_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(139, 6);
            // 
            // mnuBestTimes
            // 
            this.mnuBestTimes.Name = "mnuBestTimes";
            this.mnuBestTimes.Size = new System.Drawing.Size(142, 22);
            this.mnuBestTimes.Text = "최고기록";
            this.mnuBestTimes.Click += new System.EventHandler(this.mnuBestTimes_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(139, 6);
            // 
            // mnuExit
            // 
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.Size = new System.Drawing.Size(142, 22);
            this.mnuExit.Text = "게임 종료";
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            // 
            // mnuOptions
            // 
            this.mnuOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDifficulty,
            this.mnuGridSize});
            this.mnuOptions.Name = "mnuOptions";
            this.mnuOptions.Size = new System.Drawing.Size(43, 20);
            this.mnuOptions.Text = "옵션";
            // 
            // mnuDifficulty
            // 
            this.mnuDifficulty.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDifficulty20,
            this.mnuDifficulty50,
            this.mnuDifficulty100,
            this.mnuDifficulty150});
            this.mnuDifficulty.Name = "mnuDifficulty";
            this.mnuDifficulty.Size = new System.Drawing.Size(110, 22);
            this.mnuDifficulty.Text = "난이도";
            this.mnuDifficulty.Click += new System.EventHandler(this.mnuDifficulty_Click);
            // 
            // mnuDifficulty20
            // 
            this.mnuDifficulty20.Name = "mnuDifficulty20";
            this.mnuDifficulty20.Size = new System.Drawing.Size(110, 22);
            this.mnuDifficulty20.Tag = ".2";
            this.mnuDifficulty20.Text = "입문";
            this.mnuDifficulty20.Click += new System.EventHandler(this.mnuDiff_Click);
            // 
            // mnuDifficulty50
            // 
            this.mnuDifficulty50.Name = "mnuDifficulty50";
            this.mnuDifficulty50.Size = new System.Drawing.Size(110, 22);
            this.mnuDifficulty50.Tag = ".5";
            this.mnuDifficulty50.Text = "쉬움";
            this.mnuDifficulty50.Click += new System.EventHandler(this.mnuDiff_Click);
            // 
            // mnuDifficulty100
            // 
            this.mnuDifficulty100.Name = "mnuDifficulty100";
            this.mnuDifficulty100.Size = new System.Drawing.Size(110, 22);
            this.mnuDifficulty100.Tag = "1";
            this.mnuDifficulty100.Text = "보통";
            this.mnuDifficulty100.Click += new System.EventHandler(this.mnuDiff_Click);
            // 
            // mnuDifficulty150
            // 
            this.mnuDifficulty150.Name = "mnuDifficulty150";
            this.mnuDifficulty150.Size = new System.Drawing.Size(110, 22);
            this.mnuDifficulty150.Tag = "1.5";
            this.mnuDifficulty150.Text = "어려움";
            this.mnuDifficulty150.Click += new System.EventHandler(this.mnuDiff_Click);
            // 
            // mnuGridSize
            // 
            this.mnuGridSize.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDim14x6,
            this.mnuDim18x8,
            this.mnuDim24x12,
            this.mnuDim26x14,
            this.mnuDim30x16});
            this.mnuGridSize.Name = "mnuGridSize";
            this.mnuGridSize.Size = new System.Drawing.Size(110, 22);
            this.mnuGridSize.Text = "크기";
            // 
            // mnuDim14x6
            // 
            this.mnuDim14x6.Name = "mnuDim14x6";
            this.mnuDim14x6.Size = new System.Drawing.Size(108, 22);
            this.mnuDim14x6.Tag = "14x6";
            this.mnuDim14x6.Text = "14x6";
            this.mnuDim14x6.Click += new System.EventHandler(this.mnuGridSize_Click);
            // 
            // mnuDim18x8
            // 
            this.mnuDim18x8.Name = "mnuDim18x8";
            this.mnuDim18x8.Size = new System.Drawing.Size(108, 22);
            this.mnuDim18x8.Tag = "18x8";
            this.mnuDim18x8.Text = "18x8";
            this.mnuDim18x8.Click += new System.EventHandler(this.mnuGridSize_Click);
            // 
            // mnuDim24x12
            // 
            this.mnuDim24x12.Name = "mnuDim24x12";
            this.mnuDim24x12.Size = new System.Drawing.Size(108, 22);
            this.mnuDim24x12.Tag = "24x12";
            this.mnuDim24x12.Text = "24x12";
            this.mnuDim24x12.Click += new System.EventHandler(this.mnuGridSize_Click);
            // 
            // mnuDim26x14
            // 
            this.mnuDim26x14.Name = "mnuDim26x14";
            this.mnuDim26x14.Size = new System.Drawing.Size(108, 22);
            this.mnuDim26x14.Tag = "26x14";
            this.mnuDim26x14.Text = "26x14";
            this.mnuDim26x14.Click += new System.EventHandler(this.mnuGridSize_Click);
            // 
            // mnuDim30x16
            // 
            this.mnuDim30x16.Name = "mnuDim30x16";
            this.mnuDim30x16.Size = new System.Drawing.Size(108, 22);
            this.mnuDim30x16.Tag = "30x16";
            this.mnuDim30x16.Text = "30x16";
            this.mnuDim30x16.Click += new System.EventHandler(this.mnuGridSize_Click);
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAbout});
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(55, 20);
            this.mnuHelp.Text = "도움말";
            this.mnuHelp.Click += new System.EventHandler(this.mnuHelp_Click);
            // 
            // mnuAbout
            // 
            this.mnuAbout.Name = "mnuAbout";
            this.mnuAbout.Size = new System.Drawing.Size(122, 22);
            this.mnuAbout.Text = "게임방법";
            this.mnuAbout.Click += new System.EventHandler(this.mnuAbout_Click);
            // 
            // dsScores
            // 
            this.dsScores.DataSetName = "HighScores";
            this.dsScores.Tables.AddRange(new System.Data.DataTable[] {
            this.dtScores});
            // 
            // dtScores
            // 
            this.dtScores.Columns.AddRange(new System.Data.DataColumn[] {
            this.dcTime,
            this.colPlayer,
            this.colDifficulty,
            this.dcGridSize});
            this.dtScores.TableName = "Scores";
            // 
            // dcTime
            // 
            this.dcTime.ColumnName = "시간";
            this.dcTime.DataType = typeof(int);
            // 
            // colPlayer
            // 
            this.colPlayer.ColumnName = "플레이어";
            // 
            // colDifficulty
            // 
            this.colDifficulty.ColumnName = "난이도";
            this.colDifficulty.DataType = typeof(double);
            // 
            // dcGridSize
            // 
            this.dcGridSize.Caption = "Grid Size";
            this.dcGridSize.ColumnName = "크기";
            // 
            // tssTime
            // 
            this.tssTime.Name = "tssTime";
            this.tssTime.Size = new System.Drawing.Size(788, 19);
            this.tssTime.Spring = true;
            this.tssTime.Text = "시간:";
            this.tssTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tssTime.Click += new System.EventHandler(this.tssTime_Click);
            // 
            // tssDiff
            // 
            this.tssDiff.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.tssDiff.Name = "tssDiff";
            this.tssDiff.Size = new System.Drawing.Size(50, 19);
            this.tssDiff.Text = "난이도:";
            // 
            // stMain
            // 
            this.stMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssTime,
            this.tssDiff,
            this.tssGridSize,
            this.tssCheated});
            this.stMain.Location = new System.Drawing.Point(0, 550);
            this.stMain.Name = "stMain";
            this.stMain.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.stMain.Size = new System.Drawing.Size(962, 24);
            this.stMain.SizingGrip = false;
            this.stMain.TabIndex = 8;
            this.stMain.Text = "statusStrip1";
            // 
            // tssGridSize
            // 
            this.tssGridSize.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.tssGridSize.Name = "tssGridSize";
            this.tssGridSize.Size = new System.Drawing.Size(38, 19);
            this.tssGridSize.Text = "크기:";
            // 
            // tssCheated
            // 
            this.tssCheated.Name = "tssCheated";
            this.tssCheated.Size = new System.Drawing.Size(69, 19);
            this.tssCheated.Text = "힌트사용: X";
            // 
            // board
            // 
            this.board.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.board.Location = new System.Drawing.Point(14, 37);
            this.board.Name = "board";
            this.board.Size = new System.Drawing.Size(931, 471);
            this.board.TabIndex = 7;
            this.board.OnGameWon += new System.EventHandler(this.board_OnGameWon);
            this.board.OnGameLost += new System.EventHandler(this.board_OnGameLost);
            this.board.OnCheatedChanged += new System.EventHandler(this.shisenBoard_OnCheatedChanged);
            this.board.Load += new System.EventHandler(this.board_Load);
            this.board.Resize += new System.EventHandler(this.board_Resize);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(962, 574);
            this.Controls.Add(this.pnButtons);
            this.Controls.Add(this.stMain);
            this.Controls.Add(this.mnuMain);
            this.Controls.Add(this.board);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            //this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mnuMain;
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "사천성";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMain_FormClosed);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.Resize += new System.EventHandler(this.FrmMain_Resize);
            this.pnButtons.ResumeLayout(false);
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dsScores)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtScores)).EndInit();
            this.stMain.ResumeLayout(false);
            this.stMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnHint;
        private System.Windows.Forms.Button btnNewGame;
        private System.Windows.Forms.ToolStripMenuItem mnuGridSize;
        private System.Windows.Forms.ToolStripMenuItem mnuDim14x6;
        private System.Windows.Forms.ToolStripMenuItem mnuDim18x8;
        private System.Windows.Forms.ToolStripMenuItem mnuDim24x12;
        private System.Windows.Forms.ToolStripMenuItem mnuDim26x14;
        private System.Windows.Forms.ToolStripMenuItem mnuDim30x16;
        private System.Data.DataColumn dcGridSize;
        private System.Windows.Forms.ToolStripStatusLabel tssGridSize;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuAbout;
        private System.Windows.Forms.ToolStripStatusLabel tssCheated;
    }
}

