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
        private Dictionary<string, string> _sortAndType;
        private Dictionary<string, int> _dictTweetHashTags;
        private List<Message> _significantIncidents;
        private List<Message> _tweets;
        private List<Message> _emails;
        private List<Message> _smses;
        private List<string> _tweetMentions;
        private List<string> _tweetHashTags;
        private List<string> _quarantinedURLs;
        Email email = new Email();
        Tweet tweet = new Tweet();
        SMS sms = new SMS();

        public Dictionary<string, string> Initialisms { get => _initialisms; set => _initialisms = value; }
        public Dictionary<string, string> SortAndType { get => _sortAndType; set => _sortAndType = value; }
        public Dictionary<string, int> DictTweetHashTags { get => _dictTweetHashTags; set => _dictTweetHashTags = value; }
        public List<Message> SignificantIncidents { get => _significantIncidents; set => _significantIncidents = value; }
        public List<Message> Tweets { get => _tweets; set => _tweets = value; }
        public List<Message> Emails { get => _emails; set => _emails = value; }
        public List<Message> SMSes { get => _smses; set => _smses = value; }

        public List<string> TweetMentions { get => _tweetMentions; set => _tweetMentions = value; }
        public List<string> TweetHashTags { get => _tweetHashTags; set => _tweetHashTags = value; }
        public List<string> QuarantinedURLs { get => _quarantinedURLs; set => _quarantinedURLs = value; }


        /// <summary>
        /// Runs when the program starts, it loads in messages, and reads the textwords into the program
        /// </summary>
        public void NBMStart()
        {
            LoadMessages.InitialiseLocations();
            Initialisms = LoadMessages.LoadTextWords();
            SignificantIncidents = new List<Message>();
            Tweets = new List<Message>();
            Emails = new List<Message>();
            SMSes = new List<Message>();
            
            LoadMessages.DeserializeMessages("Email", Emails);
            LoadMessages.DeserializeMessages("Tweet", Tweets);
            LoadMessages.DeserializeMessages("SMS", SMSes);
            LoadMessages.DeserializeMessages("SignificantIncident", SignificantIncidents);

            SortAndType = SignificantIncident.GetSignificantIncidents(SignificantIncidents);
            QuarantinedURLs = Email.CollectURLs(Emails);
            TweetMentions = Tweet.CollectMentions(Tweets);
            DictTweetHashTags = Tweet.CollectHashtags(Tweets);
        }

        /// <summary>
        /// Gets the message type from the message header
        /// </summary>
        /// <param name="msgHeader"></param>
        /// <returns>
        /// The type of the message input
        /// </returns>
        public string ValidateMessageType(string msgHeader)
        {
            string msgType = "";
            Regex reg = new Regex(@"(?i)^(S|E|T){1}[0-9]{9}");

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
                    msgType = "";
                }
            }
            else
            {
                MessageBox.Show("The Message Header was passed to the function was null, please change the Message Header");
            }
            
            return msgType;
        }

        /// <summary>
        /// Checks that the message has the correct length depending on the message type
        /// </summary>
        /// <param name="msgType"></param>
        /// <param name="msgSender"></param>
        /// <param name="msgBody"></param>
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
                MessageBox.Show("The Message Body passed to the function was null, please change the Message Body");
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
        public bool ValidateMessage(string msg, string msgType, string msgBody, string msgHeader, string msgSender)
        {
            bool submitted = false;
            string subject = "";
            Message message = new Message();
            bool validLimit = false;

            if (!string.IsNullOrEmpty(msg) || !string.IsNullOrEmpty(msgType) || !string.IsNullOrEmpty(msgBody) || !string.IsNullOrEmpty(msgHeader) || !string.IsNullOrEmpty(msgSender))
            {

                switch (msgType)
                {
                    case "Email":
                        Email e = new Email();
                        subject = e.GetSubject(msg);
                        QuarantinedURLs = e.CheckURLs(msgBody);
                        msgBody = e.Quarantine(msgBody, QuarantinedURLs);
                        if (subject.Contains("SIR"))
                        {

                            SignificantIncident sI = new SignificantIncident();
                            string sortCode = sI.GetSortCode(msgBody);
                            bool validDate = sI.CheckDate(subject);
                            if (validDate)
                            {
                                string incidentType = sI.GetIncidentType(msgBody);

                                SignificantIncident significantIncident = new SignificantIncident(sortCode, incidentType, msgHeader, subject, msgType, msgBody, msgSender, QuarantinedURLs);
                                validLimit = ValidateMessageLimit(msgType, msgSender, msgBody);
                                if (validLimit)
                                {
                                    SaveMessages.SerializeMessage(significantIncident);
                                    SignificantIncidents = new List<Message>();
                                    SignificantIncidents = LoadMessages.DeserializeMessages("SignificantIncident", SignificantIncidents);
                                    SortAndType = SignificantIncident.GetSignificantIncidents(SignificantIncidents);
                                    submitted = true;
                                }
                            }
                            else { break; }
                        }
                        else
                        {
                            Email email = new Email(msgHeader, subject, msgType, msgBody, msgSender, QuarantinedURLs);
                            validLimit = ValidateMessageLimit(msgType, msgSender, msgBody);
                            if (validLimit)
                            {
                                SaveMessages.SerializeMessage(email);
                                submitted = true;
                            } else { break; }
                        }
                        
                        break;
                    case "Tweet":
                        Tweet t = new Tweet();
                        msgBody = message.GetTextSpeak(msgBody, Initialisms);
                        TweetMentions = t.GetMentionsOrHashTag(msgBody, "Mention");
                        TweetHashTags = t.GetMentionsOrHashTag(msgBody, "HashTag");
                        Tweet tweet = new Tweet(msgHeader, msgType, msgBody, msgSender, TweetMentions, TweetHashTags);
                        validLimit = ValidateMessageLimit(msgType, msgSender, msgBody);
                        if (validLimit)
                        {
                            SaveMessages.SerializeMessage(tweet);
                            Tweets = LoadMessages.DeserializeMessages("Tweet", Tweets);
                            TweetMentions = Tweet.CollectMentions(Tweets);
                            DictTweetHashTags = Tweet.CollectHashtags(Tweets);
                            submitted = true;
                        }
                        break;
                    case "SMS":
                        msgBody = message.GetTextSpeak(msgBody, Initialisms);
                        SMS sms = new SMS(msgHeader, msgType, msgBody, msgSender);
                        validLimit = ValidateMessageLimit(msgType, msgSender, msgBody);
                        if (validLimit)
                        {
                            SaveMessages.SerializeMessage(sms);
                            SMSes = LoadMessages.DeserializeMessages("SMS", SMSes);
                            submitted = true;
                        }
                        break;
                }
            }
            else
            {
                MessageBox.Show("One of the message values was null or empty that was passed to the function, please change them to have values");
            }
            return submitted;
        }

    }
}
