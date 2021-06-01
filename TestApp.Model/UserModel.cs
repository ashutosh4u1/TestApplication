//Class represents the model class for User
#region "References"
using System.Collections.Generic;
#endregion

namespace TestApp.Model
{
    public class UserModel
    {
        #region "Properties"
        //Get/Set id
        public int Id { get; set; }
        //Get/Set first name
        public string FirstName { get; set; }
        //Get/Set last name
        public string LastName { get; set; }
        //Get/Set gender
        public Common.Gender Gender { get; set; }
        //Get/Set age
        public uint Age { get; set; }
        //Get/Set list of roles
        public List<Common.Role> Roles { get; set; }
        #endregion
    }
}
