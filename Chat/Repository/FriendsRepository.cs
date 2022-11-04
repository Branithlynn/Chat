using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.Entity;
using Chat.Service;
using System.IO;
using Chat.Tool;

namespace Chat.Repository
{
    class FriendsRepository
    {
        private readonly string filePath;

        public FriendsRepository(string filePath)
        {
            this.filePath = filePath;
        }

        

        public bool IsFriend(int id)
        {
            List<Friends> friends = GetAllFriends(AuthenticationService.LoggedUser.Id);

            foreach (Friends friend in friends)
            {
                if(friend.RecepientId == id&&friend.RequesterId==AuthenticationService.LoggedUser.Id||friend.RequesterId==id&&friend.RecepientId==AuthenticationService.LoggedUser.Id&&friend.Status==2)
                {
                    return true;
                }
            }
            return false;
        }

        public List<Friends> GetAllFriends(int id)
        {
            Console.Clear();
            FileStream fs = new FileStream(this.filePath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);
            List<Friends> friends = new List<Friends>();
            try
            {
                while (!sr.EndOfStream)
                {
                    Friends friend = new Friends();
                    friend.SentDate = sr.ReadLine();
                    friend.Id = Convert.ToInt32(sr.ReadLine());
                    friend.RequesterId = Convert.ToInt32(sr.ReadLine());
                    friend.RecepientId = Convert.ToInt32(sr.ReadLine());
                    friend.Status = Convert.ToInt32(sr.ReadLine());

                    if (friend.RequesterId == id || friend.RecepientId == id)
                    {
                        friends.Add(friend);
                    }
                }
                return friends;
            }
            finally
            {
                sr.Close();
                fs.Close();

            }
            return null;

        }
        public int GetNextId()
        {
            FileStream fs = new FileStream(this.filePath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);

            int id = 1;
            while (!sr.EndOfStream)
            {
                Friends friend = new Friends();
                friend.SentDate = sr.ReadLine();
                friend.Id = Convert.ToInt32(sr.ReadLine());
                friend.RequesterId = Convert.ToInt32(sr.ReadLine());
                friend.RecepientId = Convert.ToInt32(sr.ReadLine());
                friend.Status = Convert.ToInt32(sr.ReadLine());
                if (id <= friend.Id)
                {
                    id = friend.Id + 1;
                }
            }
            sr.Close();
            fs.Close();
            return id;
        }
        public void AddFriend(Friends friend)
        {
            if (IsFriend(friend.RecepientId) == false)
            {
                FileStream fs = new FileStream(this.filePath, FileMode.Append);
                StreamWriter sw = new StreamWriter(fs);
                DateTime localDate = DateTime.Now;
                sw.WriteLine(localDate.ToString());
                sw.WriteLine(friend.Id);
                sw.WriteLine(friend.RequesterId);
                sw.WriteLine(friend.RecepientId);
                sw.WriteLine(friend.Status);
                sw.Close();
                fs.Close();
            }
        }

        public void Delete(int friendid)
        {
            string tempFilePath = "temp." + filePath;

            FileStream ffs = new FileStream(filePath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(ffs);

            FileStream sfs = new FileStream(tempFilePath, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(sfs);
            
            while (!sr.EndOfStream)
            {
                Friends friend = new Friends();
                friend.SentDate = sr.ReadLine();
                friend.Id = Convert.ToInt32(sr.ReadLine());
                friend.RequesterId = Convert.ToInt32(sr.ReadLine());
                friend.RecepientId = Convert.ToInt32(sr.ReadLine());
                friend.Status = Convert.ToInt32(sr.ReadLine());


                if (friend.RequesterId == friendid && friend.RecepientId == AuthenticationService.LoggedUser.Id ||
                    friend.RequesterId == AuthenticationService.LoggedUser.Id && friend.RecepientId == friendid)
                {
                    continue;
                }

                sw.WriteLine(friend.SentDate);
                sw.WriteLine(friend.Id);
                sw.WriteLine(friend.RequesterId);
                sw.WriteLine(friend.RecepientId);
                sw.WriteLine(friend.Status);
            }
            sw.Close();
            sfs.Close();
            sr.Close();
            ffs.Close();

            File.Delete(filePath);
            File.Move(tempFilePath, filePath);
        }
        public List<Friends> IncomingRequests()
        {
            List<Friends> result = new List<Friends>();
            List<Friends> friends = GetAllFriends(AuthenticationService.LoggedUser.Id);
            foreach (Friends friend in friends)
            {
                if (friend.Status == Convert.ToInt32(FriendStatusEnum.Pending) && friend.RecepientId == AuthenticationService.LoggedUser.Id)
                {
                    result.Add(friend);
                }
            }
            return result;
        }
        public void AcceptFriend(int friendid)
        {
            string tempFilePath = "temp." + filePath;

            FileStream ffs = new FileStream(filePath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(ffs);

            FileStream sfs = new FileStream(tempFilePath, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(sfs);

            while (!sr.EndOfStream)
            {
                Friends friend = new Friends();
                friend.SentDate = sr.ReadLine();
                friend.Id = Convert.ToInt32(sr.ReadLine());
                friend.RequesterId = Convert.ToInt32(sr.ReadLine());
                friend.RecepientId = Convert.ToInt32(sr.ReadLine());
                friend.Status = Convert.ToInt32(sr.ReadLine());


                if (friend.RequesterId != friendid)
                {
                    sw.WriteLine(friend.SentDate);
                    sw.WriteLine(friend.Id);
                    sw.WriteLine(friend.RequesterId);
                    sw.WriteLine(friend.RecepientId);
                    sw.WriteLine(friend.Status);
                }
                else
                {
                    friend.Status = Convert.ToInt32(FriendStatusEnum.Accepted);
                    sw.WriteLine(friend.SentDate);
                    sw.WriteLine(friend.Id);
                    sw.WriteLine(friend.RequesterId);
                    sw.WriteLine(friend.RecepientId);
                    sw.WriteLine(friend.Status);
                }

            }
            sw.Close();
            sfs.Close();
            sr.Close();
            ffs.Close();

            File.Delete(filePath);
            File.Move(tempFilePath, filePath);
        }
        public void RejectFriend(int friendid)
        {
            string tempFilePath = "temp." + filePath;

            FileStream ffs = new FileStream(filePath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(ffs);

            FileStream sfs = new FileStream(tempFilePath, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(sfs);

            while (!sr.EndOfStream)
            {
                Friends friend = new Friends();
                friend.SentDate = sr.ReadLine();
                friend.Id = Convert.ToInt32(sr.ReadLine());
                friend.RequesterId = Convert.ToInt32(sr.ReadLine());
                friend.RecepientId = Convert.ToInt32(sr.ReadLine());
                friend.Status = Convert.ToInt32(sr.ReadLine());


                if (friend.RequesterId != friendid)
                {
                    sw.WriteLine(friend.SentDate);
                    sw.WriteLine(friend.Id);
                    sw.WriteLine(friend.RequesterId);
                    sw.WriteLine(friend.RecepientId);
                    sw.WriteLine(friend.Status);
                }
                else
                {
                    friend.Status = Convert.ToInt32(FriendStatusEnum.Declined);
                    sw.WriteLine(friend.SentDate);
                    sw.WriteLine(friend.Id);
                    sw.WriteLine(friend.RequesterId);
                    sw.WriteLine(friend.RecepientId);
                    sw.WriteLine(friend.Status);
                }

            }
            sw.Close();
            sfs.Close();
            sr.Close();
            ffs.Close();

            File.Delete(filePath);
            File.Move(tempFilePath, filePath);
        }
    }
}
