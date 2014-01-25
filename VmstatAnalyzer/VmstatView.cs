﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using VmstatAnalyzer.Core;
using WeifenLuo.WinFormsUI.Docking;

namespace VmstatAnalyzer
{
    public partial class VmstatView : DockContent
    {
        public VmstatView()
        {
            InitializeComponent();

            chartBuilder = new ChartBuilder();

            dataSource = new DataSource();
            dataSource.LoadDataComplete += dataSource_LoadDataComplete;
            dataSource.LoadDataStart += dataSource_LoadDataStart;

            dataSource.SelectDataComplete += dataSource_SelectDataComplete;
            dataSource.SelectDataStart += dataSource_SelectDataStart;
        }

        void dataSource_SelectDataStart(object sender, EventArgs e)
        {
            CallbackSelectDataStart();
        }

        void dataSource_SelectDataComplete(object sender, DataEventArgs e)
        {
            CallbackSelectDataComplete(e.Data as DataView);
        }

        void dataSource_LoadDataStart(object sender, EventArgs e)
        {
            CallbackLoadDataStart();
        }

        void dataSource_LoadDataComplete(object sender, DataEventArgs e)
        {
            CallbackLoadDataComplete((int)e.Data);
        }

        void CallbackSelectDataStart()
        {
            if (this.InvokeRequired)
            {
                Callback callback = new Callback(CallbackSelectDataStart);
                Invoke(callback);
            }
            else
            {
                this.Cursor = Cursors.WaitCursor;
            }
        }

        void CallbackSelectDataComplete(DataView view)
        {
            if (this.InvokeRequired)
            {
                Callback<DataView> callback = new Callback<DataView>(CallbackSelectDataComplete);
                Invoke(callback, new object[] { view });
            }
            else
            {
                bindingSource.DataSource = view;
                UpdateCharts();

                this.Cursor = Cursors.Default;
            }
        }

        void CallbackLoadDataStart()
        {
            if (this.InvokeRequired)
            {
                Callback callback = new Callback(CallbackLoadDataStart);
                Invoke(callback);
            }
            else
            {
                this.Cursor = Cursors.WaitCursor;
            }
        }

        void CallbackLoadDataComplete(int count)
        {
            if (this.InvokeRequired)
            {
                Callback<int> callback = new Callback<int>(CallbackLoadDataComplete);
                Invoke(callback, new object[] { count });
            }
            else
            {
                InitTrackBars(count);

                UpdateStartTime();
                UpdateEndTime();
                UpdateData();

                this.Cursor = Cursors.Default;
            }
        }

        #region Delegates

        delegate void Callback<T>(T value);
        delegate void Callback<TKey, TValue>(TKey key, TValue value);
        delegate void Callback();

        #endregion

        #region Fields

        private ChartBuilder chartBuilder = null;

        private DataSource dataSource = null;

        private OS os = OS.AIX;

        #endregion Fields


        #region EventHandlers

        private void ChartView_Load(object sender, EventArgs e)
        {
            comboBoxChartType.SelectedIndex = 0;
        }

        private void trackBarStart_ValueChanged(object sender, EventArgs e)
        {
            if (trackBarStart.Value > trackBarEnd.Value)
            {
                trackBarStart.Value = trackBarEnd.Value;
            }
        }

        private void trackBarEnd_ValueChanged(object sender, EventArgs e)
        {
            if (trackBarEnd.Value < trackBarStart.Value)
            {
                trackBarEnd.Value = trackBarStart.Value;
            }
        }

        private void trackBarStart_MouseCaptureChanged(object sender, EventArgs e)
        {
            UpdateStartTime();
            UpdateData();
        }

        private void trackBarEnd_MouseCaptureChanged(object sender, EventArgs e)
        {
            UpdateEndTime();
            UpdateData();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            UpdateStartIndex();
            UpdateEndIndex();
            UpdateData();
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCharts();
        }

