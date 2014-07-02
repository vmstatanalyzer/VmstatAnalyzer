using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VmstatAnalyzer.Repository;

namespace VmstatAnalyzer
{
    public partial class VChartForm : Form
    {
        public VChartForm()
        {
            InitializeComponent();
        }

        private void VChartForm_Load(object sender, EventArgs e)
        {
            const string FILENAME = "./Data/ourhome63_1228_vmstat.log";

            VmstatDataLoader dataLoader = new VmstatDataLoader();
            DataTable table = dataLoader.LoadData(FILENAME);

            Console.WriteLine(table.Rows.Count);

            vChart1.SetDataView(table.AsDataView());
        }
    }
}
