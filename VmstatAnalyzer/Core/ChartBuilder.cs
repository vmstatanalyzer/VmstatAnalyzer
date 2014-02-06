using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace VmstatAnalyzer.Core
{
    public class ChartBuilder
    {
        public void Build(OS os)
        {
            InitOverviewCPUChart(OverviewCPUChart);
            InitOverviewMemoryChart(OverviewMemoryChart, os);
            InitOverviewThreadsChart(OverviewThreadsChart, os);
            InitOverviewFaultsChart(OverviewFaultsChart, os);

            InitCPUChart(CPUChart, os);
            InitCPUTotalChart(CPUTotalChart);

            InitMemoryAvmChart(MemoryAvmChart, os);
            InitMemoryFreeChart(MemoryFreeChart, os);

            InitThreadsRChart(ThreadsRChart, os);
            InitThreadsBChart(ThreadsBChart, os);

            InitInterruptChart(InterruptChart, os);
            InitContextSwitchesChart(ContextSwitchesChart, os);
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
            series.XValueMember = "time";
            series.YValueMembers = "cpu";
        }

        public void InitOverviewMemoryChart(Chart chart, OS os)
        {
            Series series = null;
            series = chart.Series[0];

            if (os == OS.AIX)
            {
                series.XValueMember = "time";
                series.YValueMembers = "memory_fre";
            }
            else
            {
                series.XValueMember = "time";
                series.YValueMembers = "memory_free";
            }
        }

        public void InitOverviewThreadsChart(Chart chart, OS os)
        {
            Series series = null;

            if (os == OS.AIX)
            {
                series = chart.Series[0];
                series.Name = "kthr_r";
                series.XValueMember = "time";
                series.YValueMembers = "kthr_r";

                series = chart.Series[1];
                series.Name = "kthr_b";
                series.XValueMember = "time";
                series.YValueMembers = "kthr_b";

                series = chart.Series[2];
                chart.Series.Remove(series);
            }
            else if (os == OS.BSD || os == OS.HPUX || os == OS.Solaris)
            {
                series = chart.Series[0];
                series.Name = "procs_r";
                series.XValueMember = "time";
                series.YValueMembers = "procs_r";

                series = chart.Series[1];
                series.Name = "procs_b";
                series.XValueMember = "time";
                series.YValueMembers = "procs_b";

                series = chart.Series[2];
                series.Name = "procs_w";
                series.XValueMember = "time";
                series.YValueMembers = "procs_w";
            }
            else if (os == OS.Linux || os == OS.Linux2541)
            {
                series = chart.Series[0];
                series.Name = "procs_r";
                series.XValueMember = "time";
                series.YValueMembers = "procs_r";

                series = chart.Series[1];
                series.Name = "procs_b";
                series.XValueMember = "time";
                series.YValueMembers = "procs_b";

                series = chart.Series[2];
                chart.Series.Remove(series);
            }
        }

        public void InitOverviewFaultsChart(Chart chart, OS os)
        {
            Series series = null;

            if (os == OS.Linux || os == OS.Linux2541)
            {
                series = chart.Series[0];
                series.Name = "system_cs";
                series.XValueMember = "time";
                series.YValueMembers = "system_cs";

                series = chart.Series[1];
                series.Name = "system_in";
                series.XValueMember = "time";
                series.YValueMembers = "system_in";

                series = chart.Series[2];
                chart.Series.Remove(series);
            }
            else
            {
                series = chart.Series[0];
                series.Name = "faults_cs";
                series.XValueMember = "time";
                series.YValueMembers = "faults_cs";

                series = chart.Series[1];
                series.Name = "faults_in";
                series.XValueMember = "time";
                series.YValueMembers = "faults_in";

                series = chart.Series[2];
                series.Name = "faults_sy";
                series.XValueMember = "time";
                series.YValueMembers = "faults_sy";
            }
        }

        public void InitCPUChart(Chart chart, OS os)
        {
            Series series = null;
            series = chart.Series["cpu_us"];
            series.XValueMember = "time";
            series.YValueMembers = "cpu_us";

            series = chart.Series["cpu_sy"];
            series.XValueMember = "time";
            series.YValueMembers = "cpu_sy";

            if (os == OS.AIX || os == OS.Linux || os == OS.Linux2541)
            {
                series = chart.Series["cpu_wa"];
                series.XValueMember = "time";
                series.YValueMembers = "cpu_wa";
            }
            else
            {
                series = chart.Series["cpu_wa"];
                chart.Series.Remove(series);
            }

            if (os == OS.Linux)
            {
                series = chart.Series["cpu_st"];
                series.XValueMember = "time";
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
            series.XValueMember = "time";
            series.YValueMembers = "cpu";
        }

        public void InitMemoryAvmChart(Chart chart, OS os)
        {
            Series series = null;
            series = chart.Series[0];

            if (os == OS.Linux || os == OS.Linux2541)
            {
                series.Name = "memory_swpd";
                series.XValueMember = "time";
                series.YValueMembers = "memory_swpd";

                series = chart.Series[1];
                series.Name = "memory_buff";
                series.XValueMember = "time";
                series.YValueMembers = "memory_buff";

                series = chart.Series[2];
                series.Name = "memory_cache";
                series.XValueMember = "time";
                series.YValueMembers = "memory_cache";
            }
            else if (os == OS.Solaris)
            {
                series.Name = "memory_swap";
                series.XValueMember = "time";
                series.YValueMembers = "memory_swap";

                series = chart.Series["Series2"];
                chart.Series.Remove(series);

                series = chart.Series["Series3"];
                chart.Series.Remove(series);
            }
            else
            {
                series.Name = "memory_avm";
                series.XValueMember = "time";
                series.YValueMembers = "memory_avm";

                series = chart.Series["Series2"];
                chart.Series.Remove(series);

                series = chart.Series["Series3"];
                chart.Series.Remove(series);
            }
        }

        public void InitMemoryFreeChart(Chart chart, OS os)
        {
            Series series = null;
            series = chart.Series[0];

            if (os == OS.AIX)
            {
                series.Name = "memory_fre";
                series.XValueMember = "time";
                series.YValueMembers = "memory_fre";
            }
            else
            {
                series.Name = "memory_free";
                series.XValueMember = "time";
                series.YValueMembers = "memory_free";
            }
        }

        public void InitThreadsRChart(Chart chart, OS os)
        {
            Series series = null;
            if (os == OS.AIX)
            {
                series = chart.Series[0];
                series.Name = "kthr_r";
                series.XValueMember = "time";
                series.YValueMembers = "kthr_r";
            }
            else
            {
                series = chart.Series[0];
                series.Name = "procs_r";
                series.XValueMember = "time";
                series.YValueMembers = "procs_r";
            }
        }

        public void InitThreadsBChart(Chart chart, OS os)
        {
            Series series = null;
            if (os == OS.AIX)
            {
                series = chart.Series[0];
                series.Name = "kthr_b";
                series.XValueMember = "time";
                series.YValueMembers = "kthr_b";

                series = chart.Series[1];
                chart.Series.Remove(series);
            }
            else if (os == OS.BSD || os == OS.HPUX || os == OS.Solaris)
            {
                series = chart.Series[0];
                series.Name = "procs_b";
                series.XValueMember = "time";
                series.YValueMembers = "procs_b";

                series = chart.Series[1];
                series.Name = "procs_w";
                series.XValueMember = "time";
                series.YValueMembers = "procs_w";
            }
            else if (os == OS.Linux || os == OS.Linux2541)
            {
                series = chart.Series[0];
                series.Name = "procs_b";
                series.XValueMember = "time";
                series.YValueMembers = "procs_b";

                series = chart.Series[1];
                chart.Series.Remove(series);
            }
        }

        public void InitInterruptChart(Chart chart, OS os)
        {
            Series series = null;

            if (os == OS.Linux || os == OS.Linux2541)
            {
                series = chart.Series[0];
                series.Name = "system_in";
                series.XValueMember = "time";
                series.YValueMembers = "system_in";
            }
            else
            {
                series = chart.Series[0];
                series.Name = "faults_in";
                series.XValueMember = "time";
                series.YValueMembers = "faults_in";
            }
        }

        public void InitContextSwitchesChart(Chart chart, OS os)
        {
            Series series = null;

            if (os == OS.Linux || os == OS.Linux2541)
            {
                series = chart.Series[0];
                series.Name = "system_cs";
                series.XValueMember = "time";
                series.YValueMembers = "system_cs";
            }
            else
            {
                series = chart.Series[0];
                series.Name = "faults_cs";
                series.XValueMember = "time";
                series.YValueMembers = "faults_cs";
            }
        }

        public void InitPageChart(Chart chart, OS os, string name)
        {
            Series series = null;
            series = chart.Series[0];
            series.Name = name;
            series.XValueMember = "time";
            series.YValueMembers = name;

            Title title = null;
            title = chart.Titles[0];
            title.Text = name;
        }
    }
}
