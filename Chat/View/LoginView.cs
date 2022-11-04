using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.Service;

namespace Chat.View
{
    class LoginView
    {
        public void Login()
        {
           
         Console.Clear();
            while (true)
            {

                Console.Write("Username: ");
                string username = Console.ReadLine();
                Console.Write("Password: ");
                string password = Console.ReadLine();

                AuthenticationService.AuthenticateUser(username, password);

                if (AuthenticationService.LoggedUser != null)
                {
                    Console.WriteLine("Welcome " + AuthenticationService.LoggedUser.Username);
                    Console.ReadKey(true);
                    break;
                }
                else
                {  
                    Console.WriteLine("Invalid username or password");
                    Console.ReadKey(true);
                }

            }
            
        }
    }
}
