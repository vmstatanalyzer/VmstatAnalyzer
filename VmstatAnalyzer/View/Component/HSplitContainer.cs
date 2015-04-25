using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VmstatAnalyzer.View.Component
{
    public partial class HSplitContainer : SplitContainer
    {
        public HSplitContainer()
        {
            InitializeComponent();
        }

        public void CollapsePanel1(bool collapse)
        {
            this.Panel1Collapsed = collapse;
            SetVisible();
        }

        public void CollapsePanel2(bool collapse)
        {
            this.Panel2Collapsed = collapse;
            SetVisible();
        }

        private void SetVisible()
        {
            this.Visible = !(this.Panel1Collapsed || this.Panel2Collapsed);
        }
    }
}
