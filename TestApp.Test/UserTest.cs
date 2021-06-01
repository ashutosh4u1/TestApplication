//Unit Testing
#region "References"
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
#endregion

namespace TestApp.Test
{
    [TestClass]
    public class UserTest
    {

        #region "Functions"
       [TestMethod]
        public void GetAllWomenLessThan25Age()
        {
            //Arrange
            TestApp.BL.UserRepository userRepo = new TestApp.BL.UserRepository();
            //Act
            IEnumerable<TestApp.Model.UserModel> userList = userRepo.GetUserByAge(TestApp.Model.Common.Gender.Female, TestApp.Model.Common.Condition.LessThan, 25);
            int iResultCount=0;//Count of expected
            using (IEnumerator<TestApp.Model.UserModel> enumerator = userList.GetEnumerator())
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
            TestApp.BL.UserRepository userRepo = new TestApp.BL.UserRepository();
            //Act
            TestApp.Model.UserModel userModel = userRepo.GetYoungestUser(TestApp.Model.Common.Gender.Male);
            //Assert
            Assert.AreEqual("SecondEmployee", userModel.FirstName);
        }

        [TestMethod]
        public void GetAllWomenHavingManagerAdminRole()
        {
            //Arrange
            TestApp.BL.UserRepository userRepo = new TestApp.BL.UserRepository();

            //Act
            IEnumerable<TestApp.Model.UserModel> userList = userRepo.GetUserByManagerAdminRole(TestApp.Model.Common.Gender.Female);
            int iResultCount = 0;//Count of expected
            using (IEnumerator<TestApp.Model.UserModel> enumerator = userList.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    iResultCount = iResultCount + 1;
                }
            }
            //Assert
            Assert.AreEqual(1, iResultCount);
        }
        #endregion
    }
}
