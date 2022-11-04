using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Entity
{
    class Friends
    {
        public string SentDate { get; set; }
        public int Id { get; set; }
        public int RequesterId { get; set; }
        public int RecepientId { get; set; }
        public int Status { get; set; }
    }
}
