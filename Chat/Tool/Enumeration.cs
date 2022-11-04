using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Tool
{
    public enum UserManagementEnum
    {
        Delete = 1,
        Add = 2,
        Update = 3,
        Exit = 4,
        SeeAll =5
    }
    public enum AdminViewEnum
    {
        UserManagement = 1,
        Exit = 2,
        UserView=3
    }
    public enum UserViewEnum
    {
        Chat = 1,
        Friends=2,
        Exit=3
    }
    public enum FriendStatusEnum
    {
        Pending = 1,
        Accepted = 2,
        Declined =3,
        Blocked = 4
    }
    public enum FriendsViewEnum
    {
        GetAll =1,
        AddFriend=2,
        Unfriend=3,
        FriendRequest=4,
        GetAvailableFriends=5,
        Exit=6
    }
    public enum FriendRequest
    {
        Accept=1,
        Reject=2,
        Exit=3
    }
    
}
