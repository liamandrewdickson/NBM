using NapierBankMessageFilter.ApplicationLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NapierBankMessageFilter.DataLayer
{
    public class SaveMessages
    {

        /// <summary>
        /// Serializes the passed Messages, Emails, SMSes and Tweets to Json
        /// </summary>
        /// <param name="message"></param>
        public static void SerializeMessage(Message message)
        {
            string fileName = message.Header + ".json";
            string location = AppDomain.CurrentDomain.BaseDirectory;
            string output = "";

            switch (message.Type)
            {
                case "Email":
                    Email email = (Email)message;
                    if (email.Subject.Contains("SIR"))
                    {
                        SignificantIncident significantIncident = (SignificantIncident)email;
                        location = Path.GetFullPath(Path.Combine(location, @"..\..\..\Messages\Email\SignificantIncident"));
                        output = JsonSerializer.Serialize(significantIncident);
                    }
                    else
                    {
                        location = Path.GetFullPath(Path.Combine(location, @"..\..\..\Messages\Email"));
                        output = JsonSerializer.Serialize(email);
                    }
                    break;
                case "Tweet":
                    Tweet tweet = (Tweet)message;
                    location = Path.GetFullPath(Path.Combine(location, @"..\..\..\Messages\Tweet"));
                    output = JsonSerializer.Serialize(tweet);
                    break;
                case "SMS":
                    location = Path.GetFullPath(Path.Combine(location, @"..\..\..\Messages\SMS"));
                    output = JsonSerializer.Serialize(message);
                    break;
            }

            Directory.CreateDirectory(location);

            string pathString = Path.Combine(location, fileName);
;

            File.WriteAllText(pathString, output);
        }

    }
}
