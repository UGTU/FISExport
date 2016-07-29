using System;
using Fdalilib;
//using Fdalilib.Actions2015;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace fdalilibtests
{
    [TestClass]
    public class AnswerTests
    {
        [TestMethod]
        public void TestWithExpectedResult()
        {
            var expected = new AnswerTests();
            var answer = new Answer<AnswerTests, TError>(expected);
            Assert.IsTrue(answer.IsSucceded);
            Assert.IsFalse(answer.IsUnexpectedResult);
            Assert.AreEqual(expected, answer.Result);
            Assert.IsNull(answer.Error);
        }

        [TestMethod]
        public void TestWithUnExpectedResult()
        {
            var expected = new DateTime();
            var answer = new Answer<AnswerTests, TError>(expected);
            Assert.IsFalse(answer.IsSucceded);
            Assert.IsTrue(answer.IsUnexpectedResult);
            Assert.IsNull(answer.Result);
            Assert.IsNull(answer.Error);
        }

        [TestMethod]
        public void TestWithErrorResult()
        {
            var expected = new TError();
            var answer = new Answer<AnswerTests, TError>(expected);
            Assert.IsFalse(answer.IsSucceded);
            Assert.IsFalse(answer.IsUnexpectedResult);
            Assert.AreEqual(expected, answer.Error);
            Assert.IsNull(answer.Result);
        }
    }
}
