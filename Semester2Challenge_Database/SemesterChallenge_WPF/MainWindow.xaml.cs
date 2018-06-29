using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
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
        public List<Owner> AllOwners { set; get; }

        public NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public MainWindow()
        {
            InitializeComponent();

            FillProcedures();

            FillOwners();
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

        public async Task FillOwners()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://semesterchallenge2.azurewebsites.net/api/OwnerViews");

            string owner = "";

            HttpResponseMessage response = await client.GetAsync(client.BaseAddress);
            if (response.IsSuccessStatusCode)
            {
                owner = await response.Content.ReadAsStringAsync();
            }

            AllOwners = JsonConvert.DeserializeObject<List<Owner>>(owner);

            dgOwners.ItemsSource = AllOwners;
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

        private void dgOwners_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Owner selectedOwner = (Owner)dgOwners.SelectedItem;

            int ownID = selectedOwner.OwnerID;
            string firstN = selectedOwner.GivenName;
            string lastN = selectedOwner.Surname;
            string phone = selectedOwner.Phone;

            txtID.Text = Convert.ToString(ownID);
            txtFirst.Text = firstN;
            txtLast.Text = lastN;
            txtPhone.Text = phone;
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var regex1 = new Regex("^[0-9]+$");
                var regex2 = new Regex("^[a-zA-Z]+$");
                if (!regex1.IsMatch(txtID.Text))
                {
                    throw new ValidationFailureException("Validation Failed!");
                }
                else if (!regex2.IsMatch(txtFirst.Text))
                {
                    throw new ValidationFailureException("Validation Failed!");
                }
                else if (!regex2.IsMatch(txtLast.Text))
                {
                    throw new ValidationFailureException("Validation Failed!");
                }
                else if (!regex1.IsMatch(txtPhone.Text))
                {
                    throw new ValidationFailureException("Validation Failed!");
                }
                else
                {
                    Owner newOwner = new Owner();

                    newOwner.OwnerID = Convert.ToInt32(txtID.Text);
                    newOwner.GivenName = txtFirst.Text;
                    newOwner.Surname = txtLast.Text;
                    newOwner.Phone = txtPhone.Text;

                    CreateOwner(newOwner);
                }
            }
            catch (ValidationFailureException ex)
            {
                MessageBox.Show("Validation Failed!");
                logger.Debug("ValidationFailureException");
            }
        }

        public async Task CreateOwner(Owner newOwner)
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("http://semesterchallenge2.azurewebsites.net/api/OwnerViews");

            string owner = JsonConvert.SerializeObject(newOwner, Formatting.None);

            var content = new StringContent(owner, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("owners", content);
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var regex1 = new Regex("^[0-9]+$");
                var regex2 = new Regex("^[a-zA-Z]+$");
                if (!regex1.IsMatch(txtID.Text))
                {
                    throw new ValidationFailureException("Validation Failed!");
                }
                else if (!regex2.IsMatch(txtFirst.Text))
                {
                    throw new ValidationFailureException("Validation Failed!");
                }
                else if (!regex2.IsMatch(txtLast.Text))
                {
                    throw new ValidationFailureException("Validation Failed!");
                }
                else if (!regex1.IsMatch(txtPhone.Text))
                {
                    throw new ValidationFailureException("Validation Failed!");
                }
                else
                {
                    Owner updateOwner = new Owner();

                    updateOwner.OwnerID = Convert.ToInt32(txtID.Text);
                    updateOwner.GivenName = txtFirst.Text;
                    updateOwner.Surname = txtLast.Text;
                    updateOwner.Phone = txtPhone.Text;

                    UpdateOwner(updateOwner);
                }
            }
            catch (ValidationFailureException ex)
            {
                MessageBox.Show("Validation Failed!");
                logger.Debug("ValidationFailureException");
            }
        }

        public async Task UpdateOwner(Owner updateOwner)
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("http://semesterchallenge2.azurewebsites.net/api/");

            string owner = JsonConvert.SerializeObject(updateOwner, Formatting.None);

            var content = new StringContent(owner, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PutAsync("owners/" + updateOwner.OwnerID, content);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Owner deleteOwner = new Owner();

            deleteOwner.OwnerID = Convert.ToInt32(txtID.Text);
            deleteOwner.GivenName = txtFirst.Text;
            deleteOwner.Surname = txtLast.Text;
            deleteOwner.Phone = txtPhone.Text;

            DeleteOwner(deleteOwner);
        }

        public async Task DeleteOwner(Owner deleteOwner)
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("http://semesterchallenge2.azurewebsites.net/api/");

            string owner = JsonConvert.SerializeObject(deleteOwner, Formatting.None);

            var content = new StringContent(owner, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.DeleteAsync("owners/" + deleteOwner.OwnerID);
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            FillOwners();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            logger.Trace("Application Launched");
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            logger.Trace("Application Exited");
        }
    }
}
