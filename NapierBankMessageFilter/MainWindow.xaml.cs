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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NapierBankMessageFilter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string msgType = "";
        private string msgHeader = "";
        private string msg = "";
        Main main = new Main();
        Message message = new Message();

        public string MsgType { get => msgType; set => msgType = value; }
        public string MsgHeader { get => msgHeader; set => msgHeader = value; }
        public string Msg { get => msg; set => msg = value; }

        public MainWindow()
        {
            InitializeComponent();
            main.NBMStart();
        }

        /// <summary>
        /// When the Message Header is filled in, the method checks the message type
        /// </summary>
        /// <param name="sender"></param>
        private void txtMsgID_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtMsgID.Text.Length == txtMsgID.MaxLength)
            {

                MsgType = main.ValidateMessageType(txtMsgID.Text);
                if (MsgType == "")
                {
                    txtMsgID.Clear();
                }

                if (!String.IsNullOrEmpty(MsgType))
                {
                    txtMsgBody.Text = message.SetMessageTitles(MsgType);
                    txtMsgBody.IsEnabled = true;
                    btnSubmit.IsEnabled = true;
                }
            }

        }

        /// <summary>
        /// When the submit button is clicked, it will process the message in the text box
        /// </summary>
        /// <param name="sender"></param>
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            Msg = txtMsgBody.Text;
            MsgHeader = txtMsgID.Text;
            main.ValidateMessage(Msg, MsgType, message.GetMessageText(Msg), MsgHeader, message.GetMessageSender(MsgType, Msg));
            txtMsgID.Clear();
            txtMsgBody.Clear();
        }
    }
}
