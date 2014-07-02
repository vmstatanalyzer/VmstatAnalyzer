using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace VmstatAnalyzer.Domain
{
    public class DataContext
    {
        public DataContext()
        {
        }

        public string FileName { get; set; }

        public string OSName { get; set; }

        public OSTypes OSType { get; set; }


    }
}
