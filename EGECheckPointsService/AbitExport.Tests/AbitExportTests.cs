using System;
using System.Diagnostics;
using AbitExportProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AbitExport.Tests
{
    [TestClass]
    public class AbitExportTests
    {
        [TestMethod]
        public void TestBatchParseArgs()
        {
            Assert.IsTrue(Program.ParseArgument("/P").ExportType == Program.ExportType.Batch);            

        }

        [TestMethod]
        public void TestSingleParseArgs()
        {
            var actual = Program.ParseArgument("/id:44986");
            Assert.AreEqual(Program.ExportType.Single, actual.ExportType);
            Assert.AreEqual(44986, actual.AbitId);
        }

   /*     [TestMethod]
        public void TestSingleParseWithExtraSpaceArgs()
        {
            var actual = Program.ParseArgument(" /id:  20342  ");
            Assert.AreEqual(Program.ExportType.Single, actual.ExportType);
            Assert.AreEqual(20342, actual.AbitId);

        }*/

        [TestMethod]
        [DeploymentItem("export2fis.exe")]
        public void TestSingleImport()
        {
            var process = new System.Diagnostics.Process
                {
                    StartInfo = new ProcessStartInfo() {Arguments = "/id:44986", FileName = @"export2fis.exe"}
                };
            Assert.IsTrue(process.Start());
            process.WaitForExit();
        }

        [TestMethod]
        public void TestMainProgramForImportSingle()
        {
            Program.Main(new[]{"/id:44986"});
        }

        [TestMethod]
        public void TestMethodImportSingle()
        {
            Program.ExportSingle(new Program.ExportParam(Program.ExportType.Single, 44986),DateTime.Today.Year);
        }

        [TestMethod]
        public void TestImportBatch()
        {
            Program.ExportSingle(new Program.ExportParam(Program.ExportType.Batch, 0), DateTime.Today.Year);
        }

    }
}
