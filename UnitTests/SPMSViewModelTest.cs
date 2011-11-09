using ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using DatabaseLayer;

namespace UnitTests
{
    /// <summary>
    ///This is a test class for SPMSViewModelTest and is intended
    ///to contain all SPMSViewModelTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SPMSViewModelTest
    {
        private TestContext testContextInstance;
        private SPMSViewModel_Accessor target;

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

        [TestInitialize()]
        public void TestInitialize()
        {
            target = new SPMSViewModel_Accessor(new MockDataModel());
            target.AuthenticateUser(1, "password");
        }

        /// <summary>
        ///A test for updateTasksForUser
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ViewModel.dll")]
        public void updateTasksForUserTest()
        {
            bool expected = true;
            bool actual;
            actual = target.updateTasksForUser();
            Assert.AreEqual(expected, actual);

            target.HistoricMode = true;
            actual = target.updateTasksForUser();
            Assert.AreEqual(expected, actual);

            target._isLoggedIn = false;
            try
            {
                target.updateTasksForUser();
                Assert.Fail("Exception not thrown");
            }
            catch (InvalidOperationException)
            {
                ;
            }
        }

        /// <summary>
        ///A test for updateTasksForStory
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ViewModel.dll")]
        public void updateTasksForStoryTest()
        {
            bool expected = true;
            bool actual;
            try
            {
                target.updateTasksForStory();
                Assert.Fail("Exception not thrown");
            }
            catch (InvalidOperationException)
            {
                ;
            }

            target.JumpToTask(new TaskView(target._dataModel.GetTaskByID(1)));
            actual = target.updateTasksForStory();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for updateStoriesForSprint
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ViewModel.dll")]
        public void updateStoriesForSprintTest()
        {
            bool expected = true;
            bool actual;
            try
            {

                actual = target.updateStoriesForSprint();
                Assert.Fail("Exception not thrown");
            }
            catch (InvalidOperationException)
            {
                ;
            }

            target.JumpToTask(new TaskView(target._dataModel.GetTaskByID(1)));
            actual = target.updateTasksForStory();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for updateSprintsForProject
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ViewModel.dll")]
        public void updateSprintsForProjectTest()
        {
            bool expected = true;
            bool actual;
            try
            {
                actual = target.updateSprintsForProject();
                Assert.Fail("Exception not thrown");
            }
            catch (InvalidOperationException)
            {
                ;
            }

            target.JumpToTask(new TaskView(target._dataModel.GetTaskByID(1)));
            actual = target.updateSprintsForProject();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for updateProjectsForUser
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ViewModel.dll")]
        public void updateProjectsForUserTest()
        {
            bool expected = true;
            bool actual;
            target._isLoggedIn = false;
            try
            {
                actual = target.updateProjectsForUser();
                Assert.Fail("Exception not thrown");
            }
            catch (InvalidOperationException)
            {
                ;
            }

            target._isLoggedIn = true;
            actual = target.updateProjectsForUser();
            Assert.AreEqual(expected, actual);

            target.HistoricMode = true;
            actual = target.updateProjectsForUser();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for updateAllTeams
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ViewModel.dll")]
        public void updateAllTeamsTest()
        {
            bool expected = true;
            bool actual;
            target._isLoggedIn = false;
            try
            {
                actual = target.updateAllTeams();
                Assert.Fail("Exception not thrown");
            }
            catch (InvalidOperationException)
            {
                ;
            }

            target._isLoggedIn = true;
            actual = target.updateAllTeams();
            Assert.AreEqual(expected, actual);
            actual = target.updateAllTeams();
        }

        /// <summary>
        ///A test for hashPassword
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ViewModel.dll")]
        public void hashPasswordTest()
        {
            string password = "password";
            string expected = "6Pl/upEE0epQR5SObftn+s2fW3M=";
            string actual;
            actual = target.hashPassword(password);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ValidateTeam
        ///</summary>
        [TestMethod()]
        public void ValidateTeamTest()
        {
            string name = string.Empty;
            UserView manager = null;
            UserView lead = null;
            bool expected = false;
            bool actual;
            actual = target.ValidateTeam(name, manager, lead);
            Assert.AreEqual(expected, actual);

            name = "Team name";
            manager = target.GetUserByID(1);
            lead = target.GetUserByID(1);
            expected = true;
            actual = target.ValidateTeam(name, manager, lead);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ValidateTask
        ///</summary>
        [TestMethod()]
        public void ValidateTaskTest()
        {
            string text = null;
            UserView owner = null;
            Nullable<TaskType> type = new Nullable<TaskType>();
            Nullable<int> size = new Nullable<int>();
            Nullable<int> value = new Nullable<int>();
            Nullable<DateTime> completion = new Nullable<DateTime>();
            Nullable<TaskState> state = new Nullable<TaskState>();

            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.ValidateTask(text, owner, type, size, value, completion, state);
            Assert.AreEqual(expected, actual);

            text = "text";
            size = 1;
            value = 1;
            state = TaskState.In_Progress;
            type = TaskType.Development;
            actual = target.ValidateTask(text, owner, type, size, value, completion, state);
            Assert.AreEqual(expected, actual);

            state = TaskState.Unassigned;
            expected = true;
            actual = target.ValidateTask(text, owner, type, size, value, completion, state);
            Assert.AreEqual(expected, actual);

            state = TaskState.In_Progress;
            owner = target.GetUserByID(1);
            actual = target.ValidateTask(text, owner, type, size, value, completion, state);
            Assert.AreEqual(expected, actual);

            state = TaskState.Unassigned;
            expected = false;
            actual = target.ValidateTask(text, owner, type, size, value, completion, state);
            Assert.AreEqual(expected, actual);

            state = TaskState.Completed;
            actual = target.ValidateTask(text, owner, type, size, value, completion, state);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ValidateStory
        ///</summary>
        [TestMethod()]
        public void ValidateStoryTest()
        {
            string priority = string.Empty;
            string text = null;
            bool expected = false;
            bool actual;
            actual = target.ValidateStory(priority, text);
            Assert.AreEqual(expected, actual);

            priority = "100";
            text = "text";
            expected = true;
            actual = target.ValidateStory(priority, text);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ValidateSprint
        ///</summary>
        [TestMethod()]
        public void ValidateSprintTest()
        {
            string name = null;
            Nullable<DateTime> startDate = null;
            Nullable<DateTime> endDate = null;
            bool expected = false;
            bool actual;
            actual = target.ValidateSprint(name, startDate, endDate);
            Assert.AreEqual(expected, actual);

            expected = true;
            name = "name";
            target.JumpToTask(new TaskView(target._dataModel.GetTaskByID(1)));
            startDate = new DateTime(2011, 10, 29);
            endDate = new DateTime(2011, 11, 2);
            actual = target.ValidateSprint(name, startDate, endDate);
            Assert.AreEqual(expected, actual);

            target.JumpToTask(new TaskView(target._dataModel.GetTaskByID(2)));
            startDate = new DateTime(2012, 10, 29);
            endDate = new DateTime(2012, 11, 2);
            actual = target.ValidateSprint(name, startDate, endDate);
            Assert.AreEqual(expected, actual);
        }


        /// <summary>
        ///A test for ValidateProject
        ///</summary>
        [TestMethod()]
        public void ValidateProjectTest()
        {
            string name = null;
            Nullable<DateTime> startDate = new Nullable<DateTime>();
            Nullable<DateTime> endDate = new Nullable<DateTime>();
            UserView owner = null;
            TeamView team = null;
            bool expected = false;
            bool actual;
            actual = target.ValidateProject(name, startDate, endDate, owner, team);
            Assert.AreEqual(expected, actual);

            name = "name";
            startDate = DateTime.Today;
            endDate = null;
            owner = target.GetUserByID(1);
            team = target.GetTeamByID(1);
            expected = true;
            actual = target.ValidateProject(name, startDate, endDate, owner, team);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ToggleHistoricMode
        ///</summary>
        [TestMethod()]
        public void ToggleHistoricModeTest()
        {
            target.HistoricMode = false;

            bool expected = true;
            target.ToggleHistoricMode();
            bool actual = target.HistoricMode;
            Assert.AreEqual(actual, expected);

            expected = false;
            target.ToggleHistoricMode();
            actual = target.HistoricMode;
            Assert.AreEqual(target.HistoricMode, expected);

        }

        /// <summary>
        ///A test for MoveUserToTeam
        ///</summary>
        [TestMethod()]
        public void MoveUserToTeamTest()
        {
            UserView user = null;
            TeamView team = null;
            bool expected = false;
            bool actual;
            try
            {
                actual = target.MoveUserToTeam(user, team);
                Assert.Fail("Exception not thrown");
            }
            catch (ArgumentNullException)
            {
                ;
            }

            user = target.GetUserByID(1);
            try
            {
                actual = target.MoveUserToTeam(user, team);
                Assert.Fail("Exception not thrown");
            }
            catch (ArgumentNullException)
            {
                ;
            }

            team = target.GetTeamByID(1);
            expected = true;
            actual = target.MoveUserToTeam(user, team);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for JumpToTask
        ///</summary>
        [TestMethod()]
        public void JumpToTaskTest()
        {
            TaskView task = null; // TODO: Initialize to an appropriate value

            target._isLoggedIn = false;
            try
            {
                target.JumpToTask(task);
                Assert.Fail("Exception not thrown");
            }
            catch (InvalidOperationException)
            {
                ;
            }

            target._isLoggedIn = true;
            try
            {
                target.JumpToTask(task);
                Assert.Fail("Exception not thrown");
            }
            catch (ArgumentNullException)
            {
                ;
            }

            task = new TaskView(target._dataModel.GetTaskByID(1));
            target.JumpToTask(task);
            Assert.AreEqual(task.TaskID, target.CurrTask.TaskID);
        }

        /// <summary>
        ///A test for GetUserByID
        ///</summary>
        [TestMethod()]
        public void GetUserByIDTest()
        {
            int userId = -1;
            UserView expected = new UserView(target._dataModel.GetUserByID(1));
            UserView actual;

            target._isLoggedIn = false;
            try
            {
                actual = target.GetUserByID(userId);
                Assert.Fail("Exception not thrown");
            }
            catch (InvalidOperationException)
            {
                ;
            }

            target._isLoggedIn = true;
            try
            {
                actual = target.GetUserByID(userId);
                Assert.Fail("Exception not thrown");
            }
            catch (ArgumentOutOfRangeException)
            {
                ;
            }

            userId = 1;
            actual = target.GetUserByID(userId);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetTeamMembers
        ///</summary>
        [TestMethod()]
        public void GetTeamMembersTest()
        {
            TeamView team = null;
            Tuple<ObservableCollection<UserView>, ObservableCollection<UserView>> actual;

            target._isLoggedIn = false;
            try
            {
                actual = target.GetTeamMembers(team);
                Assert.Fail("Exception not thrown");
            }
            catch (InvalidOperationException)
            {
                ;
            }

            target._isLoggedIn = true;
            try
            {
                actual = target.GetTeamMembers(team);
                Assert.Fail("Exception not thrown");
            }
            catch (ArgumentNullException)
            {
                ;
            }

            team = target.GetTeamByID(1);
            actual = target.GetTeamMembers(team);

            Assert.AreEqual(actual.Item1.Count, 1);
            Assert.AreEqual(actual.Item1.Count, 1);

            Assert.AreEqual(actual.Item1[0], target.GetUserByID(1));
            Assert.AreEqual(actual.Item2[0], target.GetUserByID(2));
        }

        /// <summary>
        ///A test for GetTeamForUser
        ///</summary>
        [TestMethod()]
        public void GetTeamForUserTest()
        {
            UserView user = null;
            TeamView expected = target.GetTeamByID(1);
            TeamView actual;
            target._isLoggedIn = false;
            try
            {
                actual = target.GetTeamForUser(user);
                Assert.Fail("Exception not thrown");
            }
            catch (InvalidOperationException)
            {
                ;
            }

            target._isLoggedIn = true;
            try
            {
                actual = target.GetTeamForUser(user);
                Assert.Fail("Exception not thrown");
            }
            catch (ArgumentNullException)
            {
                ;
            }

            user = target.GetUserByID(1);
            actual = target.GetTeamForUser(user);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetTeamByID
        ///</summary>
        [TestMethod()]
        public void GetTeamByIDTest()
        {
            int teamId = -1;
            TeamView expected = new TeamView(target._dataModel.GetTeamByID(1));
            TeamView actual;
            target._isLoggedIn = false;
            try
            {
                actual = target.GetTeamByID(teamId);
                Assert.Fail("Exception not thrown");
            }
            catch (InvalidOperationException)
            {
                ;
            }

            target._isLoggedIn = true;
            try
            {
                actual = target.GetTeamByID(teamId);
                Assert.Fail("Exception not thrown");
            }
            catch (ArgumentOutOfRangeException)
            {
                ;
            }

            teamId = 1;
            actual = target.GetTeamByID(teamId);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetManagers
        ///</summary>
        [TestMethod()]
        public void GetManagersTest()
        {
            ObservableCollection<UserView> actual;
            UserView userOne = target.GetUserByID(1);
            UserView userTwo = target.GetUserByID(2);

            target._isLoggedIn = false;
            try
            {
                actual = target.GetManagers();
                Assert.Fail("Exception not thrown");
            }
            catch (InvalidOperationException)
            {
                ;
            }

            target._isLoggedIn = true;
            actual = target.GetManagers();
            Assert.AreEqual(actual.Count, 2);
            Assert.AreEqual(actual[0], userOne);
            Assert.AreEqual(actual[1], userTwo);
        }

        /// <summary>
        ///A test for GetCurrSprintBurndown
        ///</summary>
        [TestMethod()]
        public void GetCurrSprintBurndownTest()
        {
            Tuple<IDictionary<DateTime, double>, IDictionary<DateTime, int>> actual;
            try
            {
                actual = target.GetCurrSprintBurndown();
                Assert.Fail("Exception not thrown");
            }
            catch (InvalidOperationException)
            {
                ;
            }

            target.JumpToTask(new TaskView(target._dataModel.GetTaskByID(2)));
            try
            {
                actual = target.GetCurrSprintBurndown();
                Assert.Fail("Exception not thrown");
            }
            catch (InvalidOperationException)
            {
                ;
            }

            target.JumpToTask(new TaskView(target._dataModel.GetTaskByID(1)));
            actual = target.GetCurrSprintBurndown();
            Assert.AreEqual(actual.Item1.Count, (target.CurrSprint.EndDate.Value - target.CurrSprint.StartDate).Days + 1);
            Assert.AreEqual(actual.Item2.Count, (target.CurrSprint.EndDate.Value - target.CurrSprint.StartDate).Days + 1);

            target.CurrSprint.EndDate = null;
            actual = target.GetCurrSprintBurndown();
            Assert.AreEqual(actual.Item1.Count, (DateTime.Today - target.CurrSprint.StartDate).Days + 1);
            Assert.AreEqual(actual.Item2.Count, (DateTime.Today - target.CurrSprint.StartDate).Days + 1);
        }

        /// <summary>
        ///A test for CreateTeam
        ///</summary>
        [TestMethod()]
        public void CreateTeamTest()
        {
            string name = null;
            UserView manager = null;
            UserView lead = null;
            bool expected = true;
            bool actual;
            target._isLoggedIn = false;
            try
            {
                actual = target.CreateTeam(name, manager, lead);
                Assert.Fail("Exception not thrown");
            }
            catch (InvalidOperationException)
            {
                ;
            }

            target._isLoggedIn = true;
            manager = target.GetUserByID(1);
            lead = target.GetUserByID(1);
            try
            {
                actual = target.CreateTeam(name, manager, lead);
                Assert.Fail("Exception not thrown");
            }
            catch (ArgumentNullException)
            {
                ;
            }

            name = "name";
            actual = target.CreateTeam(name, manager, lead);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for CreateTask
        ///</summary>
        [TestMethod()]
        public void CreateTaskTest()
        {
            string text = null;
            int size = -1;
            int value = 1;
            UserView owner = null;
            TaskType type = TaskType.Development;
            TaskState state = TaskState.Unassigned;
            bool expected = true;
            bool actual;
            try
            {
                actual = target.CreateTask(text, size, value, owner, type, state);
                Assert.Fail("Exception not thrown");
            }
            catch (InvalidOperationException)
            {
                ;
            }

            target.JumpToTask(new TaskView(target._dataModel.GetTaskByID(1)));
            try
            {
                actual = target.CreateTask(text, size, value, owner, type, state);
                Assert.Fail("Exception not thrown");
            }
            catch (ArgumentOutOfRangeException)
            {
                ;
            }

            size = 1;
            try
            {
                actual = target.CreateTask(text, size, value, owner, type, state);
                Assert.Fail("Exception not thrown");
            }
            catch (ArgumentNullException)
            {
                ;
            }

            text = "text";
            state = TaskState.In_Progress;
            try
            {
                actual = target.CreateTask(text, size, value, owner, type, state);
                Assert.Fail("Exception not thrown");
            }
            catch (InvalidOperationException)
            {
                ;
            }

            state = TaskState.Unassigned;
            actual = target.CreateTask(text, size, value, owner, type, state);
            Assert.AreEqual(expected, actual);

            state = TaskState.In_Progress;
            owner = target.GetUserByID(1);
            actual = target.CreateTask(text, size, value, owner, type, state);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for CreateStory
        ///</summary>
        [TestMethod()]
        public void CreateStoryTest()
        {
            string priority = null;
            string text = "text";
            bool expected = true;
            bool actual;
            try
            {
                actual = target.CreateStory(priority, text);
                Assert.Fail("Exception not thrown");
            }
            catch (InvalidOperationException)
            {
                ;
            }

            target.JumpToTask(new TaskView(target._dataModel.GetTaskByID(1)));
            try
            {
                actual = target.CreateStory(priority, text);
                Assert.Fail("Exception not thrown");
            }
            catch (ArgumentNullException)
            {
                ;
            }

            priority = "abc";
            try
            {
                actual = target.CreateStory(priority, text);
                Assert.Fail("Exception not thrown");
            }
            catch (ArgumentException)
            {
                ;
            }

            priority = "-1";
            try
            {
                actual = target.CreateStory(priority, text);
                Assert.Fail("Exception not thrown");
            }
            catch (ArgumentOutOfRangeException)
            {
                ;
            }

            priority = "1";
            actual = target.CreateStory(priority, text);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for CreateSprint
        ///</summary>
        [TestMethod()]
        public void CreateSprintTest()
        {
            string name = null;
            Nullable<DateTime> startDate = DateTime.Today;
            Nullable<DateTime> endDate = null;
            bool expected = true;
            bool actual;
            try
            {
                actual = target.CreateSprint(name, startDate, endDate);
                Assert.Fail("Exception not thrown");
            }
            catch (InvalidOperationException)
            {
                ;
            }

            target.JumpToTask(new TaskView(target._dataModel.GetTaskByID(1)));
            try
            {
                actual = target.CreateSprint(name, startDate, endDate);
                Assert.Fail("Exception not thrown");
            }
            catch (ArgumentNullException)
            {
                ;
            }

            name = "name";
            actual = target.CreateSprint(name, startDate, endDate);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for CreateProject
        ///</summary>
        [TestMethod()]
        public void CreateProjectTest()
        {
            string name = "name";
            Nullable<DateTime> startDate = null;
            Nullable<DateTime> endDate = null;
            UserView owner = target.GetUserByID(1);
            TeamView team = target.GetTeamByID(1);
            bool expected = true;
            bool actual;

            target._isLoggedIn = false;
            try
            {
                actual = target.CreateProject(name, startDate, endDate, owner, team);
            }
            catch (InvalidOperationException)
            {
                ;
            }

            target._isLoggedIn = true;
            try
            {
                actual = target.CreateProject(name, startDate, endDate, owner, team);
            }
            catch (ArgumentNullException)
            {
                ;
            }

            startDate = DateTime.Today;
            actual = target.CreateProject(name, startDate, endDate, owner, team);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ChangeCurrTask
        ///</summary>
        [TestMethod()]
        public void ChangeCurrTaskTest()
        {
            SPMSViewModel target = new SPMSViewModel(); // TODO: Initialize to an appropriate value
            string text = string.Empty; // TODO: Initialize to an appropriate value
            int size = 0; // TODO: Initialize to an appropriate value
            int value = 0; // TODO: Initialize to an appropriate value
            UserView owner = null; // TODO: Initialize to an appropriate value
            TaskType type = new TaskType(); // TODO: Initialize to an appropriate value
            TaskState state = new TaskState(); // TODO: Initialize to an appropriate value
            Nullable<DateTime> completion = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.ChangeCurrTask(text, size, value, owner, type, state, completion);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ChangeCurrStory
        ///</summary>
        [TestMethod()]
        public void ChangeCurrStoryTest()
        {
            SPMSViewModel target = new SPMSViewModel(); // TODO: Initialize to an appropriate value
            string priority = string.Empty; // TODO: Initialize to an appropriate value
            string text = string.Empty; // TODO: Initialize to an appropriate value
            SprintView sprint = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.ChangeCurrStory(priority, text, sprint);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ChangeCurrSprint
        ///</summary>
        [TestMethod()]
        public void ChangeCurrSprintTest()
        {
            SPMSViewModel target = new SPMSViewModel(); // TODO: Initialize to an appropriate value
            string name = string.Empty; // TODO: Initialize to an appropriate value
            Nullable<DateTime> startDate = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            Nullable<DateTime> endDate = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.ChangeCurrSprint(name, startDate, endDate);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ChangeCurrProject
        ///</summary>
        [TestMethod()]
        public void ChangeCurrProjectTest()
        {
            SPMSViewModel target = new SPMSViewModel(); // TODO: Initialize to an appropriate value
            string name = string.Empty; // TODO: Initialize to an appropriate value
            Nullable<DateTime> startDate = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            Nullable<DateTime> endDate = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            UserView owner = null; // TODO: Initialize to an appropriate value
            TeamView team = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.ChangeCurrProject(name, startDate, endDate, owner, team);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for AuthenticateUser
        ///</summary>
        [TestMethod()]
        public void AuthenticateUserTest()
        {
            int userId = 1;
            string password = "bad_pass";
            bool expected = false;
            bool actual;
            actual = target.AuthenticateUser(userId, password);
            Assert.AreEqual(expected, actual);

            password = "password";
            expected = true;
            actual = target.AuthenticateUser(userId, password);
            Assert.AreEqual(expected, actual);
        }
    }
}
