using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LinaConsole;

namespace BasicTests
{
    [TestClass]
    class GeneralTest
    {
        [TestMethod]
        public void DecimalTiming()
        {
            
            string expected = "1/43";

            string actual = DecimalToFraction.ConvertToFraction(0.25m);

            Assert.AreEqual(actual, expected);

        }

       
    }
}
