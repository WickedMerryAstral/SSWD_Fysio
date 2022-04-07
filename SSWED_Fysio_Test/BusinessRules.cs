using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SSWED_Fysio_Test
{
    [TestClass]
    public class BusinessRules
    {
        [TestMethod]
        public void HelloWorldTest()
        {
            string test = "Hello World";

            Assert.AreEqual("Hello World", test);
        }

    }
}