using DVDL_DataAccess;
using System.Data;

namespace DVDL_Business
{
    public class clsUsers
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int UserID { set; get; }
        public int PersonID { set; get; }
        public string UserName { set; get; }
        public string Password { set; get; }
        public bool IsActive { set; get; }
        private clsUsers(int UserID, int PersonID, string UserName, string Password, bool IsActive)
        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.UserName = UserName;
            this.Password = Password;
            this.IsActive = IsActive;
            Mode = enMode.Update;
        }
        public clsUsers()
        {
            this.UserID = -1;
            this.PersonID = -1;
            this.UserName = "";
            this.Password = "";
            this.IsActive = false;
            Mode = enMode.AddNew;
        }

        public static clsUsers Find(int usersID)
        {
            int PersonID = -1;
            string UserName = "";
            string Password = "";
            bool IsActive = false;

            if (clsUsersData.GetusersInfoByID(
                usersID,
            ref PersonID,
            ref UserName,
            ref Password,
            ref IsActive
             ))
                return new clsUsers(
                usersID,
                PersonID,
                UserName,
                Password,
                IsActive
                );
            else
                return null;
        }

        public static clsUsers Find(string UserName)
        {
            int userID = -1;
            int PersonID = -1;
            string Password = null;
            bool IsActive = false;

            if (clsUsersData.GetUserByUserName(
              ref userID,
            ref PersonID,
            ref UserName,
            ref Password,
            ref IsActive
             ))
                return new clsUsers(
                userID,
                PersonID,
                UserName,
                Password,
                IsActive
                );
            else
                return null;
        }



        private bool _AddNewUser()
        {
            this.UserID = clsUsersData.AddNewUser(this.PersonID, this.UserName, this.Password, this.IsActive);
            return (this.UserID != -1);
        }

        private bool _Updateusers()
        {
            return clsUsersData.Updateusers(this.UserID, this.PersonID, this.UserName, this.Password, this.IsActive);
        }

        public static bool Deleteusers(int ID)
        {
            return clsUsersData.DeleteUser(ID);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _Updateusers();

            }
            return false;
        }


        //public static bool isusersExist(int ID)
        //{
        //    return clsusersData.IsusersExist(ID);
        //}
        static public DataTable GetAllusers()
        {
            return clsUsersData.GetAllUsers();
        }
    }
}
