using System;
using LinaConsole;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CalculatorTest
{
    [TestClass]
    public class CalculatorProof
    {
        [TestMethod]
        public void ConvertToFractionTest()
        {
            string expected = "3/10";

            string actual = DecimalToFraction.ConvertToFraction(0.3m);

            Assert.AreEqual(actual, expected);
        }


        [TestMethod]
        public void DepurateTrigonometriesTest()
        {
            string expected = "10 % * 32 %";

            string actual = Depurate.NonRepeatedOccurences("10%32%");

           // Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void LiteralDepurationTest()
        {
            string expected = "log 10 * log 10 * log 10";

            string actual = Depurate.NonRepeatedOccurences("log10log10log10");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void PlusSignDepurationTest()
        {
            string expected = "10 c 10";

            string actual = Depurate.NonRepeatedOccurences("10c10");

            Assert.AreEqual(expected, actual);
        }

    }
}
