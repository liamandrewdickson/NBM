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
        private Dictionary<string, string> _initialisms;
        Email email = new Email();
        Tweet tweet = new Tweet();
        SMS sms = new SMS();

        public Dictionary<string, string> Initialisms { get => _initialisms; set => _initialisms = value; }

        /// <summary>
        /// Runs when the program starts, it loads in messages, and reads the textwords into the program
        /// </summary>
        public void NBMStart()
        {
            Initialisms = LoadMessages.LoadTextWords();
            LoadMessages.InitialiseLocations();
            LoadMessages.DeserializeMessages("Email");
            LoadMessages.DeserializeMessages("Tweet");
            LoadMessages.DeserializeMessages("SMS");
        }

        /// <summary>
        /// Gets the message type from the message header
        /// </summary>
        /// <param name="msgID"></param>
        /// <returns>
        /// The type of the message input
        /// </returns>
        public string ValidateMessageType(string msgHeader)
        {
            string msgType = "";
            Regex reg = new Regex(@"(?i)^(S|E|T){1}[1-9]{9}");

            if (!string.IsNullOrEmpty(msgHeader))
            {
                if (reg.IsMatch(msgHeader))
                {
                    if (msgHeader.StartsWith("S", StringComparison.InvariantCultureIgnoreCase))
                    {
                        msgType = "SMS";
                    }
                    else if (msgHeader.StartsWith("E", StringComparison.InvariantCultureIgnoreCase))
                    {
                        msgType = "Email";
                    }
                    else if (msgHeader.StartsWith("T", StringComparison.InvariantCultureIgnoreCase))
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

        /// <summary>
        /// Checks that the message has the correct length depending on the message type
        /// </summary>
        /// <param name="msgType"></param>
        /// <param name="msg"></param>
        public void ValidateMessageLimit(string msgType, string msg)
        {

            string[] msgParts;
            msgParts = msg.Split("Message Text: ");
            string body = msgParts[1];

            if (!string.IsNullOrEmpty(body))
            {
                if (msgType == "Email")
                {
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

        /// <summary>
        /// When the message is submitted, this triggers validating that the message is correct
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="msgType"></param>
        /// <param name="msgBody"></param>
        /// <param name="msgHeader"></param>
        /// <param name="msgSender"></param>
        public void ValidateMessage(string msg, string msgType, string msgBody, string msgHeader, string msgSender)
        {
            string subject = "";
            Message message = new Message();


            switch (msgType)
            {
                case "Email":
                    Email e = new Email();
                    subject = e.GetSubject(msg);
                    if (subject.Contains("SIR")) 
                    {

                    }
                    ValidateMessageLimit(msgType, msg);
                    Email email = new Email(msgHeader, subject, msgType, msgBody, msgSender);
                    SaveMessages.SerializeMessage(email, msgType);
                    break;
                case "Tweet":
                    msgBody = message.GetTextSpeak(msgBody, Initialisms);
                    Tweet tweet = new Tweet(msgHeader, msgType, msgBody, msgSender);
                    ValidateMessageLimit(msgType, msg);
                    SaveMessages.SerializeMessage(tweet, msgType);
                    break;
                case "SMS":
                    msgBody = message.GetTextSpeak(msgBody, Initialisms);
                    SMS sms = new SMS(msgHeader, msgType, msgBody, msgSender);
                    ValidateMessageLimit(msgType, msg);
                    SaveMessages.SerializeMessage(sms, msgType);
                    break;
            }
        }

    }
}
