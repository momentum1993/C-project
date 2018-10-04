namespace teamsum
{
    partial class wall
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Label_Point = new System.Windows.Forms.Label();
            this.Timer_Brick_Move = new System.Windows.Forms.Timer(this.components);
            this.Timer_Ball_Move = new System.Windows.Forms.Timer(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("한컴 윤체 B", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(48, 121);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(688, 167);
            this.label1.TabIndex = 1;
            this.label1.Text = "벽돌깨기게임";
            this.label1.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("한컴 윤체 B", 60F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(52, 158);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(672, 139);
            this.label3.TabIndex = 4;
            this.label3.Text = "GAME OVER";
            this.label3.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("한컴 윤체 B", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(118, 503);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(536, 66);
            this.label2.TabIndex = 5;
            this.label2.Text = "Press Space to Start!";
            this.label2.Visible = false;
            // 
            // Label_Point
            // 
            this.Label_Point.AutoSize = true;
            this.Label_Point.BackColor = System.Drawing.Color.Transparent;
            this.Label_Point.Font = new System.Drawing.Font("한컴 윤체 B", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Label_Point.ForeColor = System.Drawing.Color.White;
            this.Label_Point.Location = new System.Drawing.Point(598, 612);
            this.Label_Point.Name = "Label_Point";
            this.Label_Point.Size = new System.Drawing.Size(138, 42);
            this.Label_Point.TabIndex = 6;
            this.Label_Point.Text = "Point : 0";
            this.Label_Point.Visible = false;
            // 
            // Timer_Brick_Move
            // 
            this.Timer_Brick_Move.Interval = 20000;
            this.Timer_Brick_Move.Tick += new System.EventHandler(this.Timer_Brick_Move_Tick);
            // 
            // Timer_Ball_Move
            // 
            this.Timer_Ball_Move.Interval = 20;
            this.Timer_Ball_Move.Tick += new System.EventHandler(this.Timer_Ball_Move_Tick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("한컴 윤체 B", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(79, 486);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(611, 83);
            this.label4.TabIndex = 7;
            this.label4.Text = "Press Space to Exit";
            this.label4.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("한컴 윤체 B", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(124, 363);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(473, 83);
            this.label5.TabIndex = 8;
            this.label5.Text = "Your POINT : 0";
            this.label5.Visible = false;
            // 
            // wall
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::teamsum.Properties.Resources.background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(782, 653);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Label_Point);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "wall";
            this.Text = "wall";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.wall_FormClosed);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.wall_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label Label_Point;
        private System.Windows.Forms.Timer Timer_Brick_Move;
        private System.Windows.Forms.Timer Timer_Ball_Move;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}