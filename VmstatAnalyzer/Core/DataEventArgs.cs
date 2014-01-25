using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VmstatAnalyzer.Core
{
    public class DataEventArgs : EventArgs
    {
        public DataEventArgs(object data)
        {
            this.Data = data;
        }

        public DataEventArgs()
        {
        }

        public object Data { get; set; }
    }
}
