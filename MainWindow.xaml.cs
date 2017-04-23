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
using System.Net.Mail;
using System.Net;

namespace SMTP_Client
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private bool isCorrect(String from, String to, String body, String password) {
            bool result = false;

            return result;
        } 

        private void btnSendMail_Click(object sender, RoutedEventArgs e)
        {
            
            MailAddress from = new MailAddress("maria.melnik.a@gmail.com", text_Name.Text);
            MailAddress to = new MailAddress(text_To.Text);

            MailMessage m = new MailMessage(from, to);
            m.Subject = text_Subject.Text;
            m.Body = text_Data.Text;
            m.IsBodyHtml = false;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 25);
            smtp.Credentials = new NetworkCredential("maria.melnik.a@gmail.com", "vfhbzvtkmybr1932289");
            smtp.EnableSsl = true;
            smtp.Send(m);
            System.Windows.MessageBox.Show("Сообщение было отправлено!", "Операция прошла успешно", MessageBoxButton.OK);
        }
    }
}
