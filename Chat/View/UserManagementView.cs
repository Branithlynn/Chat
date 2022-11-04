using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.Entity;
using Chat.Repository;
using Chat.Service;
using Chat.Tool;

namespace Chat.View
{
    class UserManagementView
    {
        public void Show()
        {
            while (true)
            {
                UserManagementEnum choice = RenderMenu();

                try
                {
                    switch (choice)
                    {
                        case UserManagementEnum.Add:
                            {
                                Add();
                                break;
                            }
                        case UserManagementEnum.Delete:
                            {
                                Delete();
                                break;
                            }
                        case UserManagementEnum.Update:
                            {
                                Update();
                                break;
                            }
                        case UserManagementEnum.SeeAll:
                            {
                                GetAll();
                                break;
                            }
                        case UserManagementEnum.Exit:
                            {
                                return;
                            }
                    }
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine(ex.Message);
                    Console.ReadKey(true);
                }
            }
        }

        private UserManagementEnum RenderMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Users :");    
                Console.WriteLine("[A]dd User");
                Console.WriteLine("[D]elete User");
                Console.WriteLine("[U]pdate");
                Console.WriteLine("[S]ee all");
                Console.WriteLine("E[x]it");

                string choice = Console.ReadLine();
                switch (choice.ToUpper())
                {
                    case "A":
                        {
                            return UserManagementEnum.Add;
                        }
                    case "D":
                        {
                            return UserManagementEnum.Delete;
                        }
                    case "U":
                        {
                            return UserManagementEnum.Update;
                        }
                    case "X":
                        {
                            return UserManagementEnum.Exit;
                        }
                    case "S":
                        {
                            return UserManagementEnum.SeeAll;
                        }
                    default:
                        {
                            Console.WriteLine("Invalid choice.");
                            Console.ReadKey(true);
                            break;
                        }
                }
            }
        }


        private void Add()
        {
            Console.Clear();
           
            User user = new User();
           
            Console.Write("Username: ");
            user.Username = Console.ReadLine();
            Console.Write("Password: ");
            user.Password = Console.ReadLine();
            Console.Write("IsAdmin: ");
            user.IsAdmin = Convert.ToBoolean(Console.ReadLine());

            UserRepository userRepository = new UserRepository("users.txt");
            userRepository.Save(user);
            Console.WriteLine("The user has been saved.");
            Console.ReadKey(true);
        }
        private void GetAll()
        {
            Console.Clear();

            UserRepository usersRepository = new UserRepository("users.txt");
            List<User> users = usersRepository.GetAll();

            foreach (User user in users)
            {
                Console.WriteLine("ID:" + user.Id);
                Console.WriteLine("Username:" + user.Username);
                Console.WriteLine("Password:" + user.Password);
                Console.WriteLine("Is Admin:" + user.IsAdmin);

                Console.WriteLine("------------------------------------------");
            }

            Console.ReadKey(true);
        }
        private void Delete()
        {
            Console.Clear();
            Console.WriteLine("User Deletion:");
            Console.Write("User Id:");
            int id = Convert.ToInt32(Console.ReadLine());
            UserRepository userRepository = new UserRepository("users.txt");
            User user = userRepository.GetById(id);

            if (user == null)
            {
                Console.WriteLine("Doesnt exist.");
            }
            else
            {
                userRepository.Delete(user);
                Console.WriteLine("Deleted successfully");
            }

            Console.ReadKey(true);
        }
        private void Update()
        {
            Console.Clear();
            string input;
            Console.Write("User ID: ");
            int id = Convert.ToInt32(Console.ReadLine());

            UserRepository usersRepository = new UserRepository("users.txt");
            User user = usersRepository.GetById(id);

            if (user == null)
            {
                Console.Clear();
                Console.WriteLine("User not found.");
                Console.ReadKey(true);
            }
            else
            {
                Console.WriteLine("Current User information");
                Console.WriteLine("ID:" + user.Id);
                Console.WriteLine("Username:" + user.Username);
                Console.WriteLine("Password:" + user.Password);
                Console.WriteLine("Is Admin:" + user.IsAdmin);
                Console.WriteLine("--------------------------------");
                Console.WriteLine("Enter new User information");

                Console.Write("New Username: ");
                input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                {
                    user.Username = input;
                }
                Console.Write("New Password: ");
                input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                {
                    user.Password = input;
                }

                Console.Write("Is Admin: ");
                bool isadmin = Convert.ToBoolean(Console.ReadLine());
                string isAdmin = Convert.ToString(isadmin);
                if (!string.IsNullOrEmpty(isAdmin))
                {
                    user.IsAdmin = isadmin;
                }


                usersRepository.Save(user);
                Console.WriteLine("User Changed");
            }
          

            
            Console.ReadKey(true);
        }
    }
}
