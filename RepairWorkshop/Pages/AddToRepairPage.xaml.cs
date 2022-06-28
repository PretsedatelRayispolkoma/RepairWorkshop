using RepairWorkshop.DataBase;
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

namespace RepairWorkshop.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddToRepairPage.xaml
    /// </summary>
    public partial class AddToRepairPage : Page
    {
        private Repair _currentRepair;

        private List<Part> Parts = new List<Part>();
        private List<Part> AddedParts = new List<Part>();

        public AddToRepairPage(Repair repair)
        {
            InitializeComponent();

            if(repair != null)
            {
                _currentRepair = MainWindow.db.Repair.Attach(repair);
                DataContext = _currentRepair;
            }

            TypeOfRepairCB.ItemsSource = MainWindow.db.TypeOfRepair.ToList();
            TypeOfRepairCB.DisplayMemberPath = "Name";

            List<int> days = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14,
            15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30};
            DaysCB.ItemsSource = days;

            Parts = MainWindow.db.Part.ToList();
            PartsLV.ItemsSource = Parts;

            AddedPartsLV.ItemsSource = AddedParts;
        }

        private void AcceptBtn_Click(object sender, RoutedEventArgs e)
        {
            var currentType = TypeOfRepairCB.SelectedItem as TypeOfRepair;
            int selectedDays = Convert.ToInt32(DaysCB.SelectedItem);

            _currentRepair.RepairEmployeeId = MainWindow.IDClient;
            _currentRepair.Time = selectedDays;
            _currentRepair.TypeOfRepairId = currentType.Id;

            History history = new History();
            history.TechnicId = _currentRepair.Technic.Id;
            history.BeginDate = DateTime.Now;
            history.EndDate = history.BeginDate.AddDays(selectedDays);
            history.DivisionId = 1;

            if(AddedParts.Any())
            {
                foreach(var part in AddedParts)
                {
                    Invoice invoice = new Invoice();
                    Random rnd = new Random();
                    invoice.Amount = rnd.Next(500, 5000);
                    invoice.Date = DateTime.Now.AddDays(selectedDays / 2);
                    invoice.PartId = part.Id;
                    MainWindow.db.Invoice.Add(invoice);
                }
            }

            MainWindow.db.History.Add(history);

            try
            {
                MainWindow.db.SaveChanges();
                this.NavigationService.GoBack();
            }
            catch
            {
                MessageBox.Show("Error");
            }
        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedPart = PartsLV.SelectedItem as Part;
            Parts.Remove(selectedPart);
            AddedParts.Add(selectedPart);

            PartsLV.ItemsSource = Parts;
            AddedPartsLV.ItemsSource = AddedParts;

            PartsLV.Items.Refresh();
            AddedPartsLV.Items.Refresh();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedPart = AddedPartsLV.SelectedItem as Part;
            AddedParts.Remove(selectedPart);
            Parts.Add(selectedPart);

            PartsLV.ItemsSource = Parts;
            AddedPartsLV.ItemsSource = AddedParts;

            PartsLV.Items.Refresh();
            AddedPartsLV.Items.Refresh();
        }
    }
}
