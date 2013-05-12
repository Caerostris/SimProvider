namespace SimProvider.Graphics
{
    partial class GLTest
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
            this.glc = new OpenTK.GLControl();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // glc
            // 
            this.glc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.glc.BackColor = System.Drawing.Color.Black;
            this.glc.Location = new System.Drawing.Point(12, 12);
            this.glc.Name = "glc";
            this.glc.Size = new System.Drawing.Size(800, 600);
            this.glc.TabIndex = 0;
            this.glc.VSync = false;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // GLTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(859, 637);
            this.Controls.Add(this.glc);
            this.Name = "GLTest";
            this.Text = "GLTest";
            this.Load += new System.EventHandler(this.GLTest_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private OpenTK.GLControl glc;
        private System.Windows.Forms.Timer timer1;
    }
}