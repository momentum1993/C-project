using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Shisen_Sho {
    public partial class FrmNamePrompt : Form {
        private DataSet ds;
        private int time;
        private string player;

        private FrmNamePrompt(DataSet ds, int time) {
            InitializeComponent();
            this.ds = ds;
            this.time = time;
            player = null;
        }

        public string getPlayer() {
            return player;
        }

        private void btnOK_Click(object sender, EventArgs e) {
            player = txtName.Text.Trim();
            if (player != "") {
                DataRow[] allRows = ds.Tables["Scores"].Select("GridSize = '" +
                    Properties.Settings.Default.dim + "' and Difficulty = " +
                    Properties.Settings.Default.difficulty, "Time ASC");
                for (int i = FrmMain.NUM_HIGH_SCORES - 1; i < allRows.Length; i++) {
                    allRows[i].Delete();
                }

                DataRow newRow = ds.Tables["Scores"].NewRow();
                newRow["Player"] = player;
                newRow["Time"] = time;
                newRow["Difficulty"] = Properties.Settings.Default.difficulty;
                newRow["GridSize"] = Properties.Settings.Default.dim;
                ds.Tables["Scores"].Rows.Add(newRow);

                this.Close();
            } else {
                MessageBox.Show("이름을 입력하셔야 합니다.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            player = null;
            this.Close();
        }

        public static string showNamePrompt(Form owner, DataSet ds, int time) {
            FrmNamePrompt fnp = new FrmNamePrompt(ds, time);
            fnp.ShowDialog(owner);
            return fnp.getPlayer();
        }

        private void FrmNamePrompt_Load(object sender, EventArgs e)
        {

        }
    }
}