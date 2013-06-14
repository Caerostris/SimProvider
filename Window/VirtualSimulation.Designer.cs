namespace SimProvider.Window
{
    partial class VirtualSimulation
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
            this.Updater = new System.Windows.Forms.Timer(this.components);
            this.fpsLable = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // glc
            // 
            this.glc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.glc.BackColor = System.Drawing.Color.Black;
            this.glc.Location = new System.Drawing.Point(2, 2);
            this.glc.Name = "glc";
            this.glc.Size = new System.Drawing.Size(780, 558);
            this.glc.TabIndex = 0;
            this.glc.VSync = false;
            // 
            // Updater
            // 
            this.Updater.Enabled = true;
            this.Updater.Interval = 1;
            this.Updater.Tick += new System.EventHandler(this.Updater_Tick);
            // 
            // fpsLable
            // 
            this.fpsLable.AutoSize = true;
            this.fpsLable.BackColor = System.Drawing.Color.Transparent;
            this.fpsLable.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fpsLable.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.fpsLable.Location = new System.Drawing.Point(689, 9);
            this.fpsLable.Name = "fpsLable";
            this.fpsLable.Size = new System.Drawing.Size(49, 17);
            this.fpsLable.TabIndex = 1;
            this.fpsLable.Text = "Fps : ";
            // 
            // VirtualSimulation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.fpsLable);
            this.Controls.Add(this.glc);
            this.Name = "VirtualSimulation";
            this.Text = "VirtualSimulation";
            this.Load += new System.EventHandler(this.VirtualSimulation_Load);
            this.ResizeEnd += new System.EventHandler(this.VirtualSimulation_ResizeEnd);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer Updater;
        private System.Windows.Forms.Label fpsLable;
    }
}