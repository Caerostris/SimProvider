namespace SimProvider.Window
{
    partial class MainWindow
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
            this.buttonEditor = new System.Windows.Forms.Button();
            this.buttonCalculation = new System.Windows.Forms.Button();
            this.buttonMultiCalculation = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.DragAndDropBox = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonSimulation = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.DragAndDropBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonEditor
            // 
            this.buttonEditor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEditor.Location = new System.Drawing.Point(12, 12);
            this.buttonEditor.Name = "buttonEditor";
            this.buttonEditor.Size = new System.Drawing.Size(536, 23);
            this.buttonEditor.TabIndex = 0;
            this.buttonEditor.Text = "Editor";
            this.buttonEditor.UseVisualStyleBackColor = true;
            this.buttonEditor.Click += new System.EventHandler(this.buttonEditor_Click);
            // 
            // buttonCalculation
            // 
            this.buttonCalculation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCalculation.Location = new System.Drawing.Point(12, 41);
            this.buttonCalculation.Name = "buttonCalculation";
            this.buttonCalculation.Size = new System.Drawing.Size(536, 23);
            this.buttonCalculation.TabIndex = 1;
            this.buttonCalculation.Text = "new Calculation";
            this.buttonCalculation.UseVisualStyleBackColor = true;
            this.buttonCalculation.Click += new System.EventHandler(this.buttonCalculation_Click);
            // 
            // buttonMultiCalculation
            // 
            this.buttonMultiCalculation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMultiCalculation.Location = new System.Drawing.Point(12, 70);
            this.buttonMultiCalculation.Name = "buttonMultiCalculation";
            this.buttonMultiCalculation.Size = new System.Drawing.Size(536, 23);
            this.buttonMultiCalculation.TabIndex = 2;
            this.buttonMultiCalculation.Text = "new multi Calculation";
            this.buttonMultiCalculation.UseVisualStyleBackColor = true;
            this.buttonMultiCalculation.Click += new System.EventHandler(this.buttonMultiCalculation_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonSimulation);
            this.panel1.Controls.Add(this.buttonMultiCalculation);
            this.panel1.Controls.Add(this.buttonCalculation);
            this.panel1.Controls.Add(this.buttonEditor);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(558, 146);
            this.panel1.TabIndex = 3;
            // 
            // DragAndDropBox
            // 
            this.DragAndDropBox.AllowDrop = true;
            this.DragAndDropBox.BackColor = System.Drawing.SystemColors.ControlLight;
            this.DragAndDropBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DragAndDropBox.Controls.Add(this.label1);
            this.DragAndDropBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.DragAndDropBox.Location = new System.Drawing.Point(0, 146);
            this.DragAndDropBox.Name = "DragAndDropBox";
            this.DragAndDropBox.Size = new System.Drawing.Size(558, 100);
            this.DragAndDropBox.TabIndex = 4;
            this.DragAndDropBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.DragAndDropBox_DragDrop);
            this.DragAndDropBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.DragAndDropBox_DragEnter);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(27, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(509, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Zum Laden von gespeicherten daten bitte Datei herein ziehen.";
            // 
            // buttonSimulation
            // 
            this.buttonSimulation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSimulation.Location = new System.Drawing.Point(12, 111);
            this.buttonSimulation.Name = "buttonSimulation";
            this.buttonSimulation.Size = new System.Drawing.Size(536, 23);
            this.buttonSimulation.TabIndex = 3;
            this.buttonSimulation.Text = "new Simulation";
            this.buttonSimulation.UseVisualStyleBackColor = true;
            this.buttonSimulation.Click += new System.EventHandler(this.buttonSimulation_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(558, 242);
            this.Controls.Add(this.DragAndDropBox);
            this.Controls.Add(this.panel1);
            this.Name = "MainWindow";
            this.Text = "MainWindow";
            this.panel1.ResumeLayout(false);
            this.DragAndDropBox.ResumeLayout(false);
            this.DragAndDropBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonEditor;
        private System.Windows.Forms.Button buttonCalculation;
        private System.Windows.Forms.Button buttonMultiCalculation;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel DragAndDropBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonSimulation;
    }
}