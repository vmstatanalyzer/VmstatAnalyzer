using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VmstatAnalyzer.Core;
using System.Data;
using System.Diagnostics;

namespace VmstatAnalyzerTest.Core
{
    [TestClass]
    public class VmstatFileReaderTest
    {
        private const string FILENAME = "./Data/aix-sample.log";

        private VmstatFileReader vmstatFileReader = new VmstatFileReader();

        [TestMethod]
        public void TestReadFile()
        {
            DataTable table = vmstatFileReader.ReadFile(FILENAME);
            
            foreach (DataRow row in table.Rows)
            {
                Debug.Print(row[0].ToString());
            }
        }
    }
}
