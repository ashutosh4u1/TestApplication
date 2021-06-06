//Interface for user repository
#region "References"
using System.Collections.Generic;
using TestApp.Model;
using System.Linq;
#endregion

namespace TestApp.Data
{
    public interface IUserRepository
    {
        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>Returns the list of users</returns>
        IQueryable<UserModel> GetAllUsers();

        /// <summary>
        /// Add new user
        /// </summary>
        /// <returns>Returns boolean value. True of succeed else false</returns>
        bool AddNewUser(UserModel userModel);

        /// <summary>
        /// Add new with validating third party cpnnection.
        /// </summary>
        /// <returns>Returns boolean value. True of succeed else false</returns>
        bool AddNewUserWithThirdPartyValidation(UserRepository userRepository);

        /// <summary>
        /// To validate any third party source
        /// </summary>
        /// <returns>Returns boolean value for validation status </returns>
        bool IsThirdPartyConnectionValid(bool isConnected);
    }
}
