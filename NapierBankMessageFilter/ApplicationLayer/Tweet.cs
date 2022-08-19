using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;

namespace NapierBankMessageFilter.ApplicationLayer
{
    public class Tweet : Message
    {
        private List<string> _tweetMentions;
        private List<string> _tweetHashTags;

        public List<string> TweetMentions { get => _tweetMentions; set => _tweetMentions = value; }
        public List<string> TweetHashTags { get => _tweetHashTags; set => _tweetHashTags = value; }

        public Tweet(string header,string type, string body, string sender): base(header, type, body, sender) { }

        public Tweet() { }

        public bool ValidateTweeter(string tweeter)
        {

            Regex reg = new Regex(@"^(@){1}[1-9a-zA-Z]{15}");

            if (!string.IsNullOrEmpty(tweeter))
            {
                if (reg.IsMatch(tweeter))
                {
                    if (tweeter.Length > 20)
                    {
                        MessageBox.Show("There are too many characters in the sender of the tweet, please change the sender to fit the character limit of 15 (not including @)");
                        return false;
                    }
                }
            }
            else
            {
                throw new ArgumentNullException("A Null value was passed to the function, please change the parameter");
            }
            return true;
        }

    }
}
