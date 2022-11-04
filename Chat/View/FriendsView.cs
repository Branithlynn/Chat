using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.Tool;
using Chat.Entity;
using Chat.Repository;
using Chat.Service;

namespace Chat.View
{
    class FriendsView
    {
        public void Show()
        {
            while (true)
            {
                FriendsViewEnum choice = RenderMenu();

                try
                {
                    switch (choice)
                    {
                        case FriendsViewEnum.GetAll:
                            {
                                SeeAllFriends();
                                Console.ReadKey(true);
                                break;
                            }
                        case FriendsViewEnum.FriendRequest:
                            {
                                FriendRequestView friendRequestView = new FriendRequestView();
                                friendRequestView.Show();
                                break;
                            }
                        case FriendsViewEnum.Unfriend:
                            {
                                Unfriend();
                                break;
                            }
                        case FriendsViewEnum.AddFriend:
                            {
                                AddFriend();
                                break;
                            }
                        case FriendsViewEnum.Exit:
                            {
                                return;
                            }
                        case FriendsViewEnum.GetAvailableFriends:
                            {
                                GetAvailableFriends();
                                Console.ReadKey(true);
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

        private FriendsViewEnum RenderMenu()
        {
            while (true)
            {

                Console.Clear();
                Console.WriteLine("FriendView :");
                Console.WriteLine("[G]et all ");
                Console.WriteLine("[A]dd Friend");
                Console.WriteLine("[U]nfriend");
                Console.WriteLine("Friend [R]equest");
                Console.WriteLine("Get A[V]ailable friends");
                Console.WriteLine("E[x]it");


                string choice = Console.ReadLine();
                switch (choice.ToUpper())
                {
                    case "V":
                        {
                            return FriendsViewEnum.GetAvailableFriends;
                        }
                    case "G":
                        {
                            return FriendsViewEnum.GetAll;
                        }
                    case "X":
                        {
                            return FriendsViewEnum.Exit;
                        }
                    case "A":
                        {
                            return FriendsViewEnum.AddFriend;
                        }
                    case "U":
                        {
                            return FriendsViewEnum.Unfriend;
                        }
                    case "R":
                        {
                            return FriendsViewEnum.FriendRequest;
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

        public void GetAvailableFriends()
        {
            Console.Clear();
            Console.WriteLine("Available Friends");
            Console.WriteLine();
            GetAvailableFriends getAvailableFriends = new GetAvailableFriends();
            List<User> availableFriends = getAvailableFriends.GetAvailableUsers();

            foreach (User user in availableFriends)
            {
                Console.WriteLine("("+user.Id+")"+" "+user.Username);
            }

           
        }

        public void Unfriend()
        {
            FriendsRepository friendsRepository = new FriendsRepository("friends.txt");
            SeeAllFriends();
            Console.WriteLine();
            Console.Write("Friend Id: ");
            int id = Convert.ToInt32(Console.ReadLine());
            friendsRepository.Delete(id);
            Console.WriteLine("Friend Deleted");
            Console.ReadKey(true);
        }

        public void AddFriend()
        {
            Console.Clear();
            GetAvailableFriends();
            Console.WriteLine("Add Friend:");
            Console.Write("User ID:");
            int id = Convert.ToInt32(Console.ReadLine());
            Friends friend = new Friends();
            FriendsRepository friendsRepository = new FriendsRepository("friends.txt");
            friend.Id = friendsRepository.GetNextId();
            friend.RequesterId = AuthenticationService.LoggedUser.Id;
            friend.RecepientId = id;
            friend.Status = Convert.ToInt32(FriendStatusEnum.Pending);
            friendsRepository.AddFriend(friend);
            Console.WriteLine("Friend request sent");
            Console.ReadKey(true);
        }
        public void RemoveFriend()
        {
            SeeAllFriends();
            Console.WriteLine("Remove Friend:");
            Console.Write("User ID:");
            int id = Convert.ToInt32(Console.ReadLine());
            FriendsRepository friendsRepository = new FriendsRepository("friends.txt");

            friendsRepository.Delete(id);
            Console.WriteLine("Friend deleted.");
            Console.ReadKey(true);
        }
        public void SeeAllFriends()
        {
            FriendsRepository friendsRepository = new FriendsRepository("friends.txt");
            List<Friends> FriendIds = friendsRepository.GetAllFriends(AuthenticationService.LoggedUser.Id);
            UserRepository userRepository = new UserRepository("users.txt");
            Console.WriteLine("FriendList:");
            foreach (Friends friend in FriendIds)
            {
                if (friend.RequesterId == AuthenticationService.LoggedUser.Id&&friend.Status!=Convert.ToInt32(FriendStatusEnum.Declined))
                {
                    User item = userRepository.GetById(friend.RecepientId);
                    Console.Write(friend.RecepientId + ":"+item.Username+" - " + Enum.GetName(typeof(FriendStatusEnum), friend.Status));
                    Console.WriteLine(); 
                }
                else if (friend.RecepientId == AuthenticationService.LoggedUser.Id && friend.Status != Convert.ToInt32(FriendStatusEnum.Declined))
                {
                    User item = userRepository.GetById(friend.RequesterId);
                    Console.Write(friend.RequesterId + ":" + item.Username + " - " + Enum.GetName(typeof(FriendStatusEnum), friend.Status));
                    Console.WriteLine(); 
                }

            }
        }

    }
}
