using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.Tool;
using Chat.Repository;
using Chat.Service;
using Chat.Entity;
using System.Threading;

namespace Chat.View
{
    class UserView
    {
        
       
        public void Show()
        {
            while (true)
            {
                UserViewEnum choice = RenderMenu();

                try
                {
                    switch (choice)
                    {
                        case UserViewEnum.Friends:
                            {
                                FriendsView friendsView = new FriendsView();
                                friendsView.Show();
                                break;
                            }
                        case UserViewEnum.Chat:
                            {
                                ChatView chatView = new ChatView();
                                chatView.SelectPerson();
                                break;
                            }
                        case UserViewEnum.Exit:
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

        private UserViewEnum RenderMenu()
        {
            while (true)
            {

                Console.Clear();
                Console.WriteLine("UserView :");
                Console.WriteLine("[C]hat");
                Console.WriteLine("[F]riends");
                Console.WriteLine("E[x]it");
              

                string choice = Console.ReadLine();
                switch (choice.ToUpper())
                {

                    case "F":
                        {
                            return UserViewEnum.Friends;
                        }
                    case "X":
                        {
                            return UserViewEnum.Exit;
                        }
                    case "C":
                        {
                            return UserViewEnum.Chat;
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
