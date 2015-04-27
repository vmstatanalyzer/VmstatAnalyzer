using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace VmstatAnalyzer.View.Component
{
    public partial class CPUChart : VChart
    {
        public CPUChart()
        {
            InitializeComponent();
        }

        public void SetDataSource(DataTable table)
        {
            chart.Series.Clear();

            DataView dataView = table.AsDataView();
            


            chart.DataSource = dataView;
            chart.DataBind();
        }

        public DataTable CreateCPUTable()
        {
            DataTable table = new DataTable();
            

            return table;
        }

        private Series CreateTotalSeries()
        {
            Series series = new Series("Total");
            series.YValueMembers = "cpu_total";
            series.XValueMember = "log_time";
            series.YValueType = ChartValueType.Double;

            return series;
        }

        private Series CreateUserSeries()
        {
            Series series = new Series("us");
            series.YValueMembers = "cpu_us";
            series.XValueMember = "log_time";
            series.YValueType = ChartValueType.Double;

            return series;
        }

        private Series CreateSystemSeries()
        {
            Series series = new Series("sy");
            series.YValueMembers = "cpu_sy";
            series.XValueMember = "log_time";
            series.YValueType = ChartValueType.Double;

            return series;
        }

        private Series CreateIdleSeries()
        {
            Series series = new Series("id");
            series.YValueMembers = "cpu_id";
            series.XValueMember = "log_time";
            series.YValueType = ChartValueType.Double;

            return series;
        }

        private Series CreateWaitSeries()
        {
            Series series = new Series("wa");
            series.YValueMembers = "cpu_wa";
            series.XValueMember = "log_time";
            series.YValueType = ChartValueType.Double;

            return series;
        }
    }
}
