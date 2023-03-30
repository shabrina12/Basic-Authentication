using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics.Metrics;
using System.Runtime.Intrinsics.X86;
using System.Drawing;

namespace Tugas_4_Auth
{
    class Program2
    {
        static List<User> users = new List<User>();

        class User
        {
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string username { get; set; }
            public string password { get; set; }

            public User(string firstName, string lastName, string pswd)
            {
                first_name = firstName;
                last_name = lastName;
                password = pswd;
                username = $"{firstName[..3]}{lastName[..3]}";
            }
            public string GetUserData()
            {
                return $"Full Name : {first_name} {last_name} \n" +
                       $"User Name : {username} \n" +
                       $"Password  : {password}";
            }
        }

        //public static void Main(string[] args)
        //{
        //    Console.WriteLine(DateTime.Now);
        //    try
        //    {
        //        int choice;
        //        string confirm;

        //        do
        //        {
        //            displaymenu();
        //            Console.Write("Input (1-5): ");
        //            choice = int.Parse(Console.ReadLine());
        //            Console.Clear();
        //            switch (choice)
        //            {
        //                case 1:
        //                    add();
        //                    break;
        //                case 2:
        //                    show();
        //                    break;
        //                case 3:
        //                    //find(pengguna, usercount);
        //                    break;
        //                case 4:
        //                    //login(pengguna, usercount);
        //                    break;
        //                case 5:
        //                    Environment.Exit(0);
        //                    break;
        //                default:
        //                    Console.WriteLine("invalid input");
        //                    break;
        //            }
        //            Console.Write("Press y or Y to continue: ");
        //            confirm = Console.ReadLine().ToString();
        //            Console.Clear();
        //        } while (confirm == "y" || confirm == "Y");
        //    }
        //    catch (FormatException)
        //    {
        //        Console.WriteLine("Invalid input");
        //    }
        //    Console.ReadLine();
        //}

        static void displaymenu()
        {
            Console.WriteLine("== BASIC AUTHENTICATION ==");
            Console.WriteLine(" 1. Create User");
            Console.WriteLine(" 2. Show User");
            Console.WriteLine(" 3. Search User");
            Console.WriteLine(" 4. Login User");
            Console.WriteLine(" 5. Exit");
        }

        //ADD NEW USER
        public static void add()
        {
            Console.WriteLine("==  CREATE USER ==");

            Console.Write("Enter first name: ");
            var first_name = Console.ReadLine();

            Console.Write("Enter last name: ");
            var last_name = Console.ReadLine();

            Console.Write("Enter password: ");
            var password = Console.ReadLine();

            var user1 = new User(first_name, last_name, password);

            var hasNumber = new Regex(@"[0-9]+"); //must contain at least a number
            var hasUpperChar = new Regex(@"[A-Z]+"); //min one upper case letter
            var hasMinimum8Chars = new Regex(@".{8,}"); //min 8 characters long.

            var isValidated = hasNumber.IsMatch(user1.password) && hasUpperChar.IsMatch(user1.password) && hasMinimum8Chars.IsMatch(user1.password);

            if (isValidated)
            {
                if (users.Any(u => u.username.Contains(user1.username)))
                {
                    user1.username = $"{user1.first_name[..3]}" +
                                     $"{user1.last_name[..3]}" +
                                     $"{users.Count(u => u.username.ToLower().Contains(user1.username.ToLower()))}";

                }
                users.Add(user1);
            }
            else
            {
                Console.WriteLine("Password must consist of minimum 8 characters, at least one uppercase letter, and must consist a number: ");
            }
        }

