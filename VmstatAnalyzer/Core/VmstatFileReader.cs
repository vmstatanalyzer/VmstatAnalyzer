using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace VmstatAnalyzer.Core
{
    /// <summary>
    /// VMSTAT log file reader.
    /// </summary>
    public class VmstatFileReader
    {
        /// <summary>
        /// VMSTAT log line parser.
        /// </summary>
        private VmstatLineParser vmstatLineParser = new VmstatLineParser();

        /// <summary>
        /// Read VMSTAT log file.
        /// </summary>
        /// <param name="fileName">VMSTAT log file name</param>
        /// <returns>Data Table</returns>
        public DataTable ReadFile(string fileName)
        {
            DataTable table = new DataTable();

            bool firstLine = true;
            int columnCount = 0;

            string line = null;
            string[] values = null;

            using (TextReader reader = File.OpenText(fileName))
            {
                table.BeginLoadData();

                while ((line = reader.ReadLine()) != null)
                {
                    values = vmstatLineParser.ParseLine(line);

                    if (firstLine)
                    {
                        columnCount = values.Length;

                        for (int i = 0; i < columnCount; i++)
                        {
                            table.Columns.Add();
                        }

                        firstLine = false;
                    }

                    if (values.Length == columnCount)
                    {
                        table.Rows.Add(values);
                    }
                }

                table.EndLoadData();
            }

            return table;
        }
    }
}
