using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VmstatAnalyzer.Core;

namespace VmstatAnalyzerTest.Core
{
    [TestClass]
    public class VmstatLineParserTest
    {
        private VmstatLineParser vmstatLineParser = new VmstatLineParser();

        [TestMethod]
        public void TestFormatLine()
        {
            string line = "  hello world   \thi   \t\t\t everyone      .";
            string formattedLine = vmstatLineParser.FormatLine(line);
            StringAssert.Equals(formattedLine, "hello world hi everyone.");
        }
    }
}