        private void comboBoxChartType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxChartType.SelectedIndex)
            {
                case 0:
                    foreach (Series series in chartCPU.Series)
                    {
                        series.ChartType = SeriesChartType.StackedArea;
                    }
                    break;

                case 1:
                    foreach (Series series in chartCPU.Series)
                    {
                        series.ChartType = SeriesChartType.Area;
                    }
                    break;

                case 2:
                    foreach (Series series in chartCPU.Series)
                    {
                        series.ChartType = SeriesChartType.Line;
                    }
                    break;

                default:
                    foreach (Series series in chartCPU.Series)
                    {
                        series.ChartType = SeriesChartType.StackedArea;
                    }
                    break;
            }
        }

        private void Overview_CheckedChanged(object sender, EventArgs e)
        {
            splitContainerOverviewUpper.Panel1Collapsed = !checkBoxOverviewCPU.Checked;
            splitContainerOverviewUpper.Panel2Collapsed = !checkBoxOverviewMemory.Checked;
            splitContainerOverviewLower.Panel1Collapsed = !checkBoxOverviewThreads.Checked;
            splitContainerOverviewLower.Panel2Collapsed = !checkBoxOverviewFaults.Checked;
            splitContainerOverviewOuter.Panel1Collapsed = !(checkBoxOverviewCPU.Checked || checkBoxOverviewMemory.Checked);
            splitContainerOverviewOuter.Panel2Collapsed = !(checkBoxOverviewThreads.Checked || checkBoxOverviewFaults.Checked);
            splitContainerOverviewOuter.Visible = checkBoxOverviewCPU.Checked || checkBoxOverviewMemory.Checked || checkBoxOverviewThreads.Checked || checkBoxOverviewFaults.Checked;
        }

        private void CPU_CheckedChanged(object sender, EventArgs e)
        {
            splitContainerCPUOuter.Panel1Collapsed = !checkBoxCPU.Checked;
            splitContainerCPUOuter.Panel2Collapsed = !checkBoxCPUTotal.Checked;
            splitContainerCPUOuter.Visible = checkBoxCPU.Checked || checkBoxCPUTotal.Checked;
        }

        private void Memory_CheckedChanged(object sender, EventArgs e)
        {
            splitContainerMemoryOuter.Panel1Collapsed = !checkBoxAvm.Checked;
            splitContainerMemoryOuter.Panel2Collapsed = !checkBoxFree.Checked;
            splitContainerMemoryOuter.Visible = checkBoxAvm.Checked || checkBoxFree.Checked;
        }

        private void Threads_CheckedChanged(object sender, EventArgs e)
        {
            splitContainerThreadsOuter.Panel1Collapsed = !checkBoxThreadsR.Checked;
            splitContainerThreadsOuter.Panel2Collapsed = !checkBoxThreadsB.Checked;
            splitContainerThreadsOuter.Visible = checkBoxThreadsR.Checked || checkBoxThreadsB.Checked;
        }

        private void Faults_CheckedChanged(object sender, EventArgs e)
        {
            splitContainerFaultsOuter.Panel1Collapsed = !checkBoxInterrupt.Checked;
            splitContainerFaultsOuter.Panel2Collapsed = !checkBoxContextSwitches.Checked;
            splitContainerFaultsOuter.Visible = checkBoxInterrupt.Checked || checkBoxContextSwitches.Checked;
        }

        private void Page_CheckedChanged(object sender, EventArgs e)
        {
            splitContainerPageOuter.Panel1Collapsed = !checkBoxPageIn.Checked;
            splitContainerPageOuter.Panel2Collapsed = !checkBoxPageOut.Checked;
            splitContainerPageOuter.Visible = checkBoxPageIn.Checked || checkBoxPageOut.Checked;
        }

        #endregion EventHandlers


        #region Methods

        public void SetData(string path, OS os)
        {
            this.os = os;
            InitializeCharts();

            dataSource.LoadDataAsync(path, os);
        }

        private void UpdateData()
        {           
            dataSource.SelectByIndexAsync(trackBarStart.Value, trackBarEnd.Value);
        }

        private void InitializeCharts()
        {
            chartBuilder.OverviewCPUChart = chartCPUOverview;
            chartBuilder.OverviewMemoryChart = chartMemoryOverview;
            chartBuilder.OverviewThreadsChart = chartThreadsOverview;
            chartBuilder.OverviewFaultsChart = chartFaultsOverview;

            chartBuilder.CPUChart = chartCPU;
            chartBuilder.CPUTotalChart = chartCPUTotal;

            chartBuilder.MemoryAvmChart = chartMemoryAvm;
            chartBuilder.MemoryFreeChart = chartMemoryFree;

            chartBuilder.ThreadsRChart = chartThreadsR;
            chartBuilder.ThreadsBChart = chartThreadsB;

            chartBuilder.InterruptChart = chartInterrupt;
            chartBuilder.ContextSwitchesChart = chartContextSwitches;

            chartBuilder.PageIOChart = chartPageIn;
            chartBuilder.PageChart = chartPageOut;

            chartBuilder.Build(os);

            chartBuilder = null;
        }

        private void InitTrackBars(int count)
        {
            trackBarStart.Minimum = 0;
            trackBarStart.Maximum = count - 1;
            trackBarStart.TickFrequency = (int)(count * 0.1);

            trackBarEnd.Minimum = 0;
            trackBarEnd.Maximum = count - 1;
            trackBarEnd.TickFrequency = (int)(count * 0.1);

            trackBarStart.Value = trackBarStart.Minimum;
            trackBarEnd.Value = trackBarEnd.Maximum;
        }

        private void UpdateStartIndex()
        {
            trackBarStart.Value = dataSource.QueryStartIndexByTime(dateTimePickerStart.Value);
            UpdateStartTime();
        }

        private void UpdateEndIndex()
        {
            trackBarEnd.Value = dataSource.QueryEndIndexByTime(dateTimePickerEnd.Value);
            UpdateEndTime();
        }

        private void UpdateStartTime()
        {
            dateTimePickerStart.Value = dataSource.QueryTimeByIndex(trackBarStart.Value);
        }

        private void UpdateEndTime()
        {
            dateTimePickerEnd.Value = dataSource.QueryTimeByIndex(trackBarEnd.Value);
        }

        private void UpdateCPUOverviewStatistics()
        {
            string columnName = "cpu";

            lblCPUMinOverview.Text = string.Format("{0:N0}", dataSource.Min(columnName));
            lblCPUMaxOverview.Text = string.Format("{0:N0}", dataSource.Max(columnName));
            lblCPUAvgOverview.Text = string.Format("{0:0.00}", dataSource.Average(columnName));
        }

        private void UpdateMemoryOverviewStatistics()
        {
            string columnName = "memory_free";

            if (this.os == OS.AIX)
            {
                columnName = "memory_fre";
            }

            lblMemoryMinOverview.Text = string.Format("{0:N0}", dataSource.Min(columnName));
            lblMemoryMaxOverview.Text = string.Format("{0:N0}", dataSource.Max(columnName));
            lblMemoryAvgOverview.Text = string.Format("{0:N}", dataSource.Average(columnName));
        }

        private void UpdateThreadsROverviewStatistics()
        {
            string columnName = "procs_r";

            if (this.os == OS.AIX)
            {
                columnName = "kthr_r";
            }

            lblThreadROverviewMin.Text = string.Format("{0:N0}", dataSource.Min(columnName));
            lblThreadROverviewMax.Text = string.Format("{0:N0}", dataSource.Max(columnName));
            lblThreadROverviewAvg.Text = string.Format("{0:N}", dataSource.Average(columnName));
        }

        private void UpdateContextSwitchesOverviewStatistics()
        {
            string columnName = "faults_cs";

            if (this.os == OS.Linux)
            {
                columnName = "system_cs";
            }

            lblFaultsCSOverviewMin.Text = string.Format("{0:N0}", dataSource.Min(columnName));
            lblFaultsCSOverviewMax.Text = string.Format("{0:N0}", dataSource.Max(columnName));
            lblFaultsCSOverviewAvg.Text = string.Format("{0:N}", dataSource.Average(columnName));
        }

        private void UpdateCPUStatistics()
        {
            string columnName = "cpu";

            lblCPUMin.Text = string.Format("{0:N0}", dataSource.Min(columnName));
            lblCPUMax.Text = string.Format("{0:N0}", dataSource.Max(columnName));
            lblCPUAvg.Text = string.Format("{0:0.00}", dataSource.Average(columnName));
        }

        private void UpdateFreeMemoryStatistics()
        {
            string columnName = "memory_free";

            if (this.os == OS.AIX)
            {
                columnName = "memory_fre";
            }

            lblFreeMemoryMin.Text = string.Format("{0:N0}", dataSource.Min(columnName));
            lblFreeMemoryMax.Text = string.Format("{0:N0}", dataSource.Max(columnName));
            lblFreeMemoryAvg.Text = string.Format("{0:N}", dataSource.Average(columnName));
        }

        private void UpdateAvmStatistics()
        {
            string columnName = "memory_avm";

            if (this.os == OS.Linux)
            {
                columnName = "memory_swpd";
            }
            else if (os == OS.Solaris)
            {
                columnName = "memory_swap";
            }

            lblAvmMin.Text = string.Format("{0:N0}", dataSource.Min(columnName));
            lblAvmMax.Text = string.Format("{0:N0}", dataSource.Max(columnName));
            lblAvmAvg.Text = string.Format("{0:N}", dataSource.Average(columnName));
        }

        private void UpdateThreadsRStatistics()
        {
            string columnName = "procs_r";

            if (this.os == OS.AIX)
            {
                columnName = "kthr_r";
            }

            lblThreadsRMin.Text = string.Format("{0:N0}", dataSource.Min(columnName));
            lblThreadsRMax.Text = string.Format("{0:N0}", dataSource.Max(columnName));
            lblThreadsRAvg.Text = string.Format("{0:N}", dataSource.Average(columnName));
        }

        private void UpdateThreadsBStatistics()
        {
            string columnName = "procs_b";

            if (os == OS.AIX)
            {
                columnName = "kthr_b";
            }

            lblThreadsBMin.Text = string.Format("{0:N0}", dataSource.Min(columnName));
            lblThreadsBMax.Text = string.Format("{0:N0}", dataSource.Max(columnName));
            lblThreadsBAvg.Text = string.Format("{0:N}", dataSource.Average(columnName));
        }

        private void UpdateInterruptStatistics()
        {
            string columnName = "faults_in";

            if (os == OS.Linux)
            {
                columnName = "system_in";
            }

            lblInterruptMin.Text = string.Format("{0:N0}", dataSource.Min(columnName));
            lblInterruptMax.Text = string.Format("{0:N0}", dataSource.Max(columnName));
            lblInterruptAvg.Text = string.Format("{0:N}", dataSource.Average(columnName));
        }

        private void UpdateContextSwitchesStatistics()
        {
            string columnName = "faults_cs";

            if (os == OS.Linux)
            {
                columnName = "system_cs";
            }

            lblCSMin.Text = string.Format("{0:N0}", dataSource.Min(columnName));
            lblCSMax.Text = string.Format("{0:N0}", dataSource.Max(columnName));
            lblCSAvg.Text = string.Format("{0:N}", dataSource.Average(columnName));
        }

        private void UpdatePageInStatistics()
        {
            string columnName = "page_pi";

            if (os == OS.Linux)
            {
                columnName = "swap_si";
            }

            lblPageInMin.Text = string.Format("{0:N0}", dataSource.Min(columnName));
            lblPageInMax.Text = string.Format("{0:N0}", dataSource.Max(columnName));
            lblPageInAvg.Text = string.Format("{0:N}", dataSource.Average(columnName));
        }

        private void UpdatePageOutStatistics()
        {
            string columnName = "page_po";

            if (os == OS.Linux)
            {
                columnName = "swap_so";
            }

            lblPageSRMin.Text = string.Format("{0:N0}", dataSource.Min(columnName));
            lblPageSRMax.Text = string.Format("{0:N0}", dataSource.Max(columnName));
            lblPageSRAvg.Text = string.Format("{0:N}", dataSource.Average(columnName));
        }

        private void UpdateCharts()
        {
            switch (tabControl.SelectedIndex)
            {
                case 0:
                    chartCPUOverview.DataSource = bindingSource;
                    chartMemoryOverview.DataSource = bindingSource;
                    chartThreadsOverview.DataSource = bindingSource;
                    chartFaultsOverview.DataSource = bindingSource;

                    chartCPUOverview.DataBind();
                    chartMemoryOverview.DataBind();
                    chartThreadsOverview.DataBind();
                    chartFaultsOverview.DataBind();

                    UpdateCPUOverviewStatistics();
                    UpdateMemoryOverviewStatistics();
                    UpdateThreadsROverviewStatistics();
                    UpdateContextSwitchesOverviewStatistics();
                    break;

                case 1:
                    chartCPU.DataSource = bindingSource;
                    chartCPUTotal.DataSource = bindingSource;

                    chartCPU.DataBind();
                    chartCPUTotal.DataBind();
                    
                    UpdateCPUStatistics();
                    break;

                case 2:
                    chartMemoryAvm.DataSource = bindingSource;
                    chartMemoryFree.DataSource = bindingSource;

                    chartMemoryAvm.DataBind();
                    chartMemoryFree.DataBind();

                    UpdateFreeMemoryStatistics();
                    UpdateAvmStatistics();
                    break;

                case 3:
                    chartThreadsR.DataSource = bindingSource;
                    chartThreadsB.DataSource = bindingSource;

                    chartThreadsR.DataBind();
                    chartThreadsB.DataBind();

                    UpdateThreadsRStatistics();
                    UpdateThreadsBStatistics();
                    break;

                case 4:
                    chartInterrupt.DataSource = bindingSource;
                    chartContextSwitches.DataSource = bindingSource;

                    chartInterrupt.DataBind();
                    chartContextSwitches.DataBind();

                    UpdateInterruptStatistics();
                    UpdateContextSwitchesStatistics();
                    break;

                case 5:
                    chartPageIn.DataSource = bindingSource;
                    chartPageOut.DataSource = bindingSource;

                    chartPageIn.DataBind();
                    chartPageOut.DataBind();

                    UpdatePageInStatistics();
                    UpdatePageOutStatistics();
                    break;

                case 6:
                    UpdateRawData();
                    break;

                default:
                    break;
            }
        }

        private void UpdateRawData()
        {
            bindingSourceRawData.DataSource = dataSource.GetBindingTableView();
            dataGrid.DataSource = bindingSourceRawData;
        }

        #endregion Methods

        private void toolStripMenuItemCopyToClipboard_Click(object sender, EventArgs e)
        {
            Chart chart = (Chart)contextMenuStrip.SourceControl;

            ImageBuilder imageBuilder = new ImageBuilder();
            imageBuilder.CopyChartToClipboard(chart);
        }

        private void toolStripMenuItemExportImage_Click(object sender, EventArgs e)
        {
            Chart chart = (Chart)contextMenuStrip.SourceControl;

            ImageBuilder imageBuilder = new ImageBuilder();
            imageBuilder.ExportChart(chart);
        }

        private void toolStripMenuItemCopyToClipboardRaw_Click(object sender, EventArgs e)
        {
            
        }

        private void toolStripMenuItemExportRaw_Click(object sender, EventArgs e)
        {
            
        }

        private void SetChartVisible(bool visible)
        {
            switch (tabControl.SelectedIndex)
            {
                case 0:
                    chartCPUOverview.Visible = visible;
                    chartMemoryOverview.Visible = visible;
                    chartThreadsOverview.Visible = visible;
                    chartFaultsOverview.Visible = visible;
                    break;

                case 1:
                    chartCPU.Visible = visible;
                    chartCPUTotal.Visible = visible;
                    break;

                case 2:
                    chartMemoryAvm.Visible = visible;
                    chartMemoryFree.Visible = visible;
                    break;

                case 3:
                    chartThreadsR.Visible = visible;
                    chartThreadsB.Visible = visible;
                    break;

                case 4:
                    chartInterrupt.Visible = visible;
                    chartContextSwitches.Visible = visible;
                    break;

                case 5:
                    chartPageIn.Visible = visible;
                    chartPageOut.Visible = visible;
                    break;

                case 6:
                    break;

                default:
                    break;
            }
        }
    }
}