        public static void show()
        {
            int choice2;
            var count = 0;
            foreach (var user in users)
            {
                Console.WriteLine("========= USER INFORMATION ===========");
                Console.WriteLine("Id : " + count++);
                Console.WriteLine(user.GetUserData());
                Console.WriteLine("=========================");
            }

            Console.WriteLine("Menu");
            Console.WriteLine("1. Edit User");
            Console.WriteLine("2. Delete User");
            Console.WriteLine("3. Back");
            Console.Write("Input (1-3): ");
            choice2 = int.Parse(Console.ReadLine());
            Console.Clear();

            switch (choice2)
            {
                case 1:
                    update();
                    break;
                case 2:
                    //delete();
                    break;
                case 3:
                    Console.Clear();
                    break;
                default:
                    Console.WriteLine("invalid input");
                    break;
            }
        }

        //UPDATE USER DATA
        static void update()
        {
            string uname;
            int column_index;
            Console.WriteLine("== UPDATE USER INFORMATION ==");
            Console.Write("Enter user's username: ");
            uname = Console.ReadLine().ToLower();

            var updateUname = users.Any(u => u.username.ToLower().Contains(uname));
            //Console.WriteLine(updateUname);

            if (updateUname)
            {
                Console.WriteLine("Which field you want to update(1-4)?: ");
                Console.WriteLine("1: First Name");
                Console.WriteLine("2: Last Name");
                Console.WriteLine("3: Password");
                Console.WriteLine("-------------------------------------------------------");

                column_index = int.Parse(Console.ReadLine());

                if (column_index == 1)
                {
                    Console.Write("Enter new first name: ");
                    var first_name = Console.ReadLine().ToString();
                }

                else if (column_index == 2)
                {
                    Console.Write("Enter new last name: ");
                    var last_name = Console.ReadLine().ToString();
                }
                else if (column_index == 3)
                {
                    Console.Write("Enter new password: ");
                    var password = Console.ReadLine().ToString();
                }
                else Console.WriteLine("Invalid column index");
            }
            else Console.WriteLine("User doesn't exits.");
        }

        //VIEWING ALL USER DATA
        //public static void viewall()
        //{            
        //    int choice2;

        //    Console.WriteLine("============== USER INFORMATION ================");
        //    Console.WriteLine();

        //    var count = 0;
        //    foreach (var user in users)
        //    {
        //        Console.WriteLine("Id : " + count++);
        //        Console.WriteLine(user.GetUserData());
        //        Console.WriteLine("=========================");
        //    }

        //    Console.WriteLine("Menu");
        //    Console.WriteLine("1. Edit User");
        //    Console.WriteLine("2. Delete User");
        //    Console.WriteLine("3. Back");
        //    Console.Write("Input (1-3): ");
        //    choice2 = int.Parse(Console.ReadLine());
        //    Console.Clear();

        //    switch (choice2)
        //    {
        //        case 1:
        //            update();
        //            break;
        //        case 2:
        //            delete();
        //            break;
        //        case 3:
        //            Console.Clear();
        //            break;
        //        default:
        //            Console.WriteLine("invalid input");
        //            break;
        //    }                        

        //    //UPDATE USER DATA
        //    static void update()
        //    {
        //        string uname;
        //        int column_index;
        //        Console.WriteLine("== UPDATE USER INFORMATION ==");
        //        Console.Write("Enter user's username: ");
        //        uname = Console.ReadLine().ToString();

        //        if (users.Any(u => u.username = uname))
        //        {
        //            Console.WriteLine("Which field you want to update(1-4)?: ");
        //            Console.WriteLine("1: First Name");
        //            Console.WriteLine("2: Last Name");
        //            Console.WriteLine("3: Password");
        //            Console.WriteLine("-------------------------------------------------------");

        //            column_index = int.Parse(Console.ReadLine());

        //                if (column_index == 1)
        //                {
        //                    Console.Write("Enter new first name: ");
        //                    var first_name = Console.ReadLine().ToString();
        //                }

        //                else if (column_index == 2)
        //                {
        //                    Console.Write("Enter new last name: ");
        //                    var last_name = Console.ReadLine().ToString();
        //                }
        //                else if (column_index == 3)
        //                {
        //                    Console.Write("Enter new password: ");
        //                    var password = Console.ReadLine().ToString();
        //                }
        //                else Console.WriteLine("Invalid column index");                                       
        //        }
        //        else Console.WriteLine("User doesn't exits.");
        //    }

