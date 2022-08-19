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
        public bool ValidateMessageLimit(string msgType, string msgSender, string msgBody)
        {
            bool valid = false;

            if (!string.IsNullOrEmpty(msgBody))
            {
                if (msgType == "Email")
                {
                    if (msgBody.Length > 1028)
                    {
                        MessageBox.Show("There are too many characters in the body of the message, please change the message text to fit the character limit of 1028");
                        valid = false;
                    }
                    else { valid = true; }
                }
                else if (msgBody.Length > 140)
                {
                    MessageBox.Show("There are too many characters in the body of the message, please change the message text to fit the character limit of 140");
                    valid = false;
                }
                else
                {
                    if (msgType == "Tweet")
                    {
                        valid = tweet.ValidateTweeter(msgSender);
                    }
                    else if (msgType == "SMS")
                    {
                        valid = sms.ValidatePhoneNumber(msgSender);
                    }
                }



            }
            else
            {
                throw new ArgumentNullException("A Null value was passed to the function, please change the parameter");
            }
            return valid;
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
            bool validLimit = false;

            switch (msgType)
            {
                case "Email":
                    Email e = new Email();
                    subject = e.GetSubject(msg);
                    if (subject.Contains("SIR")) 
                    {

                    }
                    Email email = new Email(msgHeader, subject, msgType, msgBody, msgSender);
                    validLimit = ValidateMessageLimit(msgType, msgSender, msg);
                    if (validLimit)
                    {
                        SaveMessages.SerializeMessage(email, msgType);
                    }
                    break;
                case "Tweet":
                    msgBody = message.GetTextSpeak(msgBody, Initialisms);
                    Tweet tweet = new Tweet(msgHeader, msgType, msgBody, msgSender);
                    validLimit = ValidateMessageLimit(msgType, msgSender, msgBody);
                    if (validLimit)
                    {
                        SaveMessages.SerializeMessage(tweet, msgType);
                    }
                    break;
                case "SMS":
                    msgBody = message.GetTextSpeak(msgBody, Initialisms);
                    SMS sms = new SMS(msgHeader, msgType, msgBody, msgSender);
                    validLimit = ValidateMessageLimit(msgType, msgSender, msgBody);
                    if (validLimit)
                    {
                        SaveMessages.SerializeMessage(sms, msgType);
                    }
                    break;
            }
        }

    }
}
