using System;
using System.Collections.Generic;

namespace CW_less_intuitive_UI.Model.UserLoginStuf
{
    public static class UserData
    {
        private static List<User> _testUsers;

        public static List<User> TestUsers
        {
            get
            {
                ResetTestUserData();
                return _testUsers;
            }
            set { }
        }

        private static void ResetTestUserData()
        {
            if (_testUsers == null)
            {
                _testUsers = new List<User>();
                _testUsers.Add(new User());
                _testUsers[0].FakNum = "123";
                _testUsers[0].Password = "admin007";
                _testUsers[0].Username = "admin007";
                _testUsers[0].Role = (int) UserRole.Admin;
                _testUsers[0].Created = DateTime.Now;
                _testUsers[0].ActiveTo = DateTime.MaxValue;
                _testUsers.Add(new User());
                _testUsers[1].FakNum = "122";
                _testUsers[1].Password = "student1";
                _testUsers[1].Username = "student1";
                _testUsers[1].Role = (int) UserRole.Student;
                _testUsers[1].Created = DateTime.Now;
                _testUsers[1].ActiveTo = DateTime.MaxValue;
                _testUsers.Add(new User());
                _testUsers[2].FakNum = "121";
                _testUsers[2].Password = "student2";
                _testUsers[2].Username = "student2";
                _testUsers[2].Role = (int) UserRole.Student;
                _testUsers[2].Created = DateTime.Now;
                _testUsers[2].ActiveTo = DateTime.MaxValue;
            }
        }

        public static User IsUserPassCorrect(string username, string pass)
        {
            foreach (User u in UserData.TestUsers)
            {
                if (u.Username.Equals(username) && u.Password.Equals(pass))
                {
                    return u;
                }
            }

            return null;
        }

        public static bool SetUserActiveTo(string uname, DateTime activeTo)
        {
            foreach (var usr in UserData.TestUsers)
            {
                if (usr.Username.Equals(uname))
                {
                    usr.ActiveTo = activeTo;
                    Logger.LogActivity("Успешна промяна на активност на потребител с име " + usr.Username);
                    return true;
                }
            }

            return false;
        }

        public static bool AssignUserRole(string uname, UserRole role)
        {
            foreach (var usr in UserData.TestUsers)
            {
                if (usr.Username.Equals(uname))
                {
                    usr.Role = (int) role;
                    Logger.LogActivity("Успешна промяна на роля на потребител с име " + usr.Username);
                    return true;
                }
            }

            return false;
        }
    }
}