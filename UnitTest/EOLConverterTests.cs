using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Manobit.CodeBeautifier.Sources;

namespace UnitTest
{
    [TestClass]
    public class EOLConverterTests
    {
        [TestMethod]
        public void CheckEOLChars()
        {
            CollectionAssert.AreEqual( new char[] { '\r', '\n' }, EOLConverter.eolChars( EOLConverter.EOLType.Windows ) );
            CollectionAssert.AreEqual( new char[] { '\n' }, EOLConverter.eolChars( EOLConverter.EOLType.Unix ) );
            CollectionAssert.AreEqual( new char[] { '\r' }, EOLConverter.eolChars( EOLConverter.EOLType.Mac ) );
        }

        [TestMethod]
        public void ReturnsEmptyStringForEmptyString()
        {
            Assert.AreEqual( "", EOLConverter.convert( "", EOLConverter.EOLType.Windows ) );
            Assert.AreEqual( "", EOLConverter.convert( "", EOLConverter.EOLType.Unix ) );
            Assert.AreEqual( "", EOLConverter.convert( "", EOLConverter.EOLType.Mac ) );
        }

        [TestMethod]
        public void ReturnsEmptyStringForNullString()
        {
            Assert.AreEqual( "", EOLConverter.convert( null, EOLConverter.EOLType.Windows ) );
            Assert.AreEqual( "", EOLConverter.convert( null, EOLConverter.EOLType.Unix ) );
            Assert.AreEqual( "", EOLConverter.convert( null, EOLConverter.EOLType.Mac ) );
        }

        static string forTest = "\r\nSecond\n Third\r  Fourth\r\n Without";

        [TestMethod]
        public void ConvertToWindowsEnding()
        {
            string expected = "\r\nSecond\r\n Third\r\n  Fourth\r\n Without";
            Assert.AreEqual( expected, EOLConverter.convert( forTest, EOLConverter.EOLType.Windows ) );
        }

        [TestMethod]
        public void ConvertToUnixEnding()
        {
            string expected = "\nSecond\n Third\n  Fourth\n Without";
            Assert.AreEqual( expected, EOLConverter.convert( forTest, EOLConverter.EOLType.Unix ) );
        }

        [TestMethod]
        public void ConvertToMacEnding()
        {
            string expected = "\rSecond\r Third\r  Fourth\r Without";
            Assert.AreEqual( expected, EOLConverter.convert( forTest, EOLConverter.EOLType.Mac ) );
        }
    }
}
