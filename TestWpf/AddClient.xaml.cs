using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using TestWpf.Model;

namespace TestWpf
{
    /// <summary>
    /// Логика взаимодействия для AddClient.xaml
    /// </summary>
    public partial class AddClient : Window
    {
        private MainWindow _main { get; set; }
        public AddClient(MainWindow mainWindow)
        {
            InitializeComponent();
            _main = mainWindow;
        }

        private async void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] fullName = TxtName.Text.Split(' ');

                string url = "http://localhost:61512/api/Client";
                HttpClient client = new HttpClient();

                var request = new ClientModel()
                {
                    FirstName = fullName[1],
                    MiddleName = fullName[0],
                    LastName = fullName[2],
                    Login = TxtLogin.Text,
                    Password = TxtPassword.Text,
                    DateBirth = (DateTime)PickDate.SelectedDate,
                    Email = TxtEmail.Text,
                    Phone = TxtPhone.Text
                };

                var requestJson = JsonConvert.SerializeObject(request);
                StringContent sc = new StringContent(requestJson, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(url, sc);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    MessageBox.Show("Данные добавлены!");
                    _main.GetClient();

                    Close();
                }
                else
                {
                    MessageBox.Show("Сбой в системе!");

                }
            }
            catch (Exception er)
            {

                er.Message.ToString();
            }
        }
    }
}
