using System;
using System.IO;
using System.Net;
using System.Xml.Linq;
using AbitExportProject;
using Fdalilib;
using Fdalilib.Service;
using Fdalilib.Actions2015;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace fdalilibtests
{
    [TestClass]
    public class FDalilibTests
    {
        [TestMethod]
        public void TestGetDictionaries()
        {
            //var federalDatabase = CreateIntegrationService();
            //var result = federalDatabase.GetDictionaries();
            //foreach (DictionaryData item in result.Result.Items)
            //{
            //    Console.WriteLine(@"{0} - {1}", item.Code, item.Name);
            //}
        }
        [TestMethod]
        public void TestGetDictionariesDetails()
        {
            //var federalDatabase = CreateIntegrationService();
            //var result = federalDatabase.GetDictionaryDetails(10);
            //foreach (DictionaryDataDictionaryItem item in result.Result.DictionaryItems)
            //{
            //    Console.WriteLine(@"{0} - {1}", item.ID, item.Name);
                
            //}
        }
        [TestMethod]
        public void TestGetDictionariesDetailsWithErrorAnswer()
        {
            //var federalDatabase = CreateIntegrationService();
            //var result = federalDatabase.GetDictionaryDetails(1000);
            //Assert.IsFalse(result.IsSucceded);
            //Assert.IsNotNull(result.Error);
        }

        [TestMethod]
        public void TestTwiceLoadDataWithSingleConnection()
        {
            //var federalDatabase = CreateIntegrationService();
            //var resultById1 = federalDatabase.GetDictionaryDetails(1);
            //var resultById2InSameConnection = federalDatabase.GetDictionaryDetails(2);

            ////reopen connection
            //federalDatabase = CreateIntegrationService();
            //var resultById2InOtherConnection = federalDatabase.GetDictionaryDetails(2);

            //bool is_equals = true;
            //for (int i = 0; i < resultById2InOtherConnection.Result.DictionaryItems.Length; i++)
            //{
            //    is_equals &= resultById2InOtherConnection.Result.DictionaryItems[i].Equals(resultById2InSameConnection.Result.DictionaryItems[i]);
            //}

        }

        [TestMethod]
        public void TestImportPackageResult()
        {
            //var federalDatabase = CreateIntegrationService();
            //federalDatabase.ImportPackResult(340649);
        }

        [TestMethod]
        public void TestAnswerCheckApp()
        {
            //var federalDatabase = CreateIntegrationService();
            //var result = federalDatabase.GetSingleCheckApp("5718", new DateTime(2013, 6, 26));
            
        }

        [TestMethod]
        public void TestAnswerCheckAppByUid()
        {
            //var federalDatabase = CreateIntegrationService();
            //var result = federalDatabase.GetCheckPack(263422);

        }


        [TestMethod]
        public void TestAnswerCheckAppParser()
        {
            var ans = TestRes.CheckSingleAppRes;
            var xAns = XElement.Parse(ans);
            //var result = FisProxy.Deserialize<AppSingleCheckResult, TError>(xAns);
            //Console.WriteLine(result.Result.EgeDocumentCheckResults);  //.InstitutionID
            //Console.WriteLine(result.Result.EgeDocumentCheckResults.Application.ApplicationNumber);
           
        }

        [TestMethod]
        public void TestEgeCheckResultParce()
        {
            var egeCheckResultArtificialAns = TestRes.EgeDocumentCheckResultRes;
            var xegeCheckResultArtificialAns = XElement.Parse(egeCheckResultArtificialAns);
        //    var resultEgeCheck = FisProxy.Deserialize<EgeDocumentCheckResult, TError>(xegeCheckResultArtificialAns);
        //    Assert.AreEqual("5718", resultEgeCheck.Result.Application.ApplicationNumber);
            
        }

        [TestMethod]
        public void TestGetUniversityInfo()
        {
            //var federalDatabase = CreateIntegrationService();
            //var result = federalDatabase.GetUniversityInfo();
            //Console.WriteLine(result.Result.ToString());
        }

        [TestMethod]
        public void TestUnivercityInfoParce() 
        {
            var uniInfoAns = TestRes.InstitutionInfoRes;
            var campainDate = TestRes.CampainDate;
            var basicDiploma = TestRes.BasicDiploma;
            var regDate = TestRes.RegistrationDate;

            var xuniInfoAns = XElement.Parse(uniInfoAns);
            //var xCampainDate = XElement.Parse(campainDate);
            //var xBasicdiploma = XElement.Parse(basicDiploma);
            //var xRegDate = XElement.Parse(regDate);
            
            
            /*foreach (var elem in xuniInfoAns.Elements("DocumentDate"))
            {
                elem.SetValue(DateTime.Now);
            }

            foreach (var elem in xuniInfoAns.Elements("RegistrationDate"))
            {
                elem.SetValue(DateTime.Now);
            }*/

            //var InfoAnsCheck = FisProxy.Deserialize<Fdalilib.ImportClasses.InstitutionExports, TError>(xuniInfoAns);

        }
       
        
        public void TestGetDictNStruct()
        {
            //var federalDatabase = CreateIntegrationService();
            //var dictionaries = federalDatabase.GetDictionaries();
           
               
            //    foreach (DictionaryData dict in dictionaries.Result.Items)
            //    {
            //        using (var sw = new StreamWriter("out/"+dict.Name+".xml", true))
            //        {
                       
            //            sw.WriteLine(string.Format("<dict id=\"{0}\" name=\"{1}\" >", dict.Code, dict.Name));
            //            try
            //            {
            //                federalDatabase = CreateIntegrationService();
            //                var currentDict = federalDatabase.GetDictionaryDetails(dict.Code);

            //                foreach (var dictDetail in currentDict.Result.DictionaryItems)
            //                {
            //                    if (dict.Code == 10 || dict.Code == 19)
            //                    {

            //                        sw.WriteLine(FisProxy.Serialize(dictDetail));
                                    
            //                    }
            //                    else
            //                    {
            //                        sw.WriteLine(string.Format("<dictItem id=\"{0}\">{1}</dictItem>",
            //                                               dictDetail.ID, dictDetail.Name));
            //                    }
                                
            //                }
            //            }
            //            catch (Exception e)
            //            {
            //                Console.WriteLine(e.Message);

            //            }
            //            sw.WriteLine(string.Format("</dict>", dict.Name));
            //        }
            //    }
            
        }


        private static FisProxy<Root, TError, ImportResultPackage> CreateIntegrationService()
        {
            WebRequest.DefaultWebProxy = new WebProxy("http://195.22.104.27:3128/", true);

            IFisProxyService service = new WebClientFisProxyService(new EnlargeYourTimeoutClient(600000), new Uri("http://10.0.3.1:8080/import/"));

            var federalDatabase = new FisProxy<Root, TError, ImportResultPackage>(/*"fmarakasov@ugtu.net","bylnMu4",*/service)
            {
                //LogWriter =
                //    File.CreateText("../../OutLogs/" + ((DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds) +
                //                    "log.txt")
            };
            return federalDatabase;
        }


        
    }
}
