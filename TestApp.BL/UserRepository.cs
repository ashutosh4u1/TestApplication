//Class to interact with outside world
#region "References"
using System.Collections.Generic;
using System.Linq;
using TestApp.Model;
using TestApp.Data;
#endregion

namespace TestApp.BL
{
    public class UserRepository : Common.IUserRepository
    {
        #region "Variable Declaration"
        //Declaration of data model  object
        private UserDBModel userDBModel;
        #endregion

        #region "Constructor"
        public UserRepository()
        {
            //Intantiation of data model object
            userDBModel = new UserDBModel();
        }
        #endregion

        #region "Functions"
        /// <summary>
        /// Get the users by gender and age
        /// </summary>
        /// <returns>Returns the list of users</returns>
        public IEnumerable<UserModel> GetUserByAge(Model.Common.Gender gender, Model.Common.Condition condition, uint age)
        {
            //Step 1 - Populate all the users
            IEnumerable<UserModel> listOfUsers = (from testDBModel in userDBModel.usersInfoRepository
                                                  select new UserModel()
                                                  {
                                                      Id = testDBModel.Id,
                                                      FirstName = testDBModel.FirstName,
                                                      LastName = testDBModel.LastName,
                                                      Gender = testDBModel.Gender,
                                                      Age = testDBModel.Age,
                                                      Roles = testDBModel.Roles
                                                  });

            //Step 2 - Filter the users by gender and age
            switch (condition)
            {
                case Model.Common.Condition.LessThan:
                    listOfUsers = listOfUsers.Where(obj => obj.Gender == gender && obj.Age < age);
                    break;
                case Model.Common.Condition.LessThanOrEqualTo:
                    listOfUsers = listOfUsers.Where(obj => obj.Gender == gender && obj.Age <= age);
                    break;
                case Model.Common.Condition.MoreThan:
                    listOfUsers = listOfUsers.Where(obj => obj.Gender == gender && obj.Age > age);
                    break;
                case Model.Common.Condition.MoreThanOrEqualTo:
                    listOfUsers = listOfUsers.Where(obj => obj.Gender == gender && obj.Age >= age);
                    break;
            }

            //Return the list of matching users
            return listOfUsers;
        }

        /// <summary>
        /// Get the users by gender and roles
        /// </summary>
        /// <returns>Returns the list of users having manager & admin</returns>
        public IEnumerable<UserModel> GetUserByManagerAdminRole(Model.Common.Gender gender)
        {
            //Filter the users by gender
            IEnumerable<UserModel> listOfUsers = (from testDBModel in userDBModel.usersInfoRepository
                                                  select new UserModel()
                                                  {
                                                      Id = testDBModel.Id,
                                                      FirstName = testDBModel.FirstName,
                                                      LastName = testDBModel.LastName,
                                                      Gender = testDBModel.Gender,
                                                      Age = testDBModel.Age,
                                                      Roles = testDBModel.Roles
                                                  }).Where(obj => obj.Gender == gender && obj.Roles.Contains(Model.Common.Role.Manager) && obj.Roles.Contains(Model.Common.Role.Admin));

            //Return the list of matching users
            return listOfUsers;
        }

        /// <summary>
        /// Get the youngest user
        /// </summary>
        /// <returns>Returns the user object of youngest user</returns>
        public UserModel GetYoungestUser(Model.Common.Gender gender)
        {
            //Find the youngest user by gender
            UserModel listOfUsers = (from testDBModel in userDBModel.usersInfoRepository
                                                  select new UserModel()
                                                  {
                                                      Id = testDBModel.Id,
                                                      FirstName = testDBModel.FirstName,
                                                      LastName = testDBModel.LastName,
                                                      Gender = testDBModel.Gender,
                                                      Age = testDBModel.Age,
                                                      Roles = testDBModel.Roles
                                                  }).OrderBy(obj => obj.Age).FirstOrDefault();

            //Return the user object
            return listOfUsers;
        }
        #endregion
    }
}
