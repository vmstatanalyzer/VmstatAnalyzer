using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using VmstatAnalyzer.Domain;
using VmstatAnalyzer.Repository;

namespace VmstatAnalyzer.View
{
    public class ChartBuilder
    {
        public void Build(OSTypes osType)
        {
            InitOverviewCPUChart(OverviewCPUChart);
            InitOverviewMemoryChart(OverviewMemoryChart, osType);
            InitOverviewThreadsChart(OverviewThreadsChart, osType);
            InitOverviewFaultsChart(OverviewFaultsChart, osType);

            InitCPUChart(CPUChart, osType);
            InitCPUTotalChart(CPUTotalChart);

            InitMemoryAvmChart(MemoryAvmChart, osType);
            InitMemoryFreeChart(MemoryFreeChart, osType);

            InitThreadsRChart(ThreadsRChart, osType);
            InitThreadsBChart(ThreadsBChart, osType);

            InitInterruptChart(InterruptChart, osType);
            InitContextSwitchesChart(ContextSwitchesChart, osType);
        }

        public Chart CPUChart { get; set; }

        public Chart CPUTotalChart { get; set; }

        public Chart MemoryAvmChart { get; set; }

        public Chart MemoryFreeChart { get; set; }

        public Chart ThreadsRChart { get; set; }

        public Chart ThreadsBChart { get; set; }

        public Chart InterruptChart { get; set; }

        public Chart ContextSwitchesChart { get; set; }

        public Chart PageChart { get; set; }

        public Chart OverviewCPUChart { get; set; }

        public Chart OverviewMemoryChart { get; set; }

        public Chart OverviewThreadsChart { get; set; }

        public Chart OverviewFaultsChart { get; set; }

        public void InitOverviewCPUChart(Chart chart)
        {
            Series series = null;
            series = chart.Series[0];
            series.Name = "CPU";
            series.XValueMember = VmstatColumns.COLUMN_DATETIME;
            series.YValueMembers = VmstatColumns.COLUMN_CPU;
        }

        public void InitOverviewMemoryChart(Chart chart, OSTypes os)
        {
            Series series = null;
            series = chart.Series[0];

            if (os == OSTypes.AIX)
            {
                series.XValueMember = VmstatColumns.COLUMN_DATETIME;
                series.YValueMembers = "memory_fre";
            }
            else
            {
                series.XValueMember = VmstatColumns.COLUMN_DATETIME;
                series.YValueMembers = "memory_free";
            }
        }

        public void InitOverviewThreadsChart(Chart chart, OSTypes os)
        {
            Series series = null;

            if (os == OSTypes.AIX)
            {
                series = chart.Series[0];
                series.Name = "kthr_r";
                series.XValueMember = VmstatColumns.COLUMN_DATETIME;
                series.YValueMembers = "kthr_r";

                series = chart.Series[1];
                series.Name = "kthr_b";
                series.XValueMember = VmstatColumns.COLUMN_DATETIME;
                series.YValueMembers = "kthr_b";

                series = chart.Series[2];
                chart.Series.Remove(series);
            }
            else if (os == OSTypes.BSD || os == OSTypes.HPUX || os == OSTypes.Solaris)
            {
                series = chart.Series[0];
                series.Name = "procs_r";
                series.XValueMember = VmstatColumns.COLUMN_DATETIME;
                series.YValueMembers = "procs_r";

                series = chart.Series[1];
                series.Name = "procs_b";
                series.XValueMember = VmstatColumns.COLUMN_DATETIME;
                series.YValueMembers = "procs_b";

                series = chart.Series[2];
                series.Name = "procs_w";
                series.XValueMember = VmstatColumns.COLUMN_DATETIME;
                series.YValueMembers = "procs_w";
            }
            else if (os == OSTypes.Linux)
            {
                series = chart.Series[0];
                series.Name = "procs_r";
                series.XValueMember = VmstatColumns.COLUMN_DATETIME;
                series.YValueMembers = "procs_r";

                series = chart.Series[1];
                series.Name = "procs_b";
                series.XValueMember = VmstatColumns.COLUMN_DATETIME;
                series.YValueMembers = "procs_b";

                series = chart.Series[2];
                chart.Series.Remove(series);
            }
        }

