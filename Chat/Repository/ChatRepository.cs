using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.Entity;
using System.IO;

namespace Chat.Repository
{
    class ChatRepository
    {
        private readonly string filePath;

        public ChatRepository(string filePath)
        {
            this.filePath = filePath;
        }
        public int GetNextId()
        {
            FileStream fs = new FileStream(this.filePath, FileMode.OpenOrCreate);
            StreamReader reader = new StreamReader(fs);

            int id = 1;
            Message message = new Message();
            try
            {
                while (!reader.EndOfStream)
                {
                    message.Id = Convert.ToInt32(reader.ReadLine());
                    message.Body = reader.ReadLine();
                    message.SentDate = reader.ReadLine();
                    message.Sender = reader.ReadLine();
                    message.Recepient = reader.ReadLine();
                    if (id <= message.Id)
                    {
                        id = message.Id + 1;
                    }
                }

            }
            finally
            {
                reader.Close();
                fs.Close();
            }

            return id;
        }
        public List<Message> GetAllMessages(string user1Name, string user2Name)
        {
            Console.Clear();
            FileStream fs = new FileStream(this.filePath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);
            List<Message> result = new List<Message>();
            try
            {
                while (!sr.EndOfStream)
                {
                    Message message = new Message();
                    message.Id = Convert.ToInt32(sr.ReadLine());
                    message.Body = sr.ReadLine();
                    message.SentDate = sr.ReadLine();
                    message.Sender = sr.ReadLine();
                    message.Recepient = sr.ReadLine();

                    if (message.Sender == user1Name && message.Recepient == user2Name || message.Sender == user2Name && message.Recepient == user1Name)
                    {
                        result.Add(message);
                    }
                }
            }
            finally
            {

                sr.Close();
                fs.Close();
            }
            return result;
         
        }
        public void SendMessage(Message message)
        {
            FileStream fs = new FileStream(this.filePath, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            try
            {
                sw.WriteLine(message.Id);
                sw.WriteLine(message.Body);
                sw.WriteLine(message.SentDate);
                sw.WriteLine(message.Sender);
                sw.WriteLine(message.Recepient);
            }
            finally
            {
                sw.Close();
                fs.Close();
            }

            
        }
        public bool DoesUserExist(string recipient)
        {

            Message message = new Message();
            UserRepository usersRepository = new UserRepository("users.txt");
            List<User> users = usersRepository.GetAll();

            foreach (User user in users)
            {
                if (user.Username == recipient)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
