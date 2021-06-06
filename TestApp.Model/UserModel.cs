//Class represents the model class for User
#region "References"
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#endregion

namespace TestApp.Model
{
    public class UserModel
    {
        #region "Properties"
        //Get/Set id
        public int Id { get; set; }

        //Get/Set first name
        [Required(ErrorMessage ="First name is required")]
        [StringLength(15)]
        public string FirstName { get; set; }

        //Get/Set last name
        [Required(ErrorMessage ="Last name is required")]
        [StringLength(15)]
        public string LastName { get; set; }

        //Get/Set gender
        [Required(ErrorMessage ="Gender is required")]
        public Common.Gender Gender { get; set; }

        //Get/Set age
        [Required(ErrorMessage ="Age is required")]
        [Range(1, uint.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
        public uint Age { get; set; }

        //Get/Set list of roles
        [Required(ErrorMessage = "Atleast one role is required")]
        public List<Common.Role> Roles { get; set; }
        #endregion
    }
}
