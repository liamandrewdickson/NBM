using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NapierBankMessageFilter.ApplicationLayer
{
    public class Message
    {
        private string _header;
        private string _type;
        private string _body;
        private string _sender;

        public string Header { get => _header; set => _header = value; }
        public string Type { get => _type; set => _type = value; }
        public string Body { get => _body; set => _body = value; }
        public string Sender { get => _sender; set => _sender = value; }


        /// <summary>
        /// Creates a new Message
        /// </summary>
        /// <param name="header"></param>
        /// <param name="type"></param>
        /// <param name="body"></param>
        /// <param name="sender"></param>
        /// <returns>
        /// A Message with the parameters provided
        /// </returns>
        public Message(string header, string type, string body, string sender)
        {
            Header = header;
            Type = type;
            Body = body;
            Sender = sender;
        }

        public Message() { }

        /// <summary>
        /// Sets the initial titles for the text box depending on the message type
        /// </summary>
        /// <param name="msgType"></param>
        /// <returns>
        /// The initial body of the message with the filled in titles
        /// </returns>
        public string SetMessageTitles(string msgType)
        {
            string body = "";

            if (!string.IsNullOrEmpty(msgType))
                if (msgType == "Email")
                {
                    body = "Sender: \nSubject: \nMessage Text: ";
                }
                else
                {
                    body = "Sender: \nMessage Text: ";
                }
            else
            {
                MessageBox.Show("The Message Type passed to the function was null, please change the header");
            }

            return body;
        }

        /// <summary>
        /// Splits the messsage to get the sender for the message
        /// </summary>
        /// <param name="msgType"></param>
        /// <param name="msg"></param>
        /// <returns>
        /// The sender for the message
        /// </returns>
        public string GetMessageSender(string msgType, string msg)
        {
            int pFrom = msg.IndexOf("Sender: ") + "Sender: ".Length;
            string sender = "";
            int pTo = msg.LastIndexOf("\nMessage Text: ");

            if (!string.IsNullOrEmpty(msg))
            { 
                if (msgType == "Email")
                {
                    pTo = msg.LastIndexOf("\nSubject: ");
                }
                sender = msg.Substring(pFrom, pTo - pFrom);
            }
            else
            {
                MessageBox.Show("The Message Sender passed to the function was null, please change the Message Sender");
            }

            return sender;

        }

        /// <summary>
        /// Splits the messsage to get the message text for the message
        /// </summary>
        /// <param name="msg"></param>
        /// <returns>
        /// The message text, without the sender and subject
        /// </returns>
        public string GetMessageText(string msg)
        {
            string body = "";

            if (!string.IsNullOrEmpty(msg))
            {
                string[] msgParts;
                msgParts = msg.Split("Message Text: ");
                body = msgParts[1];
            }
            else
            {
                MessageBox.Show("The Message Text passed to the function was null, please change the Message Text");
            }

            return body;
        }

        /// <summary>
        /// Takes the body of the message and expands any initialisms in and updated the message
        /// </summary>
        /// <param name="body"></param>
        /// <param name="initialisms"></param>
        /// <returns>
        /// The body of the message with expanded initialisms
        /// </returns>
        public string GetTextSpeak(string body, Dictionary<string, string> initialisms)
        {
            if (!string.IsNullOrEmpty(body))
            {
                foreach (string initial in initialisms.Keys)
                {
                    body = body.Replace(initial, initial + " <" + initialisms[initial] + ">");
                }
            }
            else
            {
                MessageBox.Show("The Message Text passed to the function was empty, please change the Message Text");
            }

            return body;
        }


    }
}
