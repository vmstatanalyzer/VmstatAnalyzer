namespace VmstatAnalyzer.View
{
    partial class DataForm
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
            this.vChart1 = new VmstatAnalyzer.View.Component.VChart();
            this.SuspendLayout();
            // 
            // vChart1
            // 
            this.vChart1.Location = new System.Drawing.Point(12, 12);
            this.vChart1.Name = "vChart1";
            this.vChart1.Size = new System.Drawing.Size(314, 230);
            this.vChart1.TabIndex = 0;
            // 
            // DataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(338, 254);
            this.Controls.Add(this.vChart1);
            this.Name = "DataForm";
            this.Text = "DataForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.DataForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Component.VChart vChart1;

    }
}