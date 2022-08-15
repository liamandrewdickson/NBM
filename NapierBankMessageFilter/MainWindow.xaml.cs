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
        private string msgBody = "";
        Main main = new Main();

        public MainWindow()
        {
            InitializeComponent();

        }

        /// <summary>
        /// When the Message ID is filled in, the method checks the message type
        /// </summary>
        private void txtMsgID_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtMsgID.Text.Length == txtMsgID.MaxLength)
            {
                
                    msgType = main.ValidateMessageType(txtMsgID.Text);
                    if (msgType == "")
                    {
                        txtMsgID.Clear();
                    }
                
                if (!String.IsNullOrEmpty(msgType))
                {
                    txtMsgBody.Text = main.GetMessageBody(msgType);
                    txtMsgBody.IsEnabled = true;
                    btnSubmit.IsEnabled = true;
                }
            }
            
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            msgBody = txtMsgBody.Text;
            main.ValidateMessageLimit(msgType, msgBody);
        }
    }
}
