﻿using System;
using System.IO;
using System.Linq;
using CW.Controller;

namespace CW.Model
{
    public class UserModel
    {
        private readonly UserController _controller;

        public UserModel(UserController contr)
        {
            _controller = contr;
        }

        public bool ShowMainMenu(User user)
        {
            if (user.Role == (int) UserRole.Admin)
            {
                _controller.ShowUser("Изберте опция:\n" +
                                     "0: Изход\n" +
                                     "1: Промяна на роля на потребител\n" +
                                     "2: Промяна на активност на потребител\n" +
                                     "3: Списък на потребителите\n" +
                                     "4: Преглед на лог активност\n" +
                                     "5: Преглед на текуща активност\n");
                return true;
            }
            else
            {
                _controller.ShowUser("Нямате достъп");
                return false;
            }
        }

        public string GetRoleMenu()
        {
            return "0: Anonymous\n" +
                   "1: Admin\n" +
                   "2: Inspector\n" +
                   "3: Professor\n" +
                   "4: Student\n" +
                   "Въведете потребителско име и номера на новата роля в полетата по долу.\n";
        }

        public bool ChangeRole(string username, string role)
        {
            if (!Enum.TryParse(role, out UserRole r))
            {
                switch (role)
                {
                    case "Anonymous":
                    case "0":
                        r = UserRole.Anonymous;
                        break;
                    case "Admin":
                    case "1":
                        r = UserRole.Admin;
                        break;
                    case "Inspector":
                    case "2":
                        r = UserRole.Inspector;
                        break;
                    case "Professor":
                    case "3":
                        r = UserRole.Professor;
                        break;
                    case "Student":
                    case "4":
                        r = UserRole.Student;
                        break;
                    default:
                        _controller.AlertUser("Невалидна роля.");
                        return false;
                }
            }
            else if (r >= UserRole.Max)
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
            _controller.ShowUser(Logger.GetCurrentSessionActivities());
        }

        public UserController.ToExec ProcessMainInput(string input)
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