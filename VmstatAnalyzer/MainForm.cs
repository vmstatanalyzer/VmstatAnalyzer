using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
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

        private void OpenFile()
        {
            if (DialogResult.OK == openFileDialog.ShowDialog())
            {
                string fileName = openFileDialog.FileName;

                DataForm form = new DataForm();
                form.MdiParent = this;
                form.Dock = DockStyle.Fill;
                form.Parent = panel;
                form.Show();
            }
        }

        private void AddRemote()
        {
            AddRemoteHostForm form = new AddRemoteHostForm();
            form.ShowDialog();
        }
    }
}
