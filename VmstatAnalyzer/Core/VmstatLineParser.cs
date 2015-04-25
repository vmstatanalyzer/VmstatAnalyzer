using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace VmstatAnalyzer.Core
{
    /// <summary>
    /// VMSTAT log line parser.
    /// </summary>
    public class VmstatLineParser
    {
        /// <summary>
        /// Delimiter for VMSTAT column separation.
        /// </summary>
        private const string DELIMITER = " ";

        /// <summary>
        /// Separator for splitting VMSTAT row string.
        /// </summary>
        private string[] separator = new string[] { DELIMITER };

        /// <summary>
        /// Regex for space, tab characters.
        /// </summary>
        private Regex regex = new Regex(" +|\t+");

        /// <summary>
        /// Format string for space, tab characters.
        /// </summary>
        /// <param name="line">VMSTAT line string to be formatted</param>
        /// <returns>Formatted string</returns>
        public string FormatLine(string line)
        {
            return regex.Replace(line, DELIMITER);
        }

        /// <summary>
        /// Split VMSTAT line string.
        /// </summary>
        /// <param name="line">VMSTAT formatted line string</param>
        /// <returns>Values for VMSTAT columns</returns>
        public string[] GetValues(string line)
        {
            return line.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Return VMSTAT values for a line.
        /// It performs FormatLine and GetValues.
        /// </summary>
        /// <param name="line">VMSTAT line string</param>
        /// <returns>Values for VMSTAT columns</returns>
        public string[] ParseLine(string line)
        {
            return GetValues(FormatLine(line));
        }
    }
}
