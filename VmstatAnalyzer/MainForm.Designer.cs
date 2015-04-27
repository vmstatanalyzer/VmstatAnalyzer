namespace VmstatAnalyzer
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.ribbon = new System.Windows.Forms.Ribbon();
            this.ribbonOrbMenuItemOpen = new System.Windows.Forms.RibbonOrbMenuItem();
            this.ribbonSeparator1 = new System.Windows.Forms.RibbonSeparator();
            this.ribbonOrbMenuItemAbout = new System.Windows.Forms.RibbonOrbMenuItem();
            this.ribbonSeparator2 = new System.Windows.Forms.RibbonSeparator();
            this.ribbonOrbMenuItemClose = new System.Windows.Forms.RibbonOrbMenuItem();
            this.ribbonOrbOptionButtonExit = new System.Windows.Forms.RibbonOrbOptionButton();
            this.ribbonOrbOptionButtonOption = new System.Windows.Forms.RibbonOrbOptionButton();
            this.ribbonTab1 = new System.Windows.Forms.RibbonTab();
            this.ribbonPanel1 = new System.Windows.Forms.RibbonPanel();
            this.ribbonButtonOpenFile = new System.Windows.Forms.RibbonButton();
            this.ribbonPanel2 = new System.Windows.Forms.RibbonPanel();
            this.ribbonButtonAddRemote = new System.Windows.Forms.RibbonButton();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ribbon.Minimized = false;
            this.ribbon.Name = "ribbon";
            // 
            // 
            // 
            this.ribbon.OrbDropDown.BorderRoundness = 8;
            this.ribbon.OrbDropDown.Location = new System.Drawing.Point(0, 0);
            this.ribbon.OrbDropDown.MenuItems.Add(this.ribbonOrbMenuItemOpen);
            this.ribbon.OrbDropDown.MenuItems.Add(this.ribbonSeparator1);
            this.ribbon.OrbDropDown.MenuItems.Add(this.ribbonOrbMenuItemAbout);
            this.ribbon.OrbDropDown.MenuItems.Add(this.ribbonSeparator2);
            this.ribbon.OrbDropDown.MenuItems.Add(this.ribbonOrbMenuItemClose);
            this.ribbon.OrbDropDown.Name = "";
            this.ribbon.OrbDropDown.OptionItems.Add(this.ribbonOrbOptionButtonExit);
            this.ribbon.OrbDropDown.OptionItems.Add(this.ribbonOrbOptionButtonOption);
            this.ribbon.OrbDropDown.Size = new System.Drawing.Size(527, 210);
            this.ribbon.OrbDropDown.TabIndex = 0;
            this.ribbon.OrbImage = null;
            this.ribbon.OrbStyle = System.Windows.Forms.RibbonOrbStyle.Office_2013;
            this.ribbon.OrbText = "File";
            this.ribbon.RibbonTabFont = new System.Drawing.Font("Trebuchet MS", 9F);
            this.ribbon.Size = new System.Drawing.Size(684, 131);
            this.ribbon.TabIndex = 0;
            this.ribbon.Tabs.Add(this.ribbonTab1);
            this.ribbon.TabsMargin = new System.Windows.Forms.Padding(12, 26, 20, 0);
            this.ribbon.Text = "ribbon";
            this.ribbon.ThemeColor = System.Windows.Forms.RibbonTheme.Blue;
            // 
            // ribbonOrbMenuItemOpen
            // 
            this.ribbonOrbMenuItemOpen.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.ribbonOrbMenuItemOpen.Image = global::VmstatAnalyzer.Properties.Resources.open32;
            this.ribbonOrbMenuItemOpen.SmallImage = global::VmstatAnalyzer.Properties.Resources.open32;
            this.ribbonOrbMenuItemOpen.Text = "Open";
            this.ribbonOrbMenuItemOpen.Click += new System.EventHandler(this.ribbonOrbMenuItemOpen_Click);
            // 
            // ribbonOrbMenuItemAbout
            // 
            this.ribbonOrbMenuItemAbout.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.ribbonOrbMenuItemAbout.Image = global::VmstatAnalyzer.Properties.Resources.addons32;
            this.ribbonOrbMenuItemAbout.SmallImage = global::VmstatAnalyzer.Properties.Resources.addons32;
            this.ribbonOrbMenuItemAbout.Text = "About VmstatAnalyzer";
            this.ribbonOrbMenuItemAbout.Click += new System.EventHandler(this.ribbonOrbMenuItemAbout_Click);
            // 
            // ribbonOrbMenuItemClose
            // 
            this.ribbonOrbMenuItemClose.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.ribbonOrbMenuItemClose.Image = global::VmstatAnalyzer.Properties.Resources.close32;
            this.ribbonOrbMenuItemClose.SmallImage = global::VmstatAnalyzer.Properties.Resources.close32;
            this.ribbonOrbMenuItemClose.Text = "Close";
            this.ribbonOrbMenuItemClose.Click += new System.EventHandler(this.ribbonOrbMenuItemClose_Click);
            // 
            // ribbonOrbOptionButtonExit
            // 
            this.ribbonOrbOptionButtonExit.AltKey = "";
            this.ribbonOrbOptionButtonExit.Image = global::VmstatAnalyzer.Properties.Resources.exit16;
            this.ribbonOrbOptionButtonExit.SmallImage = global::VmstatAnalyzer.Properties.Resources.exit16;
            this.ribbonOrbOptionButtonExit.Text = "Exit VmstatAnalyzer";
            this.ribbonOrbOptionButtonExit.Click += new System.EventHandler(this.ribbonOrbOptionButtonExit_Click);
            // 
            // ribbonOrbOptionButtonOption
            // 
            this.ribbonOrbOptionButtonOption.Image = global::VmstatAnalyzer.Properties.Resources.options16;
            this.ribbonOrbOptionButtonOption.SmallImage = global::VmstatAnalyzer.Properties.Resources.options16;
            this.ribbonOrbOptionButtonOption.Text = "VmstatAnalyzer Option";
            // 
            // ribbonTab1
            // 
            this.ribbonTab1.Panels.Add(this.ribbonPanel1);
            this.ribbonTab1.Panels.Add(this.ribbonPanel2);
            this.ribbonTab1.Text = "Home";
            // 
            // ribbonPanel1
            // 
            this.ribbonPanel1.Items.Add(this.ribbonButtonOpenFile);
            this.ribbonPanel1.Text = "Local";
            // 
            // ribbonButtonOpenFile
            // 
            this.ribbonButtonOpenFile.Image = global::VmstatAnalyzer.Properties.Resources.open32;
            this.ribbonButtonOpenFile.SmallImage = global::VmstatAnalyzer.Properties.Resources.open16;
            this.ribbonButtonOpenFile.Text = "Open";
            this.ribbonButtonOpenFile.Click += new System.EventHandler(this.ribbonButtonOpenFile_Click);
            // 
            // ribbonPanel2
            // 
            this.ribbonPanel2.Items.Add(this.ribbonButtonAddRemote);
            this.ribbonPanel2.Text = "Remote";
            // 
            // ribbonButtonAddRemote
            // 
            this.ribbonButtonAddRemote.Image = global::VmstatAnalyzer.Properties.Resources.network32;
            this.ribbonButtonAddRemote.SmallImage = global::VmstatAnalyzer.Properties.Resources.network24;
            this.ribbonButtonAddRemote.Text = "Add";
            this.ribbonButtonAddRemote.Click += new System.EventHandler(this.ribbonButtonAddRemote_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "All Files|*.*";
            // 
            // statusStrip
            // 
            this.statusStrip.Location = new System.Drawing.Point(0, 490);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(684, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 512);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.ribbon);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainForm";
            this.Text = "VmstatAnalyzer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Ribbon ribbon;
        private System.Windows.Forms.RibbonTab ribbonTab1;
        private System.Windows.Forms.RibbonPanel ribbonPanel1;
        private System.Windows.Forms.RibbonButton ribbonButtonOpenFile;
        private System.Windows.Forms.RibbonPanel ribbonPanel2;
        private System.Windows.Forms.RibbonButton ribbonButtonAddRemote;
        private System.Windows.Forms.RibbonOrbMenuItem ribbonOrbMenuItemOpen;
        private System.Windows.Forms.RibbonOrbOptionButton ribbonOrbOptionButtonExit;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.RibbonOrbOptionButton ribbonOrbOptionButtonOption;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.RibbonSeparator ribbonSeparator1;
        private System.Windows.Forms.RibbonOrbMenuItem ribbonOrbMenuItemAbout;
        private System.Windows.Forms.RibbonSeparator ribbonSeparator2;
        private System.Windows.Forms.RibbonOrbMenuItem ribbonOrbMenuItemClose;
    }
}