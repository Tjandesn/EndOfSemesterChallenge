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
using Newtonsoft.Json;
using SemesterChallenge_ClassLibrary;

namespace SemesterChallenge_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Procedure> AllProcedures { set; get; }

        public MainWindow()
        {
            InitializeComponent();

            FillProcedures();
        }

        public async Task FillProcedures()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://semesterchallenge2.azurewebsites.net/api/ProcedureViews");

            string procedure = "";

            HttpResponseMessage response = await client.GetAsync(client.BaseAddress);
            if (response.IsSuccessStatusCode)
            {
                procedure = await response.Content.ReadAsStringAsync();
            }

            AllProcedures = JsonConvert.DeserializeObject<List<Procedure>>(procedure);

            dgProcedures.ItemsSource = AllProcedures;
        }
    }
}
