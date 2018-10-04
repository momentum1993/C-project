namespace Shisen_Sho {
    partial class Board {
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.tmrPath = new System.Windows.Forms.Timer(this.components);
            this.tmrGame = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // tmrPath
            // 
            this.tmrPath.Tick += new System.EventHandler(this.tmrPath_Tick);
            // 
            // tmrGame
            // 
            this.tmrGame.Interval = 1000;
            // 
            // Board
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "Board";
            this.Size = new System.Drawing.Size(175, 138);
            this.Load += new System.EventHandler(this.Board_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Board_MouseDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer tmrPath;
        public System.Windows.Forms.Timer tmrGame;
    }
}
