//Class holds the user information as no database present for this application
//This will act as database for test
#region "References"
using System;
using System.Collections.Generic;
using TestApp.Model;
#endregion

namespace TestApp.Data
{
    public class DBModels 
    {
        #region "Variables Declaration"
        private List<UserModel> _usersInfoRepository;
        #endregion

        #region "Constructor"
        //Populate the user dayta to local lsit variable
        public DBModels()
        {
            _usersInfoRepository = new List<UserModel>();

            //User1
            UserModel userInfoOne = new UserModel()
            {
                Id = 1,
                FirstName = "FirstEmployee",
                LastName = "First_LName",
                Gender = Model.Common.Gender.Male,
                Age = 24,
                Roles = new List<Model.Common.Role>()
                {
                    Model.Common.Role.Employee,
                    Model.Common.Role.Admin
                }
            };
            _usersInfoRepository.Add(userInfoOne);

            //User2
            UserModel userInfoTwo = new UserModel()
            {
                Id = 2,
                FirstName = "SecondEmployee",
                LastName = "Second_LName",
                Gender = Model.Common.Gender.Female,
                Age = 20,
                Roles = new List<Model.Common.Role>()
                {
                    Model.Common.Role.Employee,
                    Model.Common.Role.Manager
                }
            };
            _usersInfoRepository.Add(userInfoTwo);

            //User3
            UserModel userInfoThree = new UserModel()
            {
                Id = 3,
                FirstName = "ThirdEmployee",
                LastName = "Third_LName",
                Gender = Model.Common.Gender.Male,
                Age = 30,
                Roles = new List<Model.Common.Role>()
                {
                    Model.Common.Role.Manager
                }
            };
            _usersInfoRepository.Add(userInfoThree);

            //User4
            UserModel userInfoFour = new UserModel()
            {
                Id = 4,
                FirstName = "FourthEmployee",
                LastName = "Fourth_LName",
                Gender = Model.Common.Gender.Female,
                Age = 28,
                Roles = new List<Model.Common.Role>()
                {
                    Model.Common.Role.Admin,
                    Model.Common.Role.Manager
                }
            };
            _usersInfoRepository.Add(userInfoFour);
        }
        #endregion

        #region "Functions"
        //Returns all the users from database
        public List<Model.UserModel> GetAllUsersFromDatabase()
        {
            return _usersInfoRepository;
        }

        //Add new user to database
        public bool AddNewUserToDatabase(UserModel userModel)
        {
            //Add logic to save data to database
            return true;
        }

        //Log exception into file or database as per requirement
        public void logException(string message)
        {
            //Add logic to save exception into file or database as per requirement
        }
        #endregion
    }
}