        public void InitOverviewFaultsChart(Chart chart, OSTypes os)
        {
            Series series = null;

            if (os == OSTypes.Linux)
            {
                series = chart.Series[0];
                series.Name = "system_cs";
                series.XValueMember = VmstatColumns.COLUMN_DATETIME;
                series.YValueMembers = "system_cs";

                series = chart.Series[1];
                series.Name = "system_in";
                series.XValueMember = VmstatColumns.COLUMN_DATETIME;
                series.YValueMembers = "system_in";

                series = chart.Series[2];
                chart.Series.Remove(series);
            }
            else
            {
                series = chart.Series[0];
                series.Name = "faults_cs";
                series.XValueMember = VmstatColumns.COLUMN_DATETIME;
                series.YValueMembers = "faults_cs";

                series = chart.Series[1];
                series.Name = "faults_in";
                series.XValueMember = VmstatColumns.COLUMN_DATETIME;
                series.YValueMembers = "faults_in";

                series = chart.Series[2];
                series.Name = "faults_sy";
                series.XValueMember = VmstatColumns.COLUMN_DATETIME;
                series.YValueMembers = "faults_sy";
            }
        }

        public void InitCPUChart(Chart chart, OSTypes os)
        {
            Series series = null;
            series = chart.Series["cpu_us"];
            series.XValueMember = VmstatColumns.COLUMN_DATETIME;
            series.YValueMembers = "cpu_us";

            series = chart.Series["cpu_sy"];
            series.XValueMember = VmstatColumns.COLUMN_DATETIME;
            series.YValueMembers = "cpu_sy";

            if (os == OSTypes.AIX || os == OSTypes.Linux)
            {
                series = chart.Series["cpu_wa"];
                series.XValueMember = VmstatColumns.COLUMN_DATETIME;
                series.YValueMembers = "cpu_wa";
            }
            else
            {
                series = chart.Series["cpu_wa"];
                chart.Series.Remove(series);
            }

            if (os == OSTypes.Linux)
            {
                series = chart.Series["cpu_st"];
                series.XValueMember = VmstatColumns.COLUMN_DATETIME;
                series.YValueMembers = "cpu_st";
            }
            else
            {
                series = chart.Series["cpu_st"];
                chart.Series.Remove(series);
            }
        }

        public void InitCPUTotalChart(Chart chart)
        {
            Series series = null;
            series = chart.Series[0];
            series.XValueMember = VmstatColumns.COLUMN_DATETIME;
            series.YValueMembers = "cpu";
        }

        public void InitMemoryAvmChart(Chart chart, OSTypes os)
        {
            Series series = null;
            series = chart.Series[0];

            if (os == OSTypes.Linux)
            {
                series.Name = "memory_swpd";
                series.XValueMember = VmstatColumns.COLUMN_DATETIME;
                series.YValueMembers = "memory_swpd";

                series = chart.Series[1];
                series.Name = "memory_buff";
                series.XValueMember = VmstatColumns.COLUMN_DATETIME;
                series.YValueMembers = "memory_buff";

                series = chart.Series[2];
                series.Name = "memory_cache";
                series.XValueMember = VmstatColumns.COLUMN_DATETIME;
                series.YValueMembers = "memory_cache";
            }
            else if (os == OSTypes.Solaris)
            {
                series.Name = "memory_swap";
                series.XValueMember = VmstatColumns.COLUMN_DATETIME;
                series.YValueMembers = "memory_swap";

                series = chart.Series["Series2"];
                chart.Series.Remove(series);

                series = chart.Series["Series3"];
                chart.Series.Remove(series);
            }
            else
            {
                series.Name = "memory_avm";
                series.XValueMember = VmstatColumns.COLUMN_DATETIME;
                series.YValueMembers = "memory_avm";

                series = chart.Series["Series2"];
                chart.Series.Remove(series);

                series = chart.Series["Series3"];
                chart.Series.Remove(series);
            }
        }

