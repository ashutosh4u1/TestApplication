//Class to interact with outside world
#region "References"
using System.Collections.Generic;
using System.Linq;
using TestApp.Model;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TestApp.Data;
#endregion

namespace TestApp.BL
{
    public class User : IUserRepository
    {
        //#region "Variable Declaration"
        //Declaration variable of user repository
        public IUserRepository _userRepository;
        //#endregion


        #region "Constructor"
        public User()
        {
            _userRepository = new Data.UserRepository();
        }

        //Dependency Injection
        public User(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        #endregion

        #region "Functions"

        /// <summary>
        /// Get alll the users.
        /// </summary>
        /// <returns>Returns list of all the users.</returns>
        public IQueryable<UserModel> GetAllUsers()
        {
            try
            {
                return _userRepository.GetAllUsers();
            }
            catch (Exception ex)
            {
                this.logException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// Add new user
        /// </summary>
        /// <returns>Returns the boolean value true if succeed else false.</returns>
        public bool AddNewUser(UserModel userModel)
        {
            try
            {
                bool returnValue;
                //Validate the user
                returnValue = isValid(userModel);
                //If valid then go for adding the user
                if (returnValue)
                {
                    //Add logic to add new user
                    _userRepository.AddNewUser(userModel);
                }
                return returnValue;
            }
            catch (Exception ex)
            {
                this.logException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// Validates third party connection and add the user if suceed
        /// </summary>
        /// <param name="userRepository"></param>
        /// <returns>Boolean value whether succeed or failed</returns>
        public bool AddNewUserWithThirdPartyValidation(UserRepository userRepository)
        {
            try
            {
                if (IsThirdPartyConnectionValid(false))
                {
                    //Will have the logic to further processing
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                this.logException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// Return status of third party source validation
        /// </summary>
        /// <param name="isConnected"></param>
        /// <returns></returns>
        public virtual bool IsThirdPartyConnectionValid(bool isConnected)
        {
            try {
                //Will have logic to validate the third party source. This is written only for mocking.
                return isConnected;
            }
            catch (Exception ex)
            {
                this.logException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// Get the users by gender and age
        /// </summary>
        /// <returns>Returns the list of users</returns>
        public IQueryable<UserModel> GetUserByAge(Model.Common.Gender gender, Model.Common.Condition condition, uint age)
        {
            try
            {
            //Step 1 - Populate all the users
            IQueryable<UserModel> listOfUsers = this.GetAllUsers();

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
                default:
                    listOfUsers = Enumerable.Empty<UserModel>().AsQueryable();
                    break;
            }

            //Return the list of matching users
            return listOfUsers;
            }
            catch (Exception ex)
            {
                this.logException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// Get the users by gender and roles
        /// </summary>
        /// <returns>Returns the list of users having manager & admin</returns>
        public IQueryable<UserModel> GetUserByManagerAdminRole(Model.Common.Gender gender)
        {
            try {
                //Get all the users
                IQueryable<UserModel> listOfAllUsers = this.GetAllUsers();

                //Filter and Return the list of matching users
                return listOfAllUsers.Where(obj => obj.Gender == gender 
                                                    && obj.Roles.Contains(Model.Common.Role.Manager) 
                                                    && obj.Roles.Contains(Model.Common.Role.Admin));
            }
            catch (Exception ex)
            {
                this.logException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// Get the youngest user
        /// </summary>
        /// <returns>Returns the user object of youngest user</returns>
        public UserModel GetYoungestUser(Model.Common.Gender gender)
        {
            try {
                //Get all the users
                IQueryable<UserModel> listOfAllUsers = this.GetAllUsers();

                //Find the youngest user by Age
                uint minAge = listOfAllUsers.Min(obj => obj.Age);

                //Return the user detail
                return listOfAllUsers.Where(obj => obj.Age == minAge).First();
            }
            catch (Exception ex)
            {
                this.logException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// Validates the user information using annotation
        /// </summary>
        /// <returns>Returns the boolean value true if succeed else false.</returns>
        private bool isValid(UserModel userModel)
        {
            try { 
            bool isValid = true;

            //Check if data exists
            if (userModel != null)
            {
                //ValidationContext context = new ValidationContext(userModel);
                IList<ValidationResult> reults = new List<ValidationResult>();
                if (!Validator.TryValidateObject(userModel, new ValidationContext(userModel), reults, true))
                {
                    foreach (ValidationResult error in reults)
                    {
                        Console.WriteLine(error.ErrorMessage);
                        isValid = false;
                    }
                }
            }
            else
            {
                isValid = false;
                Console.WriteLine("No data provided for user");
            }
            return isValid;
            }
            catch (Exception ex)
            {
                this.logException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// Validates the user information for each fields manually
        /// </summary>
        /// <returns>Returns the boolean value true if succeed else false.</returns>
        private bool isValidManual(UserModel userModel)
        {
            try { 
            bool isValid = true;
            StringBuilder sbValidationMessage = new StringBuilder();

            //Check if data exists
            if (userModel != null)
            {
                if (string.IsNullOrEmpty(userModel.FirstName))
                {
                    isValid = false;
                    sbValidationMessage.Append("First name is mandatory");
                }
                if (string.IsNullOrEmpty(userModel.LastName))
                {
                    isValid = false;
                    sbValidationMessage.Append("Last name is mandatory");
                }

                if (userModel.Age == 0)
                {
                    isValid = false;
                    sbValidationMessage.Append("Age should be valid");
                }

                if (userModel.Roles.Count == 0)
                {
                    isValid = false;
                    sbValidationMessage.Append("User should have atleast one role");
                }
            }
            else
            {
                isValid = false;
                Console.WriteLine("No data provided for user");
            }
            return isValid;
            }
            catch (Exception ex)
            {
                this.logException(ex);
                throw ex;
            }
        }

        //Log the excption occured.
        public void logException(Exception exception)
        {
            throw exception;
        }
        #endregion
    }
}

