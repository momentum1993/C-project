namespace Shisen_Sho {
    partial class FrmHighScores {
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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmHighScores));
            this.dgScores = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgScores)).BeginInit();
            this.SuspendLayout();
            // 
            // dgScores
            // 
            this.dgScores.AllowUserToAddRows = false;
            this.dgScores.AllowUserToDeleteRows = false; //treu면 삭제 가능
            this.dgScores.AllowUserToResizeRows = false;
            this.dgScores.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgScores.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgScores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgScores.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgScores.Location = new System.Drawing.Point(12, 9);
            this.dgScores.Name = "dgScores";
            this.dgScores.ReadOnly = true;
            this.dgScores.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgScores.ShowCellErrors = false;
            this.dgScores.ShowCellToolTips = false;
            this.dgScores.ShowEditingIcon = false;
            this.dgScores.ShowRowErrors = false;
            this.dgScores.Size = new System.Drawing.Size(655, 249);
            this.dgScores.TabIndex = 0;
            this.dgScores.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgScores_CellContentClick);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(572, 264);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(96, 26);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // FrmHighScores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 302);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgScores);
            //this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(602, 332);
            this.Name = "FrmHighScores";
            this.Padding = new System.Windows.Forms.Padding(12, 9, 12, 9);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "최고기록";
            this.Load += new System.EventHandler(this.FrmHighScores_Load);
            this.Resize += new System.EventHandler(this.FrmHighScores_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dgScores)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgScores;
        private System.Windows.Forms.Button btnClose;

    }
}