        public void InitMemoryFreeChart(Chart chart, OSTypes os)
        {
            Series series = null;
            series = chart.Series[0];

            if (os == OSTypes.AIX)
            {
                series.Name = "memory_fre";
                series.XValueMember = VmstatColumns.COLUMN_DATETIME;
                series.YValueMembers = "memory_fre";
            }
            else
            {
                series.Name = "memory_free";
                series.XValueMember = VmstatColumns.COLUMN_DATETIME;
                series.YValueMembers = "memory_free";
            }
        }

        public void InitThreadsRChart(Chart chart, OSTypes os)
        {
            Series series = null;
            if (os == OSTypes.AIX)
            {
                series = chart.Series[0];
                series.Name = "kthr_r";
                series.XValueMember = VmstatColumns.COLUMN_DATETIME;
                series.YValueMembers = "kthr_r";
            }
            else
            {
                series = chart.Series[0];
                series.Name = "procs_r";
                series.XValueMember = VmstatColumns.COLUMN_DATETIME;
                series.YValueMembers = "procs_r";
            }
        }

        public void InitThreadsBChart(Chart chart, OSTypes os)
        {
            Series series = null;
            if (os == OSTypes.AIX)
            {
                series = chart.Series[0];
                series.Name = "kthr_b";
                series.XValueMember = VmstatColumns.COLUMN_DATETIME;
                series.YValueMembers = "kthr_b";

                series = chart.Series[1];
                chart.Series.Remove(series);
            }
            else if (os == OSTypes.BSD || os == OSTypes.HPUX || os == OSTypes.Solaris)
            {
                series = chart.Series[0];
                series.Name = "procs_b";
                series.XValueMember = VmstatColumns.COLUMN_DATETIME;
                series.YValueMembers = "procs_b";

                series = chart.Series[1];
                series.Name = "procs_w";
                series.XValueMember = VmstatColumns.COLUMN_DATETIME;
                series.YValueMembers = "procs_w";
            }
            else if (os == OSTypes.Linux)
            {
                series = chart.Series[0];
                series.Name = "procs_b";
                series.XValueMember = VmstatColumns.COLUMN_DATETIME;
                series.YValueMembers = "procs_b";

                series = chart.Series[1];
                chart.Series.Remove(series);
            }
        }

        public void InitInterruptChart(Chart chart, OSTypes os)
        {
            Series series = null;

            if (os == OSTypes.Linux)
            {
                series = chart.Series[0];
                series.Name = "system_in";
                series.XValueMember = VmstatColumns.COLUMN_DATETIME;
                series.YValueMembers = "system_in";
            }
            else
            {
                series = chart.Series[0];
                series.Name = "faults_in";
                series.XValueMember = VmstatColumns.COLUMN_DATETIME;
                series.YValueMembers = "faults_in";
            }
        }

        public void InitContextSwitchesChart(Chart chart, OSTypes os)
        {
            Series series = null;

            if (os == OSTypes.Linux)
            {
                series = chart.Series[0];
                series.Name = "system_cs";
                series.XValueMember = VmstatColumns.COLUMN_DATETIME;
                series.YValueMembers = "system_cs";
            }
            else
            {
                series = chart.Series[0];
                series.Name = "faults_cs";
                series.XValueMember = VmstatColumns.COLUMN_DATETIME;
                series.YValueMembers = "faults_cs";
            }
        }

        public void InitPageChart(Chart chart, OSTypes os, string name)
        {
            Series series = null;
            series = chart.Series[0];
            series.Name = name;
            series.XValueMember = VmstatColumns.COLUMN_DATETIME;
            series.YValueMembers = name;

            Title title = null;
            title = chart.Titles[0];
            title.Text = name;
        }
    }
}
