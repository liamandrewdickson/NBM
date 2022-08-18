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

        public static void SerializeMessage(Message message, string msgType)
        {
            string fileName = message.Header + ".json";
            string location = AppDomain.CurrentDomain.BaseDirectory;
            string output = "";

            switch (msgType)
            {
                case "Email":
                    Email email = (Email)message;
                    location = Path.GetFullPath(Path.Combine(location, @"..\..\..\Messages\Email"));
                    output = JsonSerializer.Serialize(email);
                    break;
                case "Tweet":
                    location = Path.GetFullPath(Path.Combine(location, @"..\..\..\Messages\Tweet"));
                    output = JsonSerializer.Serialize(message);
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
