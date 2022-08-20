using NapierBankMessageFilter.ApplicationLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NapierBankMessageFilter
{
    /// <summary>
    /// Interaction logic for MessageWindow.xaml
    /// </summary>
    public partial class MessageWindow : Window
    {
        public MessageWindow(Main main)
        {
            InitializeComponent();

            foreach (Email email in main.Emails)
            {
                txtEmails.Text = txtEmails.Text + "Header: " + email.Header + "\n" +
                "Sender: " + email.Sender + "\n" +
                "Subject: " + email.Subject + "\n" +
                "Body: " + email.Body + "\n\n";
            }

            foreach (Tweet tweet in main.Tweets)
            {
                txtTweets.Text = txtTweets.Text + "Header: " + tweet.Header + "\n" +
                "Sender: " + tweet.Sender + "\n" +
                "Body: " + tweet.Body + "\n\n";
            }

            foreach (SMS sms in main.SMSes)
            {
                txtSMSMessages.Text = txtSMSMessages.Text + "Header: " + sms.Header + "\n" +
                "Sender: " + sms.Sender + "\n" +
                "Body: " + sms.Body + "\n\n";
            }

            txtSignificantIncidents.Text = String.Join(Environment.NewLine, main.SortAndType);
            txtTrendingHastags.Text = String.Join(Environment.NewLine, main.DictTweetHashTags);
            txtMentions.Text = String.Join(Environment.NewLine, main.TweetMentions);
            txtQuarantined.Text = String.Join(Environment.NewLine, main.QuarantinedURLs);
        }

        

    }
}
