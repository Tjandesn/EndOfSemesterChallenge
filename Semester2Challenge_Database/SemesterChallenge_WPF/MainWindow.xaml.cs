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
        public List<Treatment> AllTreatments { set; get; }
        public List<Treatment> FilteredTreatments { set; get; }

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

        private void dgProcedures_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Procedure selectedProcedure = (Procedure)dgProcedures.SelectedItem;

            int procID = selectedProcedure.ProcedureID;

            FillTreatments(procID);
        }

        public async Task FillTreatments( int procID)
        {
            FilteredTreatments = new List<Treatment>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://semesterchallenge2.azurewebsites.net/api/TreatmentViews");

            string treatment = "";

            HttpResponseMessage response = await client.GetAsync(client.BaseAddress);
            if (response.IsSuccessStatusCode)
            {
                treatment = await response.Content.ReadAsStringAsync();
            }

            AllTreatments = JsonConvert.DeserializeObject<List<Treatment>>(treatment);

            var filtered = AllTreatments.Where(tr => tr.ProcedureID == procID);
            foreach (var item in filtered)
            {
                FilteredTreatments.Add(item);
            }

            dgTreatments.ItemsSource = FilteredTreatments;
        }
    }
}
