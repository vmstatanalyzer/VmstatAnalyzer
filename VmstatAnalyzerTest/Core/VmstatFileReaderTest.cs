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
        private const string FILENAME1 = "./Data/ourhome63_1228_vmstat.log";
        private const string FILENAME2 = "./Data/LGEGLTDSE2Q_1211_vmstat.log";

        private VmstatFileReader vmstatFileReader = new VmstatFileReader();

        [TestMethod]
        public void TestReadFile1()
        {
            DataTable table = vmstatFileReader.ReadFile(FILENAME1);
            
            foreach (DataRow row in table.Rows)
            {
                Debug.Print(row[0].ToString());
            }
        }

        [TestMethod]
        public void TestReadFile2()
        {
            vmstatFileReader.ReadFile(FILENAME2);
        }
    }
}
