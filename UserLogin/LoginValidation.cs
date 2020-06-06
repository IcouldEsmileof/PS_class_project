using System;
using System.Collections.Generic;
using System.Threading;

namespace UserLogin
{
    public class LoginValidation
    {
        class mTuple<T1, T2>
        {
            public T1 Item1 { get; set; }
            public T2 Item2 { get; set; }


            public mTuple(T1 item1, T2 item2)
            {
                Item1 = item1;
                Item2 = item2;
            }
        }

        public static int CurrentRole { get; private set; }
        public static string CurrentUserUsername { get; private set; }
        private string _userName, _pass, _errorMess;
        private ActionOnError _actionOnError;

        private static Dictionary<string, mTuple<int, DateTime>> _invalidLoginCount =
            new Dictionary<string, mTuple<int, DateTime>>()
            {
                {"no_pass", new mTuple<int, DateTime>(0, DateTime.Now)},
                {"no_uname", new mTuple<int, DateTime>(0, DateTime.Now)},
                {"short_pass", new mTuple<int, DateTime>(0, DateTime.Now)},
                {"short_uname", new mTuple<int, DateTime>(0, DateTime.Now)},
                {"incorrect_login", new mTuple<int, DateTime>(0, DateTime.Now)}
            };

        private static TimeSpan _maxDuration = new TimeSpan(0, 2, 0);

        public delegate void ActionOnError(string errorMsg);
        //public static ReaderWriterLock m=new ReaderWriterLock();

        public LoginValidation(string userName, string pass, ActionOnError actionOnError)
        {
            _userName = userName;
            _pass = pass;
            _actionOnError = actionOnError;
        }

        private void checker(string s)
        {
            DateTime now = DateTime.Now;
            if (_invalidLoginCount[s].Item1 >= 3 && new TimeSpan(now.Hour - _invalidLoginCount[s].Item2.Hour,
                    now.Minute - _invalidLoginCount[s].Item2.Minute, now.Second - _invalidLoginCount[s].Item2.Second) <=
                _maxDuration)
            {
                Logger.LogActivity("Твърде много опити за логване");
                Console.WriteLine("Твърде много опити за логване.\nОпитайте отново след 30 секунди.");
                Thread.Sleep(30000);
                foreach (var keyValuePair in _invalidLoginCount.Keys)
                {
                    _invalidLoginCount[keyValuePair].Item1 = 0;
                    _invalidLoginCount[keyValuePair].Item2 = now;
                }
            }
            else if (new TimeSpan(now.Hour - _invalidLoginCount[s].Item2.Hour,
                now.Minute - _invalidLoginCount[s].Item2.Minute,
                now.Second - _invalidLoginCount[s].Item2.Second) >= _maxDuration)
            {
                foreach (var keyValuePair in _invalidLoginCount.Keys)
                {
                    _invalidLoginCount[keyValuePair].Item1 = 0;
                    _invalidLoginCount[keyValuePair].Item2 = now;
                }
            }
        }

        private void setErrorMess(string mes)
        {
            _errorMess = mes;
            _actionOnError(_errorMess);
        }

        public bool ValidateUserInput(out User u)
        {
            u = null;
            CurrentRole = (int) UserRole.Anonymous;
            if (_userName.Length < 5)
            {
                if (_userName == string.Empty)
                {
                    setErrorMess("Не е посочено потребителско име.");
                    _invalidLoginCount["no_uname"].Item1++;
                    checker("no_uname");
                }
                else
                {
                    setErrorMess("Потребителското име е твърде кратко.");
                    _invalidLoginCount["short_uname"].Item1++;
                    checker("short_uname");
                }

                return false;
            }

            if (_pass.Length < 5)
            {
                if (_pass == string.Empty)
                {
                    setErrorMess("Не е посочена парола.");
                    _invalidLoginCount["no_pass"].Item1++;
                    checker("no_pass");
                }
                else
                {
                    setErrorMess("Паролата е твърде къса.");
                    _invalidLoginCount["short_pass"].Item1++;
                    checker("short_pass");
                }

                return false;
            }

            if ((u = UserData.IsUserPassCorrect(_userName, _pass)) == null)
            {
                setErrorMess("Потребителят не беше открит");
                _invalidLoginCount["incorrect_login"].Item1++;
                checker("incorrect_login");
                return false;
            }

            CurrentRole = u.Role;
            CurrentUserUsername = u.Username;
            Logger.LogActivity("Успешен Loggin");
            return true;
        }
    }
}