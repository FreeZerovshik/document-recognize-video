namespace Recognition
{
    partial class Form1
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
            this.lvDev = new System.Windows.Forms.ListView();
            this.videoPanel = new System.Windows.Forms.Panel();
            this.RecogCnt = new System.Windows.Forms.NumericUpDown();
            this.pBar = new System.Windows.Forms.ProgressBar();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.RecogCnt)).BeginInit();
            this.SuspendLayout();
            // 
            // lvDev
            // 
            this.lvDev.FullRowSelect = true;
            this.lvDev.GridLines = true;
            this.lvDev.HideSelection = false;
            this.lvDev.Location = new System.Drawing.Point(12, 12);
            this.lvDev.MultiSelect = false;
            this.lvDev.Name = "lvDev";
            this.lvDev.ShowGroups = false;
            this.lvDev.Size = new System.Drawing.Size(237, 21);
            this.lvDev.TabIndex = 2;
            this.lvDev.UseCompatibleStateImageBehavior = false;
            this.lvDev.View = System.Windows.Forms.View.List;
            // 
            // videoPanel
            // 
            this.videoPanel.Location = new System.Drawing.Point(12, 39);
            this.videoPanel.Name = "videoPanel";
            this.videoPanel.Size = new System.Drawing.Size(629, 436);
            this.videoPanel.TabIndex = 4;
            this.videoPanel.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.videoPanel_PreviewKeyDown);
            // 
            // RecogCnt
            // 
            this.RecogCnt.Location = new System.Drawing.Point(563, 13);
            this.RecogCnt.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.RecogCnt.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.RecogCnt.Name = "RecogCnt";
            this.RecogCnt.Size = new System.Drawing.Size(78, 20);
            this.RecogCnt.TabIndex = 9;
            this.RecogCnt.Tag = "";
            this.toolTip1.SetToolTip(this.RecogCnt, "Кол-во попыток распознавания");
            this.RecogCnt.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // pBar
            // 
            this.pBar.Location = new System.Drawing.Point(12, 481);
            this.pBar.Maximum = 50;
            this.pBar.Name = "pBar";
            this.pBar.Size = new System.Drawing.Size(629, 10);
            this.pBar.Step = 1;
            this.pBar.TabIndex = 11;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(653, 493);
            this.Controls.Add(this.pBar);
            this.Controls.Add(this.RecogCnt);
            this.Controls.Add(this.videoPanel);
            this.Controls.Add(this.lvDev);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "SmartRecognize";
            this.toolTip1.SetToolTip(this, "Для запуска распознавания нажмити клавишу \"С\"");
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.RecogCnt)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListView lvDev;
        private System.Windows.Forms.Panel videoPanel;
        private System.Windows.Forms.NumericUpDown RecogCnt;
        private System.Windows.Forms.ProgressBar pBar;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}

