using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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

    }
}
