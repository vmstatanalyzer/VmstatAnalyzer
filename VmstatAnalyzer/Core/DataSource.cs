using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace VmstatAnalyzer.Core
{
    public class DataSource
    {
        public DataSource()
        {
            table = new DataTable();

            formatInfo = new DateTimeFormatInfo();
            formatInfo.LongTimePattern = "HH:mm:ss";
        }

        /*
        private const int COLS_COUNT_AIX = 18;

        private const int COLS_COUNT_BSD = 20;

        private const int COLS_COUNT_HPUX = 19;

        private const int COLS_COUNT_LINUX2541 = 17;

        private const int COLS_COUNT_LINUX = 18;

        private const int COLS_COUNT_SOLARIS = 23;
         * */


        public event EventHandler LoadDataStart;

        public event EventHandler<DataEventArgs> LoadDataComplete;

        public event EventHandler SelectDataStart;

        public event EventHandler<DataEventArgs> SelectDataComplete;


        private DateTimeFormatInfo formatInfo = null;

        private DataTable table = null;

        private DataView dataView = null;

        private string fileName;

        private OS os;

        private int maxPoints = 500;

        public int MaxPoints
        {
            get
            {
                return maxPoints;
            }

            set
            {
                maxPoints = value;
            }
        }

        private struct paramdata
        {
            public object lbound;
            public object rbound;
        }

        public void LoadDataAsync(string fileName, OS os)
        {
            this.fileName = fileName;
            this.os = os;

            Thread t = new Thread(new ThreadStart(DoLoadData));
            t.IsBackground = true;
            t.Start();
        }

        private void DoLoadData()
        {
            if (LoadDataStart != null)
            {
                LoadDataStart(this, new EventArgs());
            }

            int count = LoadData(fileName, os);

            if (LoadDataComplete != null)
            {
                LoadDataComplete(this, new DataEventArgs(count));
            }
        }

        public int LoadData(string fileName, OS os)
        {
            CreateColumns(os);

            Regex regex = new Regex(" +");

            string line = null;
            string temp = null;
            string[] values = null;

            int columnCount = table.Columns.Count;
            int len = columnCount - 2;
            table.BeginLoadData();

            using (TextReader reader = File.OpenText(fileName))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    temp = regex.Replace(line.Replace("\t", " "), " ");
                    values = temp.Split(' ');

                    if (values.Length == len)
                    {
                        DataRow row = table.NewRow();
                        for (int i = 0; i < len; i++)
                        {
                            row[i + 1] = values[i];
                        }

                        table.Rows.Add(row);
                    }
                }
            }

            table.EndLoadData();

            dataView = table.AsDataView();

            return table.Rows.Count;
        }

        public void SelectByIndexAsync(int startIndex, int endIndex)
        {
            paramdata pdata;
            pdata.lbound = startIndex;
            pdata.rbound = endIndex;

            Thread t = new Thread(new ParameterizedThreadStart(DoSelectByIndex));
            t.IsBackground = true;
            t.Start(pdata);
        }

        private void DoSelectByIndex(object data)
        {
            if (SelectDataStart != null)
            {
                SelectDataStart(this, new EventArgs());
            }

            paramdata pdata = (paramdata)data;

            DataView view = SelectByIndex(int.Parse(pdata.lbound.ToString()), int.Parse(pdata.rbound.ToString()));

            if (SelectDataComplete != null)
            {
                SelectDataComplete(this, new DataEventArgs(view));
            }
        }

        public void SelectByTimeAsync(int startIndex, int endIndex)
        {
            paramdata pdata;
            pdata.lbound = startIndex;
            pdata.rbound = endIndex;

            Thread t = new Thread(new ParameterizedThreadStart(DoSelectByTime));
            t.IsBackground = true;
            t.Start(pdata);
        }

        private void DoSelectByTime(object data)
        {
            if (SelectDataStart != null)
            {
                SelectDataStart(this, new EventArgs());
            }

            paramdata pdata = (paramdata)data;

            DataView view = SelectByTime(pdata.lbound.ToString(), pdata.rbound.ToString());

            if (SelectDataComplete != null)
            {
                SelectDataComplete(this, new DataEventArgs(view));
            }
        }

        public DataView SelectByIndex(int startIndex, int endIndex)
        {
            dataView.RowFilter = string.Format("index >= {0} AND index <= {1}", startIndex, endIndex);

            DataView view = dataView.ToTable().AsDataView();
            int interval = (view.Count / maxPoints) + 1;
            view.RowFilter = string.Format("index % {0} = 0", interval);

            return view;
        }

        public DataView SelectByTime(string startTime, string endTime)
        {
            dataView.RowFilter = string.Format("time >= {0} AND time <= {1}", startTime, endTime);

            DataView view = dataView.ToTable().AsDataView();
            int interval = (view.Count / maxPoints) + 1;
            view.RowFilter = string.Format("index % {0} = 0", interval);

            return view;
        }

        public DateTime QueryTimeByIndex(int index)
        {
            DataRow row = table.Rows.Find(index);
            return DateTime.Parse(row[1].ToString(), formatInfo);
        }

        public int QueryStartIndexByTime(DateTime time)
        {
            int index = 0;

            DataRow row = table.AsEnumerable().FirstOrDefault<DataRow>(e => DateTime.Parse(e[1].ToString()) >= time);
            if (row != null)
            {
                index = int.Parse(row[0].ToString());
            }

            return index;
        }

        public int QueryEndIndexByTime(DateTime time)
        {
            int index = table.Rows.Count - 1;

            DataRow row = table.AsEnumerable().LastOrDefault<DataRow>(e => DateTime.Parse(e[1].ToString()) <= time);
            if (row != null)
            {
                index = int.Parse(row[0].ToString());
            }

            return index;
        }

        public int Min(string columnName)
        {
            int value = 0;
            value = dataView.OfType<DataRowView>().Min(e => int.Parse(e[columnName].ToString()));

            return value;
        }

        public double Max(string columnName)
        {
            int value = 0;
            value = dataView.OfType<DataRowView>().Max(e => int.Parse(e[columnName].ToString()));

            return value;
        }

        public double Average(string columnName)
        {
            double value = 0.0;
            value = dataView.OfType<DataRowView>().Average(e => double.Parse(e[columnName].ToString()));

            return value;
        }

        public double TopAverage(string columnName, double top)
        {
            double value = 0.0;
            double percentile = (100 - top) * 0.01;
            

            return value;
        }

        private void CreateColumns(OS os)
        {
            DataColumn column = null;
            column = table.Columns.Add("index", typeof(int));
            column.AutoIncrement = true;
            column.AutoIncrementSeed = 0;
            column.AutoIncrementStep = 1;
            column.ReadOnly = true;

            table.PrimaryKey = new DataColumn[] { column };

            string[] columnNames = GetColumnNames(os);

            foreach (string columnName in columnNames)
            {
                Type type = typeof(int);
                if (columnName.Equals("time"))
                {
                    type = typeof(string);
                }

                table.Columns.Add(columnName, type);
            }

            column = table.Columns.Add("cpu", typeof(int));
            column.Expression = "100 - cpu_id";
        }

        public string[] GetColumnNames(OS os)
        {
            switch (os)
            {
                case OS.AIX:
                    return new string[] { "time", 
                        "kthr_r", "kthr_b", 
                        "memory_avm", "memory_fre", 
                        "page_re", "page_pi", "page_po", "page_fr", "page_sr", "page_cy", 
                        "faults_in", "faults_sy", "faults_cs", 
                        "cpu_us", "cpu_sy", "cpu_id", "cpu_wa" };

                case OS.BSD:
                    return new string[] { "time", 
                        "procs_r", "procs_b", "procs_w", 
                        "memory_avm", "memory_free", 
                        "page_flt", "page_re", "page_pi", "page_po", "page_fr", "page_sr", 
                        "disk_01", "disk_02", 
                        "faults_in", "faults_sy", "faults_cs", 
                        "cpu_us", "cpu_sy", "cpu_id" };

                case OS.HPUX:
                    return new string[] { "time", 
                        "procs_r", "procs_b", "procs_w", 
                        "memory_avm", "memory_free", 
                        "page_re", "page_at", "page_pi", "page_po", "page_fr", "page_de", "page_sr", 
                        "faults_in", "faults_sy", "faults_cs", 
                        "cpu_us", "cpu_sy", "cpu_id" };

                case OS.Linux2541:
                    return new string[] { "time", 
                        "procs_r", "procs_b",
                        "memory_swpd", "memory_free", "memory_buff", "memory_cache", 
                        "swap_si", "swap_so", 
                        "io_bi", "io_bo", 
                        "system_in", "system_cs", 
                        "cpu_us", "cpu_sy", "cpu_id", "cpu_wa" };

                case OS.Linux:
                    return new string[] { "time", 
                        "procs_r", "procs_b",
                        "memory_swpd", "memory_free", "memory_buff", "memory_cache", 
                        "swap_si", "swap_so", 
                        "io_bi", "io_bo", 
                        "system_in", "system_cs", 
                        "cpu_us", "cpu_sy", "cpu_id", "cpu_wa", "cpu_st" };

                case OS.Solaris:
                    return new string[] { "time", 
                        "procs_r", "procs_b", "procs_w", 
                        "memory_swap", "memory_free", 
                        "page_re", "page_mf", "page_pi", "page_po", "page_fr", "page_de", "page_sr", 
                        "disk_01", "disk_02", "disk_03", "disk_04", 
                        "faults_in", "faults_sy", "faults_cs", 
                        "cpu_us", "cpu_sy", "cpu_id" };

                default:
                    return new string[] { "time", 
                        "kthr_r", "kthr_b", 
                        "memory_avm", "memory_fre", 
                        "page_re", "page_pi", "page_po", "page_fr", "page_sr", "page_cy", 
                        "faults_in", "faults_sy", "faults_cs", 
                        "cpu_us", "cpu_sy", "cpu_id", "cpu_wa" };
            }
        }

        public string[] GetPageNames(OS os)
        {
            switch (os)
            {
                case OS.AIX:
                    return new string[] { "page_re", "page_pi", "page_po", "page_fr", "page_sr", "page_cy" };

                case OS.BSD:
                    return new string[] { "page_flt", "page_re", "page_pi", "page_po", "page_fr", "page_sr" };

                case OS.HPUX:
                    return new string[] { "page_re", "page_at", "page_pi", "page_po", "page_fr", "page_de", "page_sr" };

                case OS.Linux2541:
                case OS.Linux:
                    return new string[] { "swap_si", "swap_so" };

                case OS.Solaris:
                    return new string[] { "page_re", "page_mf", "page_pi", "page_po", "page_fr", "page_de", "page_sr" };

                default:
                    return new string[] { "page_re", "page_pi", "page_po", "page_fr", "page_sr", "page_cy" };
            }
        }

        public DataView GetBindingTableView()
        {
            return dataView.ToTable(false, GetColumnNames(this.os)).AsDataView();
        }

        public void Dispose()
        {
            dataView.Dispose();
            table.Dispose();
        }
    }
}
