using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;

namespace NapierBankMessageFilter.ApplicationLayer
{
    public class SMS : Message
    {

        /// <summary>
        /// Creates a new SMS
        /// </summary>
        /// <param name="header"></param>
        /// <param name="type"></param>
        /// <param name="body"></param>
        /// <param name="sender"></param>
        /// <returns>
        /// A SMS with the parameters provided
        /// </returns>
        public SMS(string header, string type, string body, string sender) : base(header, type, body, sender) { }

        public SMS() { }


        /// <summary>
        /// Validates that the Phone Number of the message is correct
        /// </summary>
        /// <param name="phonenumber"></param>
        /// <returns>
        /// A boolean of true if the Phone Number is valid
        /// </returns>
        public bool ValidatePhoneNumber(string phonenumber)
        {
            Regex reg = new Regex(@"(9[976]\d|8[987530]\d|6[987]\d|5[90]\d|42\d|3[875]\d|2[98654321]\d|9[8543210]|8[6421]|6[6543210]|5[87654321]|4[987654310]|3[9643210]|2[70]|7|1)\d{1,14}$");

            if (!string.IsNullOrEmpty(phonenumber))
            {
                if (reg.IsMatch(phonenumber))
                {
                    if (phonenumber.Length > 14)
                    {
                        MessageBox.Show("The entered phone number is not valid, please change it to be a valid phone number.");
                        return false;
                    }
                }
                else return false;
            }
            else
            {
                MessageBox.Show("The phone number passed to the function was null, please change the phone number");
            }
            return true;
        }

    }

}


