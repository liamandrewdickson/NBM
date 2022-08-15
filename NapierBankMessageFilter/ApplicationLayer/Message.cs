using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NapierBankMessageFilter.ApplicationLayer
{
    public class Message
    {
        private string _type;
        private string _body;
        private string _sender;
        private int _limit;

        public string Type { get => _type; set => _type = value; }
        public string Body { get => _body; set => _body = value; }
        public string Sender { get => _sender; set => _sender = value; }
        public int Limit { get => _limit; set => _limit = value; }

        public Message (string type, string body, string sender, int limit)
        {
            Type = type;
            Body = body;
            Sender = sender;
            Limit = limit;
        }
       
        public Message () { }

    }
}
