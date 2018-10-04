using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Shisen_Sho {
    public partial class FrmHighScores : Form {
        public FrmHighScores(DataSet dsSource) {
            InitializeComponent();

            dgScores.DataMember = "Scores";
            dgScores.DataSource = dsSource;

            dgScores.Columns["시간"].Width -= 40;
            dgScores.Columns["플레이어"].Width += 100;
            dgScores.Columns["크기"].Width -= 20;
            dgScores.Sort(dgScores.Columns["시간"], ListSortDirection.Ascending);
        }

        private void btnClose_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void FrmHighScores_Resize(object sender, EventArgs e) {
            dgScores.Height = this.Height - 87;
            btnClose.Left = this.Width - 100;
            btnClose.Top = this.Height - 71;
        }

        private void dgScores_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FrmHighScores_Load(object sender, EventArgs e)
        {

        }
    }
}