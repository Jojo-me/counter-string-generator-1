using System.Text.RegularExpressions;
using CounterStringGenerator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CounterStringGeneratorTests
{
    [TestClass]
    public class StringGeneratorTest
    {
        [TestMethod]
        public void Zero_length_generates_empty_string()
        {
            Assert.AreEqual("", new StringGenerator().Generate(-1, '_'));
            Assert.AreEqual("", new StringGenerator().Generate(0, '_'));
        }

        [TestMethod]
        public void Shortest_string_without_special_character()
        {
            Assert.AreEqual("2", new StringGenerator().Generate(1, '_'));
        }

        [TestMethod]
        public void First_string_with_special_character()
        {
            Assert.AreEqual("2_", new StringGenerator().Generate(2, '_'));
        }

        [TestMethod]
        public void First_change_of_number_length()
        {
            Assert.AreEqual("2_4_6_8_11_", new StringGenerator().Generate(11, '_'));
        }
    }

    static class CheckMethods
    {
        public static void CheckLastNumber(int length, string counterString, char specialChar = '_')
        {
            Regex pattern = new Regex(@"(?<lastNumber>\d+)" + specialChar + @"(?<rest>\d*)$");
            Match match = pattern.Match(counterString);
            int lastUnderscore = int.Parse(match.Groups["lastNumber"].Value);
            string rest = match.Groups["rest"].Value;
            int count = lastUnderscore + rest.Length;

            Assert.AreEqual(length, count);
        }
    }

    [TestClass]
    public class MultiTests
    {
        [TestMethod]
        public void Test10And20_ByLength()
        {
            StringGenerator generator = new StringGenerator();
            Assert.AreEqual(10, generator.Generate(10, '_').Length);
            Assert.AreEqual(20, generator.Generate(20, '_').Length);
        }

        [TestMethod]
        public void Test10And20_ByLastNumber()
        {
            StringGenerator generator = new StringGenerator();
            CheckMethods.CheckLastNumber(10, generator.Generate(10, '_'));
            CheckMethods.CheckLastNumber(20, generator.Generate(20, '_'));
        }

        [TestMethod]
        public void SameSpecialChars()
        {
            StringGenerator generator = new StringGenerator();
            Assert.AreEqual("2_", generator.Generate(2, '_'));
            Assert.AreEqual("2_4_", generator.Generate(4, '_'));
        }

        [TestMethod]
        public void DifferentSpecialChars()
        {
            StringGenerator generator = new StringGenerator();
            Assert.AreEqual("2_", generator.Generate(2, '_'));
            Assert.AreEqual("2-4-", generator.Generate(4, '-'));
        }
    }
}
