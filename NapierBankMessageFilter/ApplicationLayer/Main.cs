using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;

namespace NapierBankMessageFilter.ApplicationLayer
{
    public class Main 
    {
        Email email = new Email();


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
                MessageBox.Show("Please do not pass Null values");
            }
            
            return msgType;
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
                MessageBox.Show("Please do not pass Null values");
            }

            return body;
        }

        public void SetMessageLimit(string msgType, string msg)
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
                    tweet.ValidateTweeter()
                }
                else if (msgType == "SMS")
                {
                    sms.ValidateTweeter()
                }

                if (body.Length > 140)
                {
                    MessageBox.Show("There are too many characters in the body of the message, please change the message text to fit the character limit of 140");
                }
            }
            else
            {
                MessageBox.Show("Please do not pass Null values");
            }
        }

    }
}
