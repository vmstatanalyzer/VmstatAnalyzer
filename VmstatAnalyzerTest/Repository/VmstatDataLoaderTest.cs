using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VmstatAnalyzer.Repository;
using System.Data;

namespace VmstatAnalyzerTest.Repository
{
    [TestClass]
    public class VmstatDataLoaderTest
    {
        const string FILENAME = "./Data/ourhome63_1228_vmstat.log";
        //const string FILENAME = "./Data/LGEGLTDSE2Q_1211_vmstat.log";

        [TestMethod]
        public void TestLoadData()
        {
            VmstatDataLoader dataLoader = new VmstatDataLoader();
            DataTable table = dataLoader.LoadData(FILENAME);

            Assert.IsNotNull(table, "DataTable 객체가 Null이다.");
            Assert.IsTrue(table.Columns.Count > 0, "컬럼이 존재하지 않는다.");
            Assert.IsTrue(table.Rows.Count > 0, "데이터가 존재하지 않는다.");
        }
    }
}
