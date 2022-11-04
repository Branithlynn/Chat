using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Entity
{
    class Message
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public string SentDate { get; set; }
        public string Sender { get; set; }
        public string Recepient { get; set; }
    }
}
