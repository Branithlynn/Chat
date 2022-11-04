using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chat.Tool;
using Chat.Entity;
using Chat.Repository;
using Chat.Service;
using System.Threading.Tasks;

namespace Chat.View
{
    class FriendRequestView
    {
        public void Show()
        {
            while (true)
            {
                FriendRequest choice = RenderMenu();

                try
                {
                    switch (choice)
                    {
                        case FriendRequest.Accept:
                            {
                                Accept();
                                break;
                            }
                        case FriendRequest.Reject:
                            {
                                Reject();
                                break;
                            }
                        case FriendRequest.Exit:
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

        private FriendRequest RenderMenu()
        {
            while (true)
            {

                Console.Clear();
                IncomingRequests();
                Console.WriteLine("[A]ccept Request");
                Console.WriteLine("[R]eject Request");
                Console.WriteLine("E[x]it");


                string choice = Console.ReadLine();
                switch (choice.ToUpper())
                {

                    case "A":
                        {
                        
                            return FriendRequest.Accept;
                        }
                    case "R":
                        {
                         
                            return FriendRequest.Reject;
                        }
                    case "X":
                        {
                            return FriendRequest.Exit;
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
        public void Accept()
        {
            Console.Clear();
            IncomingRequests();
            Console.WriteLine();
            Console.Write("Request Id");
            int id = Convert.ToInt32(Console.ReadLine());

            FriendsRepository friendReporsitory = new FriendsRepository("friends.txt");
            friendReporsitory.AcceptFriend(id);
            Console.WriteLine("Accepted");
            Console.ReadKey(true);
            return;
        }
        public void Reject()
        {
            Console.Clear();
            IncomingRequests();
            Console.WriteLine();
            Console.Write("Request Id");
            int id = Convert.ToInt32(Console.ReadLine());

            FriendsRepository friendReporsitory = new FriendsRepository("friends.txt");
            friendReporsitory.RejectFriend(id);
            Console.WriteLine("Friend Declined");
            Console.ReadKey(true);
            return;

        }
        public void IncomingRequests()
        {
            Console.Clear();
            FriendsRepository friendsRepository = new FriendsRepository("friends.txt");
            List<Friends> friends = friendsRepository.IncomingRequests();
            UserRepository userRepository = new UserRepository("users.txt");
            foreach (Friends friend in friends)
            {
                User user = userRepository.GetById(friend.RequesterId);
                Console.WriteLine(friend.SentDate+"  "+"("+friend.RequesterId+")" + " "+user.Username +" ("+ Enum.GetName(typeof(FriendStatusEnum), friend.Status)+")");
            }
            
            return;
        }
    }
}
