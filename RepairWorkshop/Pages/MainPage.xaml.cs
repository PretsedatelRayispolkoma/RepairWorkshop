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
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            if (MainWindow.IDRole != 1)
            {
                AcceptBtn.Visibility = Visibility.Collapsed;
            }
            if(MainWindow.IDRole != 3)
            {
                EmployeeTab.Visibility = Visibility.Collapsed;
            }
            if(MainWindow.IDRole == 2)
            {
                RegistryTab.Visibility = Visibility.Collapsed;
                InvoiceTab.Visibility = Visibility.Collapsed;
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ToRepairBtn_Click_1(object sender, RoutedEventArgs e)
        {
            var selectedTechnic = TechnicDG.SelectedItem as Technic;

            if (selectedTechnic == null)
                return;

            Repair repair = new Repair();
            repair.TechnicId = selectedTechnic.Id;
            repair.DateOfHanding = DateTime.Now;
            repair.HandingEmployeeId = MainWindow.IDClient;

            try
            {
                MainWindow.db.Repair.Add(repair);
                MainWindow.db.SaveChanges();
            }
            catch
            {
                MessageBox.Show("Error");
            }
        }

        private void AcceptBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedRepair = ToRepairDG.SelectedItem as Repair;
            this.NavigationService.Navigate(new AddToRepairPage(selectedRepair));
        }

        private void ToRepairDG_Loaded(object sender, RoutedEventArgs e)
        {
            if(MainWindow.IDRole == 2)
            {
                ToRepairDG.ItemsSource = MainWindow.db.Repair.Where(p => p.HandingEmployeeId == MainWindow.IDClient && p.RepairEmployeeId == null).ToList();
            }
            else
            {
                ToRepairDG.ItemsSource = MainWindow.db.Repair.Where(p => p.RepairEmployeeId == null).ToList();
            }
        }

        private void OnRepairDG_Loaded(object sender, RoutedEventArgs e)
        {
            if (MainWindow.IDRole == 2)
            {
                OnRepairDG.ItemsSource = MainWindow.db.Repair.Where(p => p.HandingEmployeeId == MainWindow.IDClient && p.RepairEmployeeId != null).ToList();
            }
            else
            {
                OnRepairDG.ItemsSource = MainWindow.db.Repair.Where(p => p.RepairEmployeeId != null).ToList();
            }
        }

        private void TechnicDG_Loaded(object sender, RoutedEventArgs e)
        {
            TechnicDG.ItemsSource = MainWindow.db.Technic.ToList();
        }

        private void RegistryDG_Loaded(object sender, RoutedEventArgs e)
        {
            RegistryDG.ItemsSource = MainWindow.db.History.ToList();
        }

        private void InvoiceDG_Loaded(object sender, RoutedEventArgs e)
        {
            InvoiceDG.ItemsSource = MainWindow.db.Invoice.ToList();
        }

        private void EmployeeDG_Loaded(object sender, RoutedEventArgs e)
        {
            EmployeeDG.ItemsSource = MainWindow.db.Employee.Where(p => p.IsDismissed == false).ToList();
        }

        private void AllCB_Checked(object sender, RoutedEventArgs e)
        {
            if(AllCB.IsChecked == true)
                EmployeeDG.ItemsSource = MainWindow.db.Employee.ToList();
            else
                EmployeeDG.ItemsSource = MainWindow.db.Employee.Where(p => p.IsDismissed == false).ToList();

        }

        private void AllCB_Unchecked(object sender, RoutedEventArgs e)
        {
            if (AllCB.IsChecked == true)
                EmployeeDG.ItemsSource = MainWindow.db.Employee.ToList();
            else
                EmployeeDG.ItemsSource = MainWindow.db.Employee.Where(p => p.IsDismissed == false).ToList();
        }
    }
}
