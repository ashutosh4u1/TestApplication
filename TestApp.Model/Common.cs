namespace TestApp.Model
{
    public class Common
    {
        #region "Enumeration"
        //Enumeration for Gender
        public enum Gender
        {
            Male = 1,
            Female = 2
        }

        //Enumeration for Role
        public enum Role
        {
            Manager=1,
            Employee = 2,
            Admin=3
        }

        //Enumeration for Condition
        public enum Condition
        {
            LessThan = 1,
            LessThanOrEqualTo = 2,
            MoreThan = 3,
            MoreThanOrEqualTo = 4
        }
        #endregion
    }
}
