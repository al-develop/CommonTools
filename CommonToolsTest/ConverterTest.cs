using CommonToolsTest.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonTools.Converter;

namespace CommonToolsTest
{
    [TestClass]
    public class ConverterTest
    {
        [TestMethod]
        public void BoolToVisibilityTest()
        {

        }

        [TestMethod]
        public void EnumDescriptionToStringTest()
        {
            EnumDescriptionToStringConverter converter = new EnumDescriptionToStringConverter();

            object testResultOne = converter.Convert(EnumTest.EnumMemberOne, null, null, null);
            object testResultTwo = converter.Convert(EnumTest.EnumMemberTwo, null, null, null);
        }
    }
}