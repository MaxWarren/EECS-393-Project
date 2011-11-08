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
        ///A test for TasksForUser
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ViewModel.dll")]
        public void TasksForUserTest()
        {
            ObservableCollection<TaskView> expected = null; // TODO: Initialize to an appropriate value
            ObservableCollection<TaskView> actual;
            target.TasksForUser = expected;
            actual = target.TasksForUser;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for TasksForStory
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ViewModel.dll")]
        public void TasksForStoryTest()
        {
            SPMSViewModel_Accessor target = new SPMSViewModel_Accessor(); // TODO: Initialize to an appropriate value
            ObservableCollection<TaskView> expected = null; // TODO: Initialize to an appropriate value
            ObservableCollection<TaskView> actual;
            target.TasksForStory = expected;
            actual = target.TasksForStory;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for StoriesForSprint
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ViewModel.dll")]
        public void StoriesForSprintTest()
        {
            SPMSViewModel_Accessor target = new SPMSViewModel_Accessor(); // TODO: Initialize to an appropriate value
            ObservableCollection<StoryView> expected = null; // TODO: Initialize to an appropriate value
            ObservableCollection<StoryView> actual;
            target.StoriesForSprint = expected;
            actual = target.StoriesForSprint;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for SprintsForProject
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ViewModel.dll")]
        public void SprintsForProjectTest()
        {
            SPMSViewModel_Accessor target = new SPMSViewModel_Accessor(); // TODO: Initialize to an appropriate value
            ObservableCollection<SprintView> expected = null; // TODO: Initialize to an appropriate value
            ObservableCollection<SprintView> actual;
            target.SprintsForProject = expected;
            actual = target.SprintsForProject;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ProjectsForUser
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ViewModel.dll")]
        public void ProjectsForUserTest()
        {
            SPMSViewModel_Accessor target = new SPMSViewModel_Accessor(); // TODO: Initialize to an appropriate value
            ObservableCollection<ProjectView> expected = null; // TODO: Initialize to an appropriate value
            ObservableCollection<ProjectView> actual;
            target.ProjectsForUser = expected;
            actual = target.ProjectsForUser;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsManager
        ///</summary>
        [TestMethod()]
        public void IsManagerTest()
        {
            SPMSViewModel target = new SPMSViewModel(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            target.IsManager = expected;
            actual = target.IsManager;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for HistoricMode
        ///</summary>
        [TestMethod()]
        public void HistoricModeTest()
        {
            SPMSViewModel target = new SPMSViewModel(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            target.HistoricMode = expected;
            actual = target.HistoricMode;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CurrUser
        ///</summary>
        [TestMethod()]
        public void CurrUserTest()
        {
            SPMSViewModel target = new SPMSViewModel(); // TODO: Initialize to an appropriate value
            UserView expected = null; // TODO: Initialize to an appropriate value
            UserView actual;
            target.CurrUser = expected;
            actual = target.CurrUser;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CurrTeam
        ///</summary>
        [TestMethod()]
        public void CurrTeamTest()
        {
            SPMSViewModel target = new SPMSViewModel(); // TODO: Initialize to an appropriate value
            TeamView expected = null; // TODO: Initialize to an appropriate value
            TeamView actual;
            target.CurrTeam = expected;
            actual = target.CurrTeam;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CurrTask
        ///</summary>
        [TestMethod()]
        public void CurrTaskTest()
        {
            SPMSViewModel target = new SPMSViewModel(); // TODO: Initialize to an appropriate value
            TaskView expected = null; // TODO: Initialize to an appropriate value
            TaskView actual;
            target.CurrTask = expected;
            actual = target.CurrTask;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CurrStory
        ///</summary>
        [TestMethod()]
        public void CurrStoryTest()
        {
            SPMSViewModel target = new SPMSViewModel(); // TODO: Initialize to an appropriate value
            StoryView expected = null; // TODO: Initialize to an appropriate value
            StoryView actual;
            target.CurrStory = expected;
            actual = target.CurrStory;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CurrSprint
        ///</summary>
        [TestMethod()]
        public void CurrSprintTest()
        {
            SPMSViewModel target = new SPMSViewModel(); // TODO: Initialize to an appropriate value
            SprintView expected = null; // TODO: Initialize to an appropriate value
            SprintView actual;
            target.CurrSprint = expected;
            actual = target.CurrSprint;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CurrProject
        ///</summary>
        [TestMethod()]
        public void CurrProjectTest()
        {
            SPMSViewModel target = new SPMSViewModel(); // TODO: Initialize to an appropriate value
            ProjectView expected = null; // TODO: Initialize to an appropriate value
            ProjectView actual;
            target.CurrProject = expected;
            actual = target.CurrProject;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for AllTeams
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ViewModel.dll")]
        public void AllTeamsTest()
        {
            SPMSViewModel_Accessor target = new SPMSViewModel_Accessor(); // TODO: Initialize to an appropriate value
            ObservableCollection<TeamView> expected = null; // TODO: Initialize to an appropriate value
            ObservableCollection<TeamView> actual;
            target.AllTeams = expected;
            actual = target.AllTeams;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for updateTasksForUser
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ViewModel.dll")]
        public void updateTasksForUserTest()
        {
            SPMSViewModel_Accessor target = new SPMSViewModel_Accessor(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.updateTasksForUser();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for updateTasksForStory
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ViewModel.dll")]
        public void updateTasksForStoryTest()
        {
            SPMSViewModel_Accessor target = new SPMSViewModel_Accessor(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.updateTasksForStory();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for updateStoriesForSprint
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ViewModel.dll")]
        public void updateStoriesForSprintTest()
        {
            SPMSViewModel_Accessor target = new SPMSViewModel_Accessor(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.updateStoriesForSprint();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for updateSprintsForProject
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ViewModel.dll")]
        public void updateSprintsForProjectTest()
        {
            SPMSViewModel_Accessor target = new SPMSViewModel_Accessor(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.updateSprintsForProject();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for updateProjectsForUser
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ViewModel.dll")]
        public void updateProjectsForUserTest()
        {
            SPMSViewModel_Accessor target = new SPMSViewModel_Accessor(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.updateProjectsForUser();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for updateAllTeams
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ViewModel.dll")]
        public void updateAllTeamsTest()
        {
            SPMSViewModel_Accessor target = new SPMSViewModel_Accessor(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.updateAllTeams();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for hashPassword
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ViewModel.dll")]
        public void hashPasswordTest()
        {
            SPMSViewModel_Accessor target = new SPMSViewModel_Accessor(); // TODO: Initialize to an appropriate value
            string password = string.Empty; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.hashPassword(password);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ValidateTeam
        ///</summary>
        [TestMethod()]
        public void ValidateTeamTest()
        {
            SPMSViewModel target = new SPMSViewModel(); // TODO: Initialize to an appropriate value
            string name = string.Empty; // TODO: Initialize to an appropriate value
            UserView manager = null; // TODO: Initialize to an appropriate value
            UserView lead = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.ValidateTeam(name, manager, lead);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ValidateTask
        ///</summary>
        [TestMethod()]
        public void ValidateTaskTest()
        {
            SPMSViewModel target = new SPMSViewModel(); // TODO: Initialize to an appropriate value
            string text = string.Empty; // TODO: Initialize to an appropriate value
            UserView owner = null; // TODO: Initialize to an appropriate value
            Nullable<TaskType> type = new Nullable<TaskType>(); // TODO: Initialize to an appropriate value
            Nullable<int> size = new Nullable<int>(); // TODO: Initialize to an appropriate value
            Nullable<int> value = new Nullable<int>(); // TODO: Initialize to an appropriate value
            Nullable<DateTime> completion = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            Nullable<TaskState> state = new Nullable<TaskState>(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.ValidateTask(text, owner, type, size, value, completion, state);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ValidateStory
        ///</summary>
        [TestMethod()]
        public void ValidateStoryTest()
        {
            SPMSViewModel target = new SPMSViewModel(); // TODO: Initialize to an appropriate value
            string priority = string.Empty; // TODO: Initialize to an appropriate value
            string text = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.ValidateStory(priority, text);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ValidateSprint
        ///</summary>
        [TestMethod()]
        public void ValidateSprintTest()
        {
            SPMSViewModel target = new SPMSViewModel(); // TODO: Initialize to an appropriate value
            string name = string.Empty; // TODO: Initialize to an appropriate value
            Nullable<DateTime> startDate = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            Nullable<DateTime> endDate = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.ValidateSprint(name, startDate, endDate);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ValidateProject
        ///</summary>
        [TestMethod()]
        public void ValidateProjectTest()
        {
            SPMSViewModel target = new SPMSViewModel(); // TODO: Initialize to an appropriate value
            string name = string.Empty; // TODO: Initialize to an appropriate value
            Nullable<DateTime> startDate = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            Nullable<DateTime> endDate = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            UserView owner = null; // TODO: Initialize to an appropriate value
            TeamView team = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.ValidateProject(name, startDate, endDate, owner, team);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ToggleHistoricMode
        ///</summary>
        [TestMethod()]
        public void ToggleHistoricModeTest()
        {
            SPMSViewModel target = new SPMSViewModel(); // TODO: Initialize to an appropriate value
            target.ToggleHistoricMode();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for MoveUserToTeam
        ///</summary>
        [TestMethod()]
        public void MoveUserToTeamTest()
        {
            SPMSViewModel target = new SPMSViewModel(); // TODO: Initialize to an appropriate value
            UserView user = null; // TODO: Initialize to an appropriate value
            TeamView team = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.MoveUserToTeam(user, team);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for JumpToTask
        ///</summary>
        [TestMethod()]
        public void JumpToTaskTest()
        {
            SPMSViewModel target = new SPMSViewModel(); // TODO: Initialize to an appropriate value
            TaskView task = null; // TODO: Initialize to an appropriate value
            target.JumpToTask(task);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for GetUserByID
        ///</summary>
        [TestMethod()]
        public void GetUserByIDTest()
        {
            SPMSViewModel target = new SPMSViewModel(); // TODO: Initialize to an appropriate value
            int userId = 0; // TODO: Initialize to an appropriate value
            UserView expected = null; // TODO: Initialize to an appropriate value
            UserView actual;
            actual = target.GetUserByID(userId);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetTeamMembers
        ///</summary>
        [TestMethod()]
        public void GetTeamMembersTest()
        {
            SPMSViewModel target = new SPMSViewModel(); // TODO: Initialize to an appropriate value
            TeamView team = null; // TODO: Initialize to an appropriate value
            Tuple<ObservableCollection<UserView>, ObservableCollection<UserView>> expected = null; // TODO: Initialize to an appropriate value
            Tuple<ObservableCollection<UserView>, ObservableCollection<UserView>> actual;
            actual = target.GetTeamMembers(team);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetTeamForUser
        ///</summary>
        [TestMethod()]
        public void GetTeamForUserTest()
        {
            SPMSViewModel target = new SPMSViewModel(); // TODO: Initialize to an appropriate value
            UserView user = null; // TODO: Initialize to an appropriate value
            TeamView expected = null; // TODO: Initialize to an appropriate value
            TeamView actual;
            actual = target.GetTeamForUser(user);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetTeamByID
        ///</summary>
        [TestMethod()]
        public void GetTeamByIDTest()
        {
            SPMSViewModel target = new SPMSViewModel(); // TODO: Initialize to an appropriate value
            int teamId = 0; // TODO: Initialize to an appropriate value
            TeamView expected = null; // TODO: Initialize to an appropriate value
            TeamView actual;
            actual = target.GetTeamByID(teamId);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetManagers
        ///</summary>
        [TestMethod()]
        public void GetManagersTest()
        {
            SPMSViewModel target = new SPMSViewModel(); // TODO: Initialize to an appropriate value
            ObservableCollection<UserView> expected = null; // TODO: Initialize to an appropriate value
            ObservableCollection<UserView> actual;
            actual = target.GetManagers();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetCurrSprintBurndown
        ///</summary>
        [TestMethod()]
        public void GetCurrSprintBurndownTest()
        {
            SPMSViewModel target = new SPMSViewModel(); // TODO: Initialize to an appropriate value
            Tuple<IDictionary<DateTime, double>, IDictionary<DateTime, int>> expected = null; // TODO: Initialize to an appropriate value
            Tuple<IDictionary<DateTime, double>, IDictionary<DateTime, int>> actual;
            actual = target.GetCurrSprintBurndown();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CreateTeam
        ///</summary>
        [TestMethod()]
        public void CreateTeamTest()
        {
            SPMSViewModel target = new SPMSViewModel(); // TODO: Initialize to an appropriate value
            string name = string.Empty; // TODO: Initialize to an appropriate value
            UserView manager = null; // TODO: Initialize to an appropriate value
            UserView lead = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.CreateTeam(name, manager, lead);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CreateTask
        ///</summary>
        [TestMethod()]
        public void CreateTaskTest()
        {
            SPMSViewModel target = new SPMSViewModel(); // TODO: Initialize to an appropriate value
            string text = string.Empty; // TODO: Initialize to an appropriate value
            int size = 0; // TODO: Initialize to an appropriate value
            int value = 0; // TODO: Initialize to an appropriate value
            UserView owner = null; // TODO: Initialize to an appropriate value
            TaskType type = new TaskType(); // TODO: Initialize to an appropriate value
            TaskState state = new TaskState(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.CreateTask(text, size, value, owner, type, state);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CreateStory
        ///</summary>
        [TestMethod()]
        public void CreateStoryTest()
        {
            SPMSViewModel target = new SPMSViewModel(); // TODO: Initialize to an appropriate value
            string priority = string.Empty; // TODO: Initialize to an appropriate value
            string text = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.CreateStory(priority, text);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CreateSprint
        ///</summary>
        [TestMethod()]
        public void CreateSprintTest()
        {
            SPMSViewModel target = new SPMSViewModel(); // TODO: Initialize to an appropriate value
            string name = string.Empty; // TODO: Initialize to an appropriate value
            Nullable<DateTime> startDate = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            Nullable<DateTime> endDate = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.CreateSprint(name, startDate, endDate);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CreateProject
        ///</summary>
        [TestMethod()]
        public void CreateProjectTest()
        {
            SPMSViewModel target = new SPMSViewModel(); // TODO: Initialize to an appropriate value
            string name = string.Empty; // TODO: Initialize to an appropriate value
            Nullable<DateTime> startDate = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            Nullable<DateTime> endDate = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            UserView owner = null; // TODO: Initialize to an appropriate value
            TeamView team = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.CreateProject(name, startDate, endDate, owner, team);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
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
            SPMSViewModel target = new SPMSViewModel(); // TODO: Initialize to an appropriate value
            int userId = 0; // TODO: Initialize to an appropriate value
            string password = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.AuthenticateUser(userId, password);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
