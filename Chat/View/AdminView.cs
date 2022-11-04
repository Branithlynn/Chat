using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.Tool;

namespace Chat.View
{
    class AdminView
    {
        public void Show()
        {
            while (true)
            {
                AdminViewEnum choice = RenderMenu();

                try
                {
                    switch (choice)
                    {
                        case AdminViewEnum.UserManagement:
                            {
                                UserManagementView userManagementView = new UserManagementView();
                                userManagementView.Show();
                                break;
                            }
                        case AdminViewEnum.Exit:
                            {
                                return;
                            }
                        case AdminViewEnum.UserView:
                            {
                                UserView userView = new UserView();
                                userView.Show();
                                break;
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

        private AdminViewEnum RenderMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Admin View: ");
                Console.WriteLine("[U]ser Management View");
                Console.WriteLine("User [V]iew");
                Console.WriteLine("E[x]it");

                string choice = Console.ReadLine();
                switch (choice.ToUpper())
                {
                    case "U":
                        {
                            return AdminViewEnum.UserManagement;
                        }
                    case "V":
                        {
                            return AdminViewEnum.UserView;
                        }
                    case "X":
                        {
                            return AdminViewEnum.Exit;
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

    }
}
