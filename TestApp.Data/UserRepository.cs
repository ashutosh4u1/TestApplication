//Class holds the user information as no database present for this application
#region "References"
using System;
using System.Linq;
using TestApp.Model;
using System.Net.Mail;
#endregion

namespace TestApp.Data
{
   public class UserRepository : IUserRepository
    {
        #region "Constructor"
        private DBModels _dbModels;
        #endregion

        #region "Constructor"
        //Populate the user dayta to local lsit variable
        public UserRepository()
        {
            _dbModels = new DBModels(); ;
        }
        #endregion

        //Returns all the users from Database
        public IQueryable<UserModel> GetAllUsers()
        {
            return _dbModels.GetAllUsersFromDatabase().AsQueryable();
        }

        //Add new user to database
        public bool AddNewUser(UserModel userModel)
        {
            return _dbModels.AddNewUserToDatabase(userModel);
        }

        //Add new user to database
        public bool AddNewUserWithThirdPartyValidation(UserRepository userRepository)
        {
            return _dbModels.AddNewUserToDatabase(new UserModel { });
        }

        //Return to vlidate any third party source validation
        public virtual bool IsThirdPartyConnectionValid(bool isConnected)
        {
            return false;
        }
    }
}
