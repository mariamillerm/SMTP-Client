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
using Microsoft.Win32;

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

        private bool isCorrectMail(String to, String body) {
            bool result = false;
            if (to != "" && body != "") {
                result = true;
            }
            return result;
        }

        private bool isCorrectLogin(String mail, String password) {
            if (mail == "" || password == "") {
                return false;
            }
            return true;
        }

        private void btnSendMail_Click(object sender, RoutedEventArgs e)
        {
            if (!isCorrectMail(text_To.Text, text_Data.Text)) {
                System.Windows.MessageBox.Show("Отсутствует содержание либо адрес получателя!", "Ошибка", MessageBoxButton.OK);
                return;
            }
            if (!isCorrectLogin(login.Text, password.Password))
            {
                System.Windows.MessageBox.Show("Отсутствуют аутентификационные данные!", "Ошибка", MessageBoxButton.OK);
                return;
            }

            MailAddress from = new MailAddress(login.Text, text_Name.Text);
            MailAddress to;
            try
            {
                 to = new MailAddress(text_To.Text);

            }
            catch (FormatException) {
                System.Windows.MessageBox.Show("Неверный адрес получателя!", "Ошибка", MessageBoxButton.OK);
                return;
            }

            MailMessage m = new MailMessage(from, to);
            m.Subject = text_Subject.Text;
            m.Body = text_Data.Text;
            //вложения
            //m.Attachments.Add(new Attachment("D://temlog.txt"));
            if (lb_Additions.Items.Count > 0) {
                for (int i = 0; i < lb_Additions.Items.Count; i++) {
                    m.Attachments.Add(new Attachment((String)lb_Additions.Items.GetItemAt(i)));
                }
            }
            m.IsBodyHtml = false;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 25);
            smtp.Credentials = new NetworkCredential(login.Text, password.Password); 
            smtp.EnableSsl = true;
            try
            {
                smtp.Send(m);
            }
            catch (Exception) {
                System.Windows.MessageBox.Show("Были введены неверные аутентификационные данные либо серверу не удалось установить соединение!", "Ошибка", MessageBoxButton.OK);
                return;
            }
            System.Windows.MessageBox.Show("Сообщение было отправлено!", "Операция прошла успешно", MessageBoxButton.OK);
            text_Data.Text = "";
            text_Subject.Text = "";
            lb_Additions.Items.Clear();
        }

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == true)
            {
                String filename = openFileDialog1.FileName;
                lb_Additions.Items.Add(filename);
            }
        }

        private void lb_Additions_KeyDown(object sender, KeyEventArgs e)
        {
            if (lb_Additions.SelectedIndex != -1 && Keyboard.IsKeyDown(Key.Delete))
            {
                lb_Additions.Items.RemoveAt(lb_Additions.SelectedIndex);
            }
        }
    }
}
