using SCRUMProjectManagementSystem;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;
using ViewModel;

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

        /// <summary>
        ///A test for Convert
        ///</summary>
        [TestMethod()]
        public void ConvertTest()
        {
            TaskStateConverter target = new TaskStateConverter(); // TODO: Initialize to an appropriate value
            TaskState value = TaskState.Unassigned;
            Type targetType = typeof(string);
            object paramter = null;
            CultureInfo culture = null;
            string expected = "Unassigned";
            string actual = target.Convert(value, targetType, paramter, culture) as string;
            Assert.AreEqual(expected, actual);

            value = TaskState.In_Progress;
            expected = "In Progress";
            actual = target.Convert(value, targetType, paramter, culture) as string;
            Assert.AreEqual(expected, actual);

            value = TaskState.Completed;
            expected = "Completed";
            actual = target.Convert(value, targetType, paramter, culture) as string;
            Assert.AreEqual(expected, actual);

            value = TaskState.Blocked;
            expected = "Blocked";
            actual = target.Convert(value, targetType, paramter, culture) as string;
            Assert.AreEqual(expected, actual);

            value = (TaskState)3;
            expected = "None";
            actual = target.Convert(value, targetType, paramter, culture) as string;
            Assert.AreEqual(expected, actual);

            object nullVal = null;
            expected = string.Empty;
            actual = target.Convert(nullVal, targetType, paramter, culture) as string;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ConvertBack
        ///</summary>
        [TestMethod()]
        public void ConvertBackTest()
        {
            TaskStateConverter target = new TaskStateConverter(); // TODO: Initialize to an appropriate value
            string value = "Unassigned";
            Type targetType = typeof(TaskState);
            object paramter = null;
            CultureInfo culture = null;
            TaskState expected = TaskState.Unassigned;
            TaskState actual = (TaskState)target.ConvertBack(value, targetType, paramter, culture);
            Assert.AreEqual(expected, actual);

            value = "In Progress";
            expected = TaskState.In_Progress;
            actual = (TaskState)target.ConvertBack(value, targetType, paramter, culture);
            Assert.AreEqual(expected, actual);

            value = "Completed";
            expected = TaskState.Completed;
            actual = (TaskState)target.ConvertBack(value, targetType, paramter, culture);
            Assert.AreEqual(expected, actual);

            value = "Blocked";
            expected = TaskState.Blocked;
            actual = (TaskState)target.ConvertBack(value, targetType, paramter, culture);
            Assert.AreEqual(expected, actual);

            value = "Invalid";
            expected = TaskState.Unassigned;
            actual = (TaskState)target.ConvertBack(value, targetType, paramter, culture);
            Assert.AreEqual(expected, actual);

            object nullVal = null;
            expected = TaskState.Unassigned;
            actual = (TaskState)target.ConvertBack(nullVal, targetType, paramter, culture);
            Assert.AreEqual(expected, actual);
        }
    }
}
