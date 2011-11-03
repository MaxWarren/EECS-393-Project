using System;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SCRUMProjectManagementSystem;
using ViewModel;

namespace UnitTests
{
    /// <summary>
    ///This is a test class for TaskTypeConverterTest and is intended
    ///to contain all TaskTypeConverterTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TaskTypeConverterTest
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

        /// <summary>
        ///A test for Convert
        ///</summary>
        [TestMethod()]
        public void ConvertTest()
        {
            TaskTypeConverter target = new TaskTypeConverter();
            TaskType value = TaskType.Development;
            Type targetType = typeof(string);
            object paramter = null;
            CultureInfo culture = null;
            string expected = "Development";
            string actual = target.Convert(value, targetType, paramter, culture) as string;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ConvertBack
        ///</summary>
        [TestMethod()]
        public void ConvertBackTest()
        {
            TaskTypeConverter target = new TaskTypeConverter(); // TODO: Initialize to an appropriate value
            string value = "Development";
            Type targetType = typeof(TaskType);
            object paramter = null;
            CultureInfo culture = null;
            TaskType expected = TaskType.Development;
            TaskType actual = (TaskType)target.ConvertBack(value, targetType, paramter, culture);
            Assert.AreEqual(expected, actual);
        }
    }
}
