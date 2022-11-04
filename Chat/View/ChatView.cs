using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.Repository;
using Chat.Entity;
using Chat.Service;
using System.Threading;

namespace Chat.View
{
    class ChatView
    {
        public static string sentMessage;
        public void SelectPerson()
        {
            Console.Clear();
            ChatRepository chatRepository = new ChatRepository("messages.txt");
            UserRepository userRepository = new UserRepository("users.txt");
            FriendsRepository friendsRepository = new FriendsRepository("friends.txt");
            FriendsView friendsView = new FriendsView();
            friendsView.SeeAllFriends();


            Console.WriteLine("Who do you want to chat with?");
            Console.Write("Recepient: ");
            string input = Console.ReadLine();

            if (chatRepository.DoesUserExist(input))
            {
                User user = userRepository.GetByName(input);
                if (friendsRepository.IsFriend(user.Id))
                {
                    Chat(input);
                }
                else
                {
                    Console.WriteLine("User is not a friend");
                    Console.ReadKey(true);
                    return;
                }

            }
            else
            {
                Console.WriteLine("Non existing user");
                Console.ReadKey(true);
                return;
            }
        }

        public void Chat(string recepient)
        {
            Console.Clear();
            ChatRepository chatRepository = new ChatRepository("messages.txt");
            Console.WriteLine("Chat with: " + recepient);
            Console.ReadKey(true);
            Thread mainThread = Thread.CurrentThread;
            mainThread.Name = "Main";
            sentMessage = "";
            Thread receiving = new Thread(() => GetAllMessagesThread(AuthenticationService.LoggedUser.Username, recepient));
            receiving.Start();
            while (true)
            {
                string input = Console.ReadLine();
                switch (input.ToUpper())
                {
                    case "S":
                        {
                            receiving.Suspend();

                            SendMessage(AuthenticationService.LoggedUser.Username, recepient);
                            receiving.Resume();
                            break;
                        }
                    case "X":
                        {
                            receiving.Abort();
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
                if (sentMessage != "" && sentMessage.ToUpper() == "X")
                {
                    receiving.Abort();
                    UserView userView = new UserView();
                    userView.Show();
                }
            }

            Console.ReadKey(true);
        }
        public void GetAllMessages(string sender, string recepient)
        {
            Console.Clear();
            ChatRepository chatRepository = new ChatRepository("messages.txt");
            List<Message> messages = chatRepository.GetAllMessages(sender, recepient);

            foreach (Message message in messages)
            {
                Console.WriteLine(message.SentDate + "  " + message.Sender + ": " + message.Body);
            }

            Console.WriteLine("[S]end message");
            Console.WriteLine("E[x]it");
            Console.Write(">: ");

        }
        public void GetAllMessagesThread(string sender, string recepient)
        {
            while (true)
            {
                Console.Clear();
                ChatRepository chatRepository = new ChatRepository("messages.txt");
                List<Message> messages = chatRepository.GetAllMessages(sender, recepient);

                foreach (Message message in messages)
                {
                    Console.WriteLine(message.SentDate + "  " + message.Sender + ": " + message.Body);
                }
                Console.WriteLine("[S]end message");
                Console.WriteLine("E[x]it");
                Console.Write(">: ");

                Thread.Sleep(5000);

            }
        }
        public void SendMessage(string sender, string recepient)
        {


            DateTime localDate = DateTime.Now;
            Message message = new Message();
            Console.Write(">: ");
            sentMessage = "";
            string input = Console.ReadLine();
            message.Id = 5;
            message.Body = input;
            message.SentDate = localDate.ToString();
            message.Sender = sender;
            message.Recepient = recepient;

            ChatRepository chatRepository = new ChatRepository("messages.txt");

            sentMessage = input;
            if (input.ToUpper() != "X")
            {
                chatRepository.SendMessage(message);
                sentMessage = "sent";
                GetAllMessages(AuthenticationService.LoggedUser.Username, recepient);
            }
        }
    }
}
