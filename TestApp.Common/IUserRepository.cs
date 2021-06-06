//Interface for user repository
#region "References"
using System.Collections.Generic;
using TestApp.Model;
using System.Linq;
#endregion

namespace TestApp.Common
{
    public interface IUserRepository
    {
        /// <summary>
        /// Get the users by gender and age
        /// </summary>
        /// <returns>Returns the list of users</returns>
        IQueryable<UserModel>  GetUserByAge(Model.Common.Gender gender, Model.Common.Condition condition, uint age);

        /// <summary>
        /// Get the youngest user
        /// </summary>
        /// <returns>Returns the user object of youngest user</returns>
        UserModel GetYoungestUser(Model.Common.Gender gender);

        /// <summary>
        /// Get the users by gender and roles
        /// </summary>
        /// <returns>Returns the list of users</returns>
        IQueryable<UserModel>  GetUserByManagerAdminRole(Model.Common.Gender gender);
    }
}
