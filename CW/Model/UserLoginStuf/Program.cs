﻿using System;
 using System.Linq;

 namespace CW
{
    internal static class Program
    {
        private static void Printer(string mess)
        {
            Console.WriteLine("!!!" + mess + "!!!");
        }

        /*public static void Main(string uname, string pass)
        {
            LoginValidation lv = new LoginValidation(uname, pass, Printer);
            User u;
            if (lv.ValidateUserInput(out u))
            {
                Console.WriteLine(u.Username + " "
                                             + u.Password + " "
                                             + u.FakNum + " "
                                             + u.Role);
                switch (LoginValidation.CurrentRole)
                {
                    case (int) UserRole.Admin:
                        Console.WriteLine("Admin");
                        break;
                    case (int) UserRole.Anonymous:
                        Console.WriteLine("Anonymous");
                        break;
                    case (int) UserRole.Inspector:
                        Console.WriteLine("Inspector");
                        break;
                    case (int) UserRole.Professor:
                        Console.WriteLine("Professor");
                        break;
                    case (int) UserRole.Student:
                        Console.WriteLine("Student");
                        break;
                    default:
                        Console.WriteLine("Incorrect role");
                        break;
                }

                if (u.Role == (int) UserRole.Admin)
                {
                    for (bool b = true; b;)
                    {
                        Console.WriteLine("Изберте опция:\n" +
                                          "0: Изход\n" +
                                          "1: Промяна на роля на потребител\n" +
                                          "2: Промяна на активност на потребител\n" +
                                          "3: Списък на потребителите\n" +
                                          "4: Преглед на лог активност\n" +
                                          "5: Преглед на текуща активност");
                        string input = Console.ReadLine();
                        string user;
                        switch (int.Parse(input))
                        {
                            case 0:
                                b = false;
                                break;
                            case 1:
                                Console.WriteLine("Въведете потребителско име:");
                                user = Console.ReadLine();
                                Console.Write("0: Anonymous\n" +
                                              "1: Admin\n" +
                                              "2: Inspector\n" +
                                              "3: Professor\n" +
                                              "4: Student\n" +
                                              "Нова роля:");
                                input = Console.ReadLine();
                                UserRole role;
                                if (!UserRole.TryParse(input, out role))
                                {
                                    switch (input)
                                    {
                                        case "Anonymous":
                                            role = UserRole.Anonymous;
                                            break;
                                        case "Admin":
                                            role = UserRole.Admin;
                                            break;
                                        case "Inspector":
                                            role = UserRole.Inspector;
                                            break;
                                        case "Professor":
                                            role = UserRole.Professor;
                                            break;
                                        case "Student":
                                            role = UserRole.Student;
                                            break;
                                        default:
                                            Console.WriteLine("Невалидна роля.");
                                            break;
                                    }
                                }

                                UserData.AssignUserRole(user, role);
                                break;
                            case 2:
                                Console.WriteLine("Въведете потребителско име:");
                                user = Console.ReadLine();
                                Console.WriteLine("Въведете новата дата във формата дд.мм.гггг:");
                                string date = Console.ReadLine();
                                int[] d = date.Split('.').ToList().ConvertAll(Convert.ToInt32).ToArray();
                                if (d.Length == 3)
                                {
                                    UserData.SetUserActiveTo(user, new DateTime(d[2], d[1], d[0]));
                                }
                                else
                                {
                                    Console.WriteLine("Невалидна дата.");
                                }

                                break;
                            case 3:
                                UserData.TestUsers.ForEach(Console.WriteLine);
                                break;
                            case 4:
                                StreamReader reader = new StreamReader(Logger.FileName);
                                for (; !reader.EndOfStream;)
                                {
                                    Console.WriteLine(reader.ReadLine());
                                }

                                reader.Close();
                                break;
                            case 5:
                                Console.WriteLine(Logger.GetCurrentSessionActivities());
                                break;
                            default:
                                Console.WriteLine("Невалиден избор.");
                                break;
                        }
                    }
                }
            }


            // DateTimeExploration();
        }*/

        private static void DateTimeExploration()
        {
            DateTime dt = new DateTime(2017, 9, 15, 10, 30, 0);
            Console.WriteLine(dt.Hour);
            Console.WriteLine(DateTime.Now.Hour);
            Console.WriteLine(DateTime.Now.AddHours(12));
            string date = Console.ReadLine();
            int[] d = date.Split('.').ToList().ConvertAll(Convert.ToInt32).ToArray();
            if (d.Length == 3)
            {
                Console.WriteLine(value: new DateTime(d[2], d[1], d[0]));
            }
            else
            {
                Console.WriteLine("Invalid date");
            }
        }
    }
}