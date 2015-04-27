using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VmstatAnalyzer.Core;
using VmstatAnalyzer.View;

namespace VmstatAnalyzer
{
    public partial class MainForm : RibbonForm
    {
        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        private void ribbonButtonOpenFile_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void ribbonButtonAddRemote_Click(object sender, EventArgs e)
        {
            AddRemote();
        }

        private void ribbonOrbMenuItemOpen_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void ribbonOrbMenuItemClose_Click(object sender, EventArgs e)
        {
            CloseMdiChild();
        }

        private void ribbonOrbMenuItemAbout_Click(object sender, EventArgs e)
        {
            ShowAboutBox();
        }

        private void ribbonOrbOptionButtonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OpenFile()
        {
            if (DialogResult.OK == openFileDialog.ShowDialog())
            {
                string fileName = openFileDialog.FileName;

                VmstatFileReader vmstatFileReader = new VmstatFileReader();
                DataTable table = vmstatFileReader.ReadFile(fileName);

                VmstatForm form = new VmstatForm();
                form.Text = openFileDialog.SafeFileName;
                form.MdiParent = this;
                form.Show();

                form.SetDataSource(table);
            }
        }

        private void CloseMdiChild()
        {
            while (this.ActiveMdiChild != null)
            {
                this.ActiveMdiChild.Close();
            }
        }

        private void AddRemote()
        {
            AddRemoteHostForm form = new AddRemoteHostForm();
            form.ShowDialog();
        }

        private void ShowAboutBox()
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.ShowDialog();
        }
    }
}
