using NapierBankMessageFilter.ApplicationLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace NapierBankMessageFilter.DataLayer
{
    public class LoadMessages
    {
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

        public static void InitialiseLocations()
        {
            string[] locations = new string[] { @"..\..\..\Messages\Tweet", @"..\..\..\Messages\Email", @"..\..\..\Messages\SMS" };

            foreach (string location in locations)
            {
                Directory.CreateDirectory(Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, location)));
            }
        }

        public static List<Message> DeserializeMessages(string msgType)
        {
            string location = AppDomain.CurrentDomain.BaseDirectory;
            string contents = "";
            List <Message> list = new List<Message>();
            List<Email> eList = new List<Email>();
            Message deserializedMsg;
            Email deserializedEmail;

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
            }

            if (Directory.EnumerateFileSystemEntries(location).Any())
            {
                foreach (string file in Directory.EnumerateFiles(location, "*.json"))
                {
                    contents = File.ReadAllText(file);
                    if (msgType == "Email")
                    {
                        deserializedEmail = JsonSerializer.Deserialize<Email>(contents);
                        list.Add(deserializedEmail);
                    }
                    else
                    {
                        deserializedMsg = JsonSerializer.Deserialize<Message>(contents);
                        list.Add(deserializedMsg);
                    } 
                }
            }
            

            return list;

        }


    }
}
