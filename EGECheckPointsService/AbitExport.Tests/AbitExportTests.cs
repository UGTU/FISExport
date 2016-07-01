using System;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics;
using AbitExportProject;
using AbitExportProject.ActionMethods;
using AbitExportProject.Controllers;
using AbitExportProject.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace AbitExport.Tests
{
    [TestClass]
    public class AbitExportTests
    {
        [TestMethod]
        public void TestBatchParseArgs()
        {
         //   Assert.IsTrue(BaseProxyMethod<Root>.ParseArgument("/P").ExportType == ExportType.Batch);            

        }

        [TestMethod]
        public void TestSingleParseArgs()
        {
            //var actual = BaseMethod.ParseArgument("/id:44986");
            //Assert.AreEqual(ExportType.Single, actual.ExportType);
            //Assert.AreEqual(44986, actual.AbitId);
        }

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
            //var bc = new BaseMethod();
            //bc.ExportSingle(44986, DateTime.Today.Year);
        }

        [TestMethod]
        public void TestImportBatch()
        {
         //   Program.ExportSingle(new Program.ExportParam(Program.ExportType.Batch, 0), DateTime.Today.Year);
        }

        [TestMethod]
        public void GetDictionaryFromXml()
        {
            var method = new GetDictionaryMethod();
            var result = method.ReadDataFromFile("dict.xml");
            Console.WriteLine("Операция выполнена. Нажмите [ENTER]...");
            Console.ReadLine();
        }

    }
}
