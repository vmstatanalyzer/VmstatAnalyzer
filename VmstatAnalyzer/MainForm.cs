using Core.Common.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VmstatAnalyzer.Core;
using VmstatAnalyzer.Properties;
using WeifenLuo.WinFormsUI.Docking;

namespace VmstatAnalyzer
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private OS os = OS.AIX;

        private void MainForm_Load(object sender, EventArgs e)
        {
            int index = 0;

            XmlSerializerFacade facade = new XmlSerializerFacade(typeof(Options), "options.config");
            facade.Deserialize();
            Options options = (Options)facade.Object;

            index = options.OS;

            toolStripComboBoxServerType.SelectedIndex = options.OS;
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExitProgram();
        }

        private void OpenFile()
        {
            if (DialogResult.OK == openFileDialog.ShowDialog())
            {
                string path = openFileDialog.FileName;

                VmstatView vmstatView = new VmstatView();
                vmstatView.RightToLeftLayout = this.RightToLeftLayout;
                vmstatView.Text = openFileDialog.SafeFileName;
                vmstatView.Show(this.dockPanel);
                vmstatView.SetData(path, os);

                AddDocument(vmstatView);
            }
        }

        private void ExitProgram()
        {
            this.Close();
        }

        private void AddDocument(DockContent dockContent)
        {
            ToolStripMenuItem documentMenuItem = new ToolStripMenuItem(dockContent.Text, Resources.document, documentToolStripMenuItem_Click);
            documentMenuItem.CheckOnClick = true;
            documentMenuItem.Checked = true;
            documentMenuItem.Tag = dockContent;
            windowToolStripMenuItem.DropDownItems.Add(documentMenuItem);
        }

        private void documentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DockContent dockContent = (sender as ToolStripMenuItem).Tag as DockContent;
            dockContent.Show();
        }

        private void vmstatAnalyzerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowAboutBox();
        }

        private void appInfoToolStripButton_Click(object sender, EventArgs e)
        {
            ShowAboutBox();
        }

        private void ShowAboutBox()
        {
            AboutBox box = new AboutBox();
            box.ShowDialog();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IDockContent dockContent = this.dockPanel.ActiveDocument;
            if (dockContent != null)
            {
                foreach (ToolStripMenuItem menuItem in windowToolStripMenuItem.DropDownItems)
                {
                    if (dockContent == menuItem.Tag as IDockContent)
                    {
                        windowToolStripMenuItem.DropDownItems.Remove(menuItem);
                        break;
                    }
                }

                dockContent.DockHandler.HideOnClose = false;
                dockContent.DockHandler.Close();
            }
        }

        private void toolStripComboBoxServerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = toolStripComboBoxServerType.SelectedIndex;

            switch (index)
            {
                case 0:
                    os = OS.AIX;
                    break;

                case 1:
                    os = OS.HPUX;
                    break;

                case 2:
                    os = OS.Solaris;
                    break;

                case 3:
                    os = OS.Linux2541;
                    break;

                case 4:
                    os = OS.Linux;
                    break;

                case 5:
                    os = OS.BSD;
                    break;

                default:
                    os = OS.AIX;
                    break;
            }

            Options options = new Options();
            options.OS = index;
            XmlSerializerFacade facade = new XmlSerializerFacade(typeof(Options), "options.config", options);
            facade.Serialize();
        }
    }
}
