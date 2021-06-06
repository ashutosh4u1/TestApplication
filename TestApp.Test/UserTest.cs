//Unit Testing
#region "References"
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TestApp.Data;
using TestApp.BL;
using TestApp.Model;
using Moq;
#endregion

namespace Test
{
    [TestClass]
    public class UserTest
    {

        #region "Variable Declaration"
        public IUserRepository _userRepository;
        #endregion

        #region "Constructor"
        public UserTest()
        {
            _userRepository = new UserRepository();
        }
        #endregion

        #region "Functions"
        [TestMethod]
        public void GetAllWomenLessThan25Age()
        {
            //Arrange
            User user = new User(_userRepository);

            //Act
            IEnumerable<UserModel> userList = user.GetUserByAge(Common.Gender.Female, Common.Condition.LessThan, 25);
            int iResultCount=0;//Count of expected
            using (IEnumerator<UserModel> enumerator = userList.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    iResultCount = iResultCount + 1;
                }
            }
            //Assert
            Assert.AreEqual(1, iResultCount);
        }

        [TestMethod]
        public void GetYongestMan()
        {
            //Arrange
            User user = new User(_userRepository);
            //Act
            UserModel userModel = user.GetYoungestUser(Common.Gender.Male);
            //Assert
            Assert.AreEqual("SecondEmployee", userModel.FirstName);
        }

        [TestMethod]
        public void GetYongestManWithInvalidAge()
        {
            //Arrange
            User user = new User(_userRepository);
            //Act
            UserModel userModel = user.GetYoungestUser(Common.Gender.Male);
            //Assert
            Assert.AreEqual("AnyEmployee", userModel.FirstName);
        }

        [TestMethod]
        public void GetAllWomenHavingManagerAdminRole()
        {
            //Arrange
            User user = new User(_userRepository);

            //Act
            IEnumerable<UserModel> userList = user.GetUserByManagerAdminRole(Common.Gender.Female);
            int iResultCount = 0;//Count of expected
            using (IEnumerator<UserModel> enumerator = userList.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    iResultCount = iResultCount + 1;
                }
            }
            //Assert
            Assert.AreEqual(1, iResultCount);
        }

        [TestMethod]
        public void AddUserWithAllValidInformation()
        {
            //Arrange
            User user = new User(_userRepository);

            //Act
            UserModel userModel = new UserModel();
            userModel.FirstName = "NewUser_FName";
            userModel.LastName = "NewUser_LName";
            userModel.Gender = Common.Gender.Male;
            userModel.Age = 25;
            List<Common.Role> roles = new List<Common.Role>();
            roles.Add(Common.Role.Admin);
            userModel.Roles = roles;
            bool isUserAdded = user.AddNewUser(userModel);

            //Assert
            Assert.AreEqual(true, isUserAdded);
        }

        [TestMethod]
        public void AddNewUserAfterThirdPartyValidation_Error_WithoutMocking()
        {
            //Arrange
            User user = new User(_userRepository);
            UserRepository userRepository = new UserRepository();

            //Act
            bool isUserAdded = user.AddNewUserWithThirdPartyValidation(userRepository);

            //Assert
            Assert.AreEqual(true, isUserAdded);
        }

        [TestMethod]
        public void AddNewUserAfterThirdPartyValidation_Success_WithMocking()
        {
            //Arrange
            //mocking the class to bypass the IsDatabaseConnectionValid method. After mocking it will always return true so actual logic will not be used.
            var mockObj = new Mock<UserRepository>() { CallBase = true };
            mockObj.Setup(p => p.IsThirdPartyConnectionValid(false)).Returns(true);

            //Act
            bool isUserAdded = mockObj.Object.AddNewUserWithThirdPartyValidation(mockObj.Object);

            //Assert
            Assert.AreEqual(true, isUserAdded);
        }
        [TestMethod]
        public void AddUserWithNoInformation()
        {
            //Arrange
            User user = new User(_userRepository);
            //Act
            UserModel userModel = new UserModel();
            bool isUserAdded = user.AddNewUser(userModel);

            //Assert
            Assert.IsTrue(isUserAdded);
        }

        [TestMethod]
        public void AddUserWithInvalidAge()
        {
            //Arrange
            User user = new User(_userRepository);
            //Act
            UserModel userModel = new UserModel();
            userModel.FirstName = "NewUser_FName";
            userModel.LastName = "NewUser_LName";
            userModel.Gender = Common.Gender.Male;
            userModel.Age = 0;
            List<Common.Role> roles = new List<Common.Role>();
            roles.Add(Common.Role.Admin);
            userModel.Roles = roles;
            bool isUserAdded = user.AddNewUser(userModel);

            //Assert
            Assert.AreEqual(true, isUserAdded);
        }

        [TestMethod]
        public void AddUserWithoutRole()
        {
            //Arrange
            User user = new User(_userRepository);
            //Act
            UserModel userModel = new UserModel();
            userModel.FirstName = "NewUser_FName";
            userModel.LastName = "NewUser_LName";
            userModel.Gender = Common.Gender.Male;
            userModel.Age = 25;
            bool isUserAdded = user.AddNewUser(userModel);

            //Assert
            Assert.AreEqual(true, isUserAdded);
        }

        [TestMethod]
        public void AddUserWithLastNameHavingLengthMoreThan15()
        {
            //Arrange
            User user = new User(_userRepository);
            //Act
            UserModel userModel = new UserModel();
            userModel.FirstName = "NewUser_FName";
            userModel.LastName = "NewUser_LName_NewUser_LName";
            bool isUserAdded = user.AddNewUser(userModel);

            //Assert
            Assert.AreEqual(true, isUserAdded);
        }

        [TestMethod]
        public void AddUserWithFirstNameHavingLengthMoreThan15()
        {
            //Arrange
            User user = new User(_userRepository);
            //Act
            UserModel userModel = new UserModel();
            userModel.FirstName = "NewUser_FName_NewUser_FName";
            userModel.LastName = "NewUser_LName";
            bool isUserAdded = user.AddNewUser(userModel);

            //Assert
            Assert.AreEqual(true, isUserAdded);
        }

        [TestMethod]
        public void AddUserWithoutGender()
        {
            //Arrange
            User user = new User(_userRepository);
            //Act
            UserModel userModel = new UserModel();
            userModel.FirstName = "NewUser_FName";
            userModel.LastName = "NewUser_LName";
            userModel.LastName = "NewUser_LName";
            bool isUserAdded = user.AddNewUser(userModel);

            //Assert
            Assert.AreEqual(true, isUserAdded);
        }
        #endregion
    }
}