        //    //DELETING DATA OF STUDENTS
        //    static void delete(user[] pengguna, ref int usercount)
        //    {
        //        string username;
        //        int index;
        //        if (usercount > 0)
        //        {
        //            Console.WriteLine("===  DELETING USER INFORMATION  ===");
        //            Console.Write("Enter user's username: ");
        //            username = Console.ReadLine();
        //            index = search(pengguna, username.ToString(), usercount);

        //            if ((index != -1) && (usercount != 0))
        //            {
        //                if (index == (usercount - 1))
        //                {

        //                    clean(pengguna, index);
        //                    --usercount;

        //                    Console.WriteLine("The record was deleted.");
        //                }
        //                else
        //                {
        //                    for (int i = index; i < usercount - 1; i++)
        //                    {
        //                        pengguna[i] = pengguna[i + 1];
        //                        clean(pengguna, usercount);
        //                        --usercount;
        //                    }
        //                }
        //            }
        //            else Console.WriteLine("User doesn't exist.");
        //        }
        //        else Console.WriteLine("No user record to delete");
        //    }

        //    static void clean(user[] pengguna, int index)
        //    {
        //        pengguna[index].first_name = null;
        //        pengguna[index].last_name = null;
        //        pengguna[index].username = null;
        //        pengguna[index].password = null;
        //    }
        //}

        ////SEARCHING RECORD OF USER BY USERNAME
        //static void find(user[] pengguna, int usercount)
        //{
        //    Console.WriteLine("== SEARCH USER ==");
        //    string username;
        //    Console.Write("Enter username: ");
        //    username = Console.ReadLine();

        //    int index = search(pengguna, username.ToString(), usercount);
        //    if (index != -1)
        //    {
        //        Console.WriteLine("First Name: " + pengguna[index].first_name);
        //        Console.WriteLine("Last Name: " + pengguna[index].last_name);
        //        Console.WriteLine("Username: " + pengguna[index].username);
        //        Console.WriteLine();
        //    }
        //    else Console.WriteLine("User doesn't exits.");
        //}

        ////UNTUK FINDING,DELETING,UPDATING THE DATA OF USER
        //static int search()
        //{
        //    var cari = Console.ReadLine();
        //    var filtered = users.Where(user => user.first_name.ToLower().Contains(cari.ToLower()) || user.last_name.ToLower().Contains(cari.ToLower()));

        //    foreach (var user in filtered)
        //    {
        //        Console.WriteLine("=========================");
        //        Console.WriteLine("Id : " + count++);
        //        Console.WriteLine(user.GetUserData());
        //        Console.WriteLine("=========================");
        //    }
        //}

        //static int searchAkun(user[] pengguna, string username, string password, int usercount)
        //{
        //    int found = -1;
        //    for (int i = 0; i < usercount && found == -1; i++)
        //    {
        //        if (pengguna[i].username == username && pengguna[i].password == password) found = i;

        //        else found = -1;
        //    }
        //    return found;
        //}

        ////LOGIN USER
        //static void login(user[] pengguna, int usercount)
        //{
        //    Console.WriteLine("== LOGIN USER ==");
        //    string username;
        //    string password;
        //    Console.Write("Enter username: ");
        //    username = Console.ReadLine();
        //    Console.Write("Enter password: ");
        //    password = Console.ReadLine();

        //    int index = searchAkun(pengguna, username.ToString(), password.ToString(), usercount);
        //    if (index != -1)
        //    {
        //        Console.WriteLine();
        //        Console.WriteLine("Selamat datang " + pengguna[index].first_name + " " + pengguna[index].last_name + " !");
        //        Console.WriteLine();
        //    }
        //    else Console.WriteLine("User doesn't exits.");
        //}

    }
}
