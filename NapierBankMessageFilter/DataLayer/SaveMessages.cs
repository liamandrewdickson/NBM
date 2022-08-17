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


            switch (msgType)
            {
                case "Email":
                    location = location + @"..\..\..\Messages\Tweets";
                    break;
                case "Tweet":
                    location = location + @"..\..\..\Messages\Tweets";
                    break;
                case "SMS":
                    location = location + @"..\..\..\Messages\Tweets";
                    break;
            }

            Directory.CreateDirectory(location);

            string pathString = Path.Combine(location, fileName);

            string output = JsonSerializer.Serialize(message);
            File.WriteAllText(pathString, output);
        }

    }
}
