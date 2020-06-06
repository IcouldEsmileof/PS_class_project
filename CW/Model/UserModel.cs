using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Interfaces;
using UserLogin;

namespace CW.Model
{
    public class UserModel
    {
        private readonly IUserController _controller;

        public UserModel(IUserController contr)
        {
            _controller = contr;
        }

        public bool ShowMainMenu(User user)
        {
            _controller.ShowUser("Здравей " + user.Username + "\n");
            if (user.ActiveTo != null && (user.Role == (int) UserRole.Admin && user.ActiveTo.Value.CompareTo(DateTime.Now)>0))
            {
                return true;
            }
            else
            {
                _controller.AppendToShowUser("Нямате достъп.\nТрябва да сте админ, ако искате да използванете приложението.\n");
                return false;
            }
        }

        public List<string> GetRoles()
        {
            List<string> rolesList = new List<string>();
            for (int i = 0; i < (int) UserRole.Max; i++)
            {
                rolesList.Add(((UserRole)i).ToString());
            }

            return rolesList;
        }

        public bool ChangeRole(string username, string role)
        {
            UserRole r;
            if (!Enum.TryParse(role, out r))
            {
                _controller.AlertUser("Невалидна роля.");
                return false;
            }

            if (r >= UserRole.Max||(int)r<0)
            {
                _controller.AlertUser("Невалидна роля.");
                return false;
            }

            if (UserData.AssignUserRole(username, r))
            {
                return true;
            }
            else
            {
                _controller.AlertUser("Потребителят не беше открит.");
                return false;
            }
            
        }

        public string GetActiveToMenu()
        {
            return "Въведете потребителско име.\n" +
                   "Въведете новата дата във формата дд.мм.гггг\n";
        }

        public bool ChangeActiveTo(string username, string activeTo)
        {
            try
            {
                int[] d = activeTo.Split('.').ToList().ConvertAll(Convert.ToInt32).ToArray();
                if (d.Length == 3)
                {
                    DateTime date;
                    try
                    {
                        date = new DateTime(d[2], d[1], d[0]);
                        if (date.Date <= DateTime.Now.Date)
                        {
                            _controller.AlertUser("Невалидна дата.");
                            return false;
                        }
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        _controller.AlertUser("Невалидна дата.");
                        return false;
                    }

                    if (UserData.SetUserActiveTo(username, date))
                    {
                        return true; 
                    }
                    _controller.AlertUser("Потребителят не беше открит.");
                    return false;
                }
                _controller.AlertUser("Невалидна дата.");
                return false;
            }
            catch (FormatException)
            {
                _controller.AlertUser("Невалидна дата.\nДатата трябва да е във формата дд.мм.гггг");
                return false;
            }
        }

        public void ShowUserList()
        {
            UserData.TestUsers.ForEach(u => _controller.AppendToShowUser(u + "\n"));
        }

        public void ShowLogs()
        {
            try
            {
                _controller.ShowUser("");
                var reader = new StreamReader(Logger.FileName);
                for (; !reader.EndOfStream;)
                {
                    _controller.AppendToShowUser(reader.ReadLine() + "\n");
                }

                reader.Close();
            }
            catch (IOException)
            {
                _controller.AlertUser("Възникна грешка при четенето на файла.");
            }
        }

        public void ShowCurrentActivity()
        {
            _controller.AppendToShowUser(Logger.GetCurrentSessionActivities());
        }

        [Obsolete("Method is deprecated, please don't use it with this CW.UserModel.",true)]
        public IUserController.ToExec ProcessMainInput(string input)
        {
            if (int.TryParse(input, out var result))
            {
                switch (result)
                {
                    case 0:
                        return _controller.Close;
                    case 1:
                        return _controller.ChangeUserRole;
                    case 2:
                        return _controller.ChangeUserActiveTo;
                    case 3:
                        return _controller.ShowUsersList;
                    case 4:
                        return _controller.ShowLogs;
                    case 5:
                        return _controller.ShowCurrentActivity;
                    default:
                        _controller.AlertUser("Invalid input");
                        break;
                }
            }
            else
            {
                _controller.AlertUser("Invalid input");
            }

            return null;
        }
    }
}