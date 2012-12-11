using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace StringCalculator_Kata
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    [TestFixture]
    class StringCalculator_KataTests
    {
        private StringCalculator stringCalculator;

        [SetUp]
        public void Setup()
        {
            stringCalculator = new StringCalculator();
        }
        [Test]
        public void AddClassTakesStringReturnsInt()

        {
            
            var actual = stringCalculator.Add("1");

            Assert.IsInstanceOf<int>(actual);
        }

        [Test]
        public void when_input_is_blank_string_result_is_zero()
        {
            const string emptyString = "";
            
            var actual = stringCalculator.Add(emptyString);

            Assert.AreEqual(0, actual);

        }

        [Ignore]
        public void StringFormatWithNumberCanInsertNumberInQuotes()
        {
            string actual = string.Format("{0}", 1);
            Assert.AreEqual("1",actual);
        }

        [TestCase(1)]
        [TestCase(10)]
        [TestCase(0)]
        public void when_input_is_one_number_result_is_same_number(int number)
        {
            string anyNumber = string.Format("{0}",number);

            var actual = stringCalculator.Add(anyNumber);

            Assert.AreEqual(number, actual);
        }

        [TestCase("0,0", 0)]
        [TestCase("1,0", 1)]
        [TestCase("0,1", 1)]
        [TestCase("1,1", 2)]
        public void when_input_is_two_numbers_result_is_the_sum_of_both(string numbers, int expected)
        {
            var actual = stringCalculator.Add(numbers);

            Assert.AreEqual(expected, actual);
        }

        [TestCase("1,1", 2)]
        [TestCase("0,1,2", 3)]
        [TestCase("1,2,3,4", 10)]
        [TestCase("0,0,0,0", 0)]
        [TestCase("1,0,0,0,1", 2)]
        public void when_input_is_any_number_of_numbers_result_is_the_sum_of_all(string numbers, int expected)
        {
            var actual = stringCalculator.Add(numbers);

            Assert.AreEqual(expected, actual);
        }

        [TestCase("1\n1",1)]
        public void stringify_number_separated_by_new_line_add_sum_of_numbers(string numbers, int expected)
        {
            var actual = stringCalculator.Add(numbers);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetSeparatorIdentifiesCommaOrNewLine()
        {
            var comma = stringCalculator.GetSeParator("0,1");

            Assert.AreEqual(comma,',');
        }

        [Test]
        public void GetSeparatorIdentifiesNewLine()
        {
            var newLine = stringCalculator.GetSeParator("0\n1");

            Assert.AreEqual('\n',newLine);
        }


    }

    internal class StringCalculator 
    {
        public int Add(string stringNumbers)
        {
            if (stringNumbers == "")
            {
                return 0;
            }
            var separator = GetSeParator(stringNumbers);
            return stringNumbers.Split(separator)
                .Select(x => Int32.Parse(x))
                .Sum();
            
        }

        public char GetSeParator(string stringNumbers)
        {
            var comma = stringNumbers.Split(',');
            var newLine = stringNumbers.Contains("\n");
            if (comma.Count() > 0)
                return ',';

            if (newLine)
                return '\n';

            return '.';
        }
    }
}
