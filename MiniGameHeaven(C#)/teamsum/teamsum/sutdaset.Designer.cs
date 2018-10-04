namespace teamsum
{
    partial class sutdaset
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.setCoin = new System.Windows.Forms.TextBox();
            this.setID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.setStart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // setCoin
            // 
            this.setCoin.Location = new System.Drawing.Point(96, 109);
            this.setCoin.Name = "setCoin";
            this.setCoin.Size = new System.Drawing.Size(149, 25);
            this.setCoin.TabIndex = 9;
            // 
            // setID
            // 
            this.setID.Location = new System.Drawing.Point(96, 61);
            this.setID.Name = "setID";
            this.setID.Size = new System.Drawing.Size(149, 25);
            this.setID.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "Coin : ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "ID : ";
            // 
            // setStart
            // 
            this.setStart.Location = new System.Drawing.Point(41, 155);
            this.setStart.Name = "setStart";
            this.setStart.Size = new System.Drawing.Size(204, 37);
            this.setStart.TabIndex = 5;
            this.setStart.Text = "START";
            this.setStart.UseVisualStyleBackColor = true;
            this.setStart.Click += new System.EventHandler(this.setStart_Click);
            // 
            // sutdaset
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 250);
            this.Controls.Add(this.setCoin);
            this.Controls.Add(this.setID);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.setStart);
            this.Name = "sutdaset";
            this.Text = "sutdaset";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox setCoin;
        private System.Windows.Forms.TextBox setID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button setStart;
    }
}