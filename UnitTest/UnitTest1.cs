using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using productionltd;

namespace UnitTest {
    [TestClass]
    public class UnitTest1 {

        [TestMethod]
        public void ProcessTimeTest() {
            Product p = new Product("Test Rist", true, "30");
            OrderItem OI = new OrderItem(amount, p);
            OI.Save();
            int amount = 50;
            
             
        }
    }
}
