using SCRUMProjectManagementSystem;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;

namespace UnitTests
{
    
    
    /// <summary>
    ///This is a test class for TaskStateConverterTest and is intended
    ///to contain all TaskStateConverterTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TaskStateConverterTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for TaskStateConverter Constructor
        ///</summary>
        [TestMethod()]
        public void TaskStateConverterConstructorTest()
        {
            TaskStateConverter target = new TaskStateConverter();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for Convert
        ///</summary>
        [TestMethod()]
        public void ConvertTest()
        {
            TaskStateConverter target = new TaskStateConverter(); // TODO: Initialize to an appropriate value
            object value = null; // TODO: Initialize to an appropriate value
            Type targetType = null; // TODO: Initialize to an appropriate value
            object paramter = null; // TODO: Initialize to an appropriate value
            CultureInfo culture = null; // TODO: Initialize to an appropriate value
            object expected = null; // TODO: Initialize to an appropriate value
            object actual;
            actual = target.Convert(value, targetType, paramter, culture);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ConvertBack
        ///</summary>
        [TestMethod()]
        public void ConvertBackTest()
        {
            TaskStateConverter target = new TaskStateConverter(); // TODO: Initialize to an appropriate value
            object value = null; // TODO: Initialize to an appropriate value
            Type targetType = null; // TODO: Initialize to an appropriate value
            object paramter = null; // TODO: Initialize to an appropriate value
            CultureInfo culture = null; // TODO: Initialize to an appropriate value
            object expected = null; // TODO: Initialize to an appropriate value
            object actual;
            actual = target.ConvertBack(value, targetType, paramter, culture);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
