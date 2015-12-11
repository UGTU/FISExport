using System;
using EGECheckPointsService;
using EGECheckUnitTestProject.FCTServiceReference;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EGECheckUnitTestProject
{
    [TestClass]
    public class EgeServiceTests
    {
        private readonly EgeCheckServiceClient _client = new EgeCheckServiceClient();
        
        
        [TestMethod]
        public void SimpleTestForSpecialPerson()
        {
            var result = _client.SingleCheck("Канева", "Анна", "Николаевна", "8709", "369529", null, null);
            Console.WriteLine(result);
            //Assert.AreEqual(Resource.VokuevaTamaraVasilievna, result);
        }
    }
}
