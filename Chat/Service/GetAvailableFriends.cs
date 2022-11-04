using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.Entity;
using Chat.Repository;


namespace Chat.Service
{
    class GetAvailableFriends
    {
        public List<User> GetAvailableUsers()
        {
            FriendsRepository friendsRepo = new FriendsRepository("friends.txt");
            UserRepository usersRepo = new UserRepository("users.txt");

            List<User> availableFirends = new List<User>();
            List<User> users = usersRepo.GetAll();

            foreach (User user in users)
            {
                if (!friendsRepo.IsFriend(user.Id)&&user.Id!=AuthenticationService.LoggedUser.Id)
                {
                    availableFirends.Add(user);
                }
            }

            return availableFirends;
        }
    }
}
