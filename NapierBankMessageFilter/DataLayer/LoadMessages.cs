using NapierBankMessageFilter.ApplicationLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Windows.Interop;

namespace NapierBankMessageFilter.DataLayer
{
    public class LoadMessages
    {
        /// <summary>
        /// Reads in the text words and initialisms
        /// </summary>
        /// <returns>
        /// A Dictionary of initialisms and expanded phrases
        /// </returns>
        public static Dictionary<string, string> LoadTextWords()
        {
            Dictionary<string, string> initialisms = new Dictionary<string, string>();
            string line = "";
            string[] phrases;

            using (var stream = new StreamReader(@"..\..\..\Assets\textwords.csv"))
            {
                while (!stream.EndOfStream)
                {
                    line = stream.ReadLine();
                    if (string.IsNullOrEmpty(line)) 
                    {
                        continue;
                    }
                    phrases = line.Split(',');
                    initialisms.Add(phrases[0], phrases[1]);
                }
            }

            return initialisms;
        }

        /// <summary>
        /// Creates the file locations if they do not exist
        /// </summary>
        public static void InitialiseLocations()
        {
            string[] locations = new string[] { @"..\..\..\Messages\Tweet", @"..\..\..\Messages\Email", @"..\..\..\Messages\SMS", @"..\..\..\Messages\Email\SignificantIncident" };

            foreach (string location in locations)
            {
                Directory.CreateDirectory(Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, location)));
            }
        }

        /// <summary>
        /// Deserializes previous Messages, Emails, SMSes and Tweets
        /// </summary>
        /// <param name="msgType"></param>
        /// <returns>
        /// A List of deserialized Messages
        /// </returns>
        public static List<Message> DeserializeMessages(string msgType, List<Message> messages)
        {
            string location = AppDomain.CurrentDomain.BaseDirectory;
            string contents = "";
            Email deserializedEmail;
            SignificantIncident deserializedSI;
            Tweet deserializedTweet;
            SMS deserializedSMS;

            switch (msgType)
            {
                case "Email":
                    location = Path.GetFullPath(Path.Combine(location, @"..\..\..\Messages\Email"));
                    break;
                case "Tweet":
                    location = Path.GetFullPath(Path.Combine(location, @"..\..\..\Messages\Tweet"));
                    break;
                case "SMS":
                    location = Path.GetFullPath(Path.Combine(location, @"..\..\..\Messages\SMS"));
                    break;
                case "SignificantIncident":
                    location = Path.GetFullPath(Path.Combine(location, @"..\..\..\Messages\Email\SignificantIncident"));
                    break;
            }

            if (Directory.EnumerateFileSystemEntries(location).Any())
            {
                foreach (string file in Directory.EnumerateFiles(location, "*.json"))
                {
                    contents = File.ReadAllText(file);
                    switch (msgType)
                    {
                        case "Email":
                            deserializedEmail = JsonSerializer.Deserialize<Email>(contents);
                            messages.Add(deserializedEmail);
                            break;
                        case "Tweet":
                            deserializedTweet = JsonSerializer.Deserialize<Tweet>(contents);
                            messages.Add(deserializedTweet);
                            break;
                        case "SMS":
                            deserializedSMS = JsonSerializer.Deserialize<SMS>(contents);
                            messages.Add(deserializedSMS);
                            break;
                        case "SignificantIncident":
                            deserializedSI = JsonSerializer.Deserialize<SignificantIncident>(contents);
                            messages.Add(deserializedSI);
                            break;
                    }

                }
            }
            

            return messages;

        }

        /// <summary>
        /// Gets saved messages from proveided CSV file
        /// </summary>
        /// <param name="path"></param>
        /// <param name="file"></param>
        /// <param name="main"></param>
        /// <returns>
        /// A List of deserialized Messages
        /// </returns>
        public static void CollectMessagesFromCSV(string path, string file, Main main)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            Message message = new Message();

            using (var stream = new StreamReader(path))
            {
                while (!stream.EndOfStream)
                {
                    string line = stream.ReadLine();
                    if (string.IsNullOrEmpty(line))
                    {
                        continue;
                    }
                    var words = line.Split(',');
                    data.Add(words[0], words[1]);
                }
            }

            foreach (KeyValuePair<string, string> kvp in data)
            {
                string header = kvp.Key;
                string msg = kvp.Value;
                string msgType = main.ValidateMessageType(header);

                if (msgType == "Email")
                {
                    msg = msg.Replace("Subject:", "\nSubject:");
                }
                msg = msg.Replace("Message Text:", "\nMessage Text:");

                main.ValidateMessage(msg, msgType, message.GetMessageText(msg), header, message.GetMessageSender(msgType, msg));
            }
        }

    }
}
