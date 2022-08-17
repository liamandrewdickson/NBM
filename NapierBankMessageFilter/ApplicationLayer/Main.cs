using NapierBankMessageFilter.DataLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;

namespace NapierBankMessageFilter.ApplicationLayer
{
    public class Main 
    {
        private Dictionary<string, string> initialisms;
        Email email = new Email();
        Tweet tweet = new Tweet();
        SMS sms = new SMS();

        public Dictionary<string, string> Initialisms { get => initialisms; set => initialisms = value; }

        public void NBMStart()
        {
            Initialisms = LoadMessages.LoadTextWords();
        }

        public string ValidateMessageType(string msgID)
        {
            string msgType = "";
            Regex reg = new Regex(@"(?i)^(S|E|T){1}[1-9]{9}");

            if (!string.IsNullOrEmpty(msgID))
            {
                if (reg.IsMatch(msgID))
                {
                    if (msgID.StartsWith("S", StringComparison.InvariantCultureIgnoreCase))
                    {
                        msgType = "SMS";
                    }
                    else if (msgID.StartsWith("E", StringComparison.InvariantCultureIgnoreCase))
                    {
                        msgType = "Email";
                    }
                    else if (msgID.StartsWith("T", StringComparison.InvariantCultureIgnoreCase))
                    {
                        msgType = "Tweet";
                    }
                }
                else
                {
                    MessageBox.Show("Please input an ID with the message type of S, E or T at the start followed by 9 digits");
                }
            }
            else
            {
                throw new ArgumentNullException("A Null value was passed to the function, please change the parameter");
            }
            
            return msgType;
        }

        public string GetMessageSender(string msgType, string msg)
        {
            int pFrom = msg.IndexOf("Sender: ") + "Sender: ".Length;
            int pTo = msg.LastIndexOf("\nMessage Text: ");
            string sender = msg.Substring(pFrom, pTo - pFrom);

            if (!string.IsNullOrEmpty(sender))
            {
                switch (msgType)
                {
                    case "Email":
                        break;
                    case "Tweet":
                        break;
                    case "SMS":
                        break;
                }
            }
            else
            {
                throw new ArgumentNullException("A Null value was passed to the function, please change the parameter");
            }
            
            return sender;

        }

        public string GetMessageBody(string msgType)
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
                throw new ArgumentNullException("A Null value was passed to the function, please change the parameter");
            }

            return body;
        }

        public void ValidateMessageLimit(string msgType, string msg)
        {

            string[] msgParts;
            msgParts = msg.Split("Message Text: ");
            string body = msgParts[1];

            if (!string.IsNullOrEmpty(body))
            {
                if (msgType == "Email")
                {
                    email.ValidateSubject(msg);
                    if (body.Length > 1028)
                    {
                        MessageBox.Show("There are too many characters in the body of the message, please change the message text to fit the character limit of 1028");
                    }
                }
                else if (msgType == "Tweet")
                {
                    tweet.ValidateTweeter(msg);
                }
                else if (msgType == "SMS")
                {
                    sms.ValidatePhoneNumber(msg);
                }

                if (body.Length > 140)
                {
                    MessageBox.Show("There are too many characters in the body of the message, please change the message text to fit the character limit of 140");
                }

            }
            else
            {
                throw new ArgumentNullException("A Null value was passed to the function, please change the parameter");
            }
        }

        public void ValidateMessage(string msgType, string msgBody, string msgHeader, string msgSender)
        {
            switch (msgType)
            {
                case "Email":
                    Email sms = new Email(msgHeader, msgType, msgBody, msgSender);
                    SaveMessages.SerializeMessage(tweet, msgType);
                    break;
                case "Tweet":
                    Tweet tweet = new Tweet(msgHeader, msgType, msgBody, msgSender);
                    SaveMessages.SerializeMessage(tweet, msgType);
                    break;
                case "SMS":
                    SMS sms = new SMS(msgHeader, msgType, msgBody, msgSender);
                    SaveMessages.SerializeMessage(tweet, msgType);
                    break;
            }
        }

    }
}
