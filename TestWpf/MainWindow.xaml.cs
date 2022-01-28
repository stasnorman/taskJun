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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TestWpf.Model;

namespace TestWpf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<ClientModel> _client { get; set; }
        private ClientModel _selectedClient { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            GetClient();
        }

        public async void GetClient()
        {
            try
            {
                string url = "http://localhost:61512/api/Client";
                HttpClient client = new HttpClient();

                var response = await client.GetAsync(url);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    _client = JsonConvert.DeserializeObject<List<ClientModel>>(responseContent);
                    GridClients.ItemsSource = _client.ToList();
                }
            }
            catch (Exception er)
            {

                er.Message.ToString();
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddClient addClient = new AddClient(this);
            addClient.Show();
        }

        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_selectedClient != null)
                {
                    string url = $"http://localhost:61512/api/Client/{_selectedClient.Id}";
                    HttpClient client = new HttpClient();

                    var response = await client.DeleteAsync(url);
                    var responseContent = await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string message = JsonConvert.DeserializeObject<string>(responseContent);
                        MessageBox.Show($"{message}");

                        GetClient();
                    }
                    else
                    {
                        MessageBox.Show("Сбой в системе!");

                    }
                }
                else
                {
                    MessageBox.Show("Выберите клиента!");

                }
            }
            catch (Exception er)
            {

                er.Message.ToString();
            }
        }

        private void GridClients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedClient = GridClients.SelectedItem as ClientModel;
        }
    }
}
