namespace DVDL_Business.Global
{
    public class LoginSystem
    {


        public static bool Login(string UserName, string Password)
        {

            //clsUsers User = clsUsers.Find(UserName, Password);

            //if (User != null)
            //{
            //    //global
            //    clsGlobal.CurrentUser = User;

            //    if (User.Password == Password && User.UserName == UserName)
            //    {
            //        return (User.IsActive);
            //    }
            //}

            return false;
        }

        //new user
        public bool Register()
        {
            return false;
        }


    }
}
