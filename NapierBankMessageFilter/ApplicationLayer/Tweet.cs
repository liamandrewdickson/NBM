using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

        /// <summary>
        /// Creates a new Tweet
        /// </summary>
        /// <param name="header"></param>
        /// <param name="type"></param>
        /// <param name="body"></param>
        /// <param name="sender"></param>
        /// <param name="tweetMentions"></param>
        /// <param name="tweetHashTags"></param>
        /// <returns>
        /// A Tweet with the parameters provided
        /// </returns>
        public Tweet(string header, string type, string body, string sender, List<string> tweetMentions, List<string> tweetHashTags) : base(header, type, body, sender)
        {
            TweetMentions = tweetMentions;
            TweetHashTags = tweetHashTags;
        }

        public Tweet() { }

        /// <summary>
        /// Validates that the Tweeter of the message is correct
        /// </summary>
        /// <param name="tweeter"></param>
        /// <returns>
        /// A boolean of true if the Tweeter is valid
        /// </returns>
        public bool ValidateTweeter(string tweeter)
        {

            Regex reg = new Regex(@"^(@){1}[1-9a-zA-Z]{1,15}");

            if (!string.IsNullOrEmpty(tweeter))
            {
                if (reg.IsMatch(tweeter))
                {
                    if (tweeter.Length > 16)
                    {
                        MessageBox.Show("There are too many characters in the sender of the tweet, please change the sender to fit the character limit of 15 (not including @)");
                        return false;
                    }
                }
                else return false;
            }
            else
            {
                MessageBox.Show("The Tweeter passed to the function was null, please change the tweeter");
            }
            return true;
        }


        /// <summary>
        /// Gets any Mentions or HashTags that are in the body of a Tweet
        /// </summary>
        /// <param name="body"></param>
        /// <returns>
        /// A list of the Mentions or a list of HashTags
        /// </returns>
        public List<string> GetMentionsOrHashTag(string body, string type)
        {
            Regex rx = new Regex(@"@+[a-zA-Z0-9(_)]{1,}");
            if (type == "HashTag")
            {
                rx = new Regex(@"#+[a-zA-Z0-9(_)]{1,}");
            }
            MatchCollection matches = rx.Matches(body);
            List<string> list = new List<string>();

            foreach (Match match in matches)
            {
                if (type == "HashTag")
                {
                    list.Add(match.Value);
                    TweetHashTags = list;
                }
                else
                {
                    list.Add(match.Value);
                    TweetMentions = list;
                }
            }

            if (type == "HashTag")
            {
                return TweetHashTags;
            }
            else
            {
                return TweetMentions;
            }
        }

        /// <summary>
        /// Collects the Mentions from Tweets and checks their usage
        /// </summary>
        /// <param name="tweets"></param>
        /// <returns>
        /// A List of mentions
        /// </returns>
        public static List<string> CollectMentions(List<Message> tweets)
        {
            List<string> list = new List<string>();

            foreach (Message message in tweets)
            {
                Tweet tweet = (Tweet)message;

                if (tweet.TweetMentions != null)
                {
                    foreach (string ment in tweet.TweetMentions)
                    {
                        list.Add(ment);
                    }
                }
                
            }

            return list;
        }

        /// <summary>
        /// Collects the HashTags from Tweets and checks their usage
        /// </summary>
        /// <param name="tweets"></param>
        /// <returns>
        /// A Dictionary of HashTags
        /// </returns>
        public static Dictionary<string, int> CollectHashtags(List<Message> tweets)
        {
            List<string> list = new List<string>();
            Dictionary<string, int> dict = new Dictionary<string, int>();

            foreach (Message message in tweets)
            {
                Tweet tweet = (Tweet)message;
                if (tweet.TweetHashTags != null)
                {
                    foreach (string hash in tweet.TweetHashTags)
                    {
                        list.Add(hash);
                    }
                }

            }

            var hashTags = list.GroupBy(x => x)
                            .ToDictionary(y => y.Key, y => y.Count())
                            .OrderByDescending(z => z.Value);

            foreach (var h in hashTags)
            {
                dict.Add(h.Key, h.Value);
            }

            return dict;
        }


    }
}
