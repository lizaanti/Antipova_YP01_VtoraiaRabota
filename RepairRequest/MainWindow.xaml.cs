using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace RepairRequest
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RepairRequestsDBEntities dbContext;

        public MainWindow()
        {
            InitializeComponent();
            RequestsDataGrid.ItemsSource = RepairRequestsDBEntities.GetContext().Employees.ToList();
            RequestsDataGrid.ItemsSource = RepairRequestsDBEntities.GetContext().RepairRequests.ToList();
            RequestsDataGrid.ItemsSource = RepairRequestsDBEntities.GetContext().RequestComments.ToList();
            dbContext = new RepairRequestsDBEntities();
            LoadRequests();
        }

        private void LoadRequests()
        {
            // Получаем заявки из базы данных
            var requests = dbContext.RepairRequests.ToList();

            // Привязываем данные к DataGrid
            RequestsDataGrid.ItemsSource = requests;
        }

        private void AddRequest_Click(object sender, RoutedEventArgs e)
        {
            var newRequest = new RepairRequests
            {
                RequestDate = DateTime.Now,
                EquipmentName = "Ноутбук Lenovo",
                FaultType = "Не включается",
                Description = "Неисправность блока питания",
                ClientName = "ООО 'Техносервис'",
                Status = "В ожидании"
            };

            dbContext.RepairRequests.Add(newRequest);
            dbContext.SaveChanges();

            MessageBox.Show("Заявка добавлена!");
            LoadRequests();
        }

        private void EditRequest_Click(object sender, RoutedEventArgs e)
        {
            if (RequestsDataGrid.SelectedItem is RepairRequests selectedRequest)
            {
                selectedRequest.Status = "В работе";
                dbContext.SaveChanges();

                MessageBox.Show("Статус заявки обновлен!");
                LoadRequests();
            }
            else
            {
                MessageBox.Show("Выберите заявку для редактирования.");
            }
        }

        private void DeleteRequest_Click(object sender, RoutedEventArgs e)
        {
            if (RequestsDataGrid.SelectedItem is RepairRequests selectedRequest)
            {
                dbContext.RepairRequests.Remove(selectedRequest);
                dbContext.SaveChanges();

                MessageBox.Show("Заявка удалена!");
                LoadRequests();
            }
            else
            {
                MessageBox.Show("Выберите заявку для удаления.");
            }
        }

        private void Report_Click(object sender, RoutedEventArgs e)
        {
            // Подсчет общей статистики
            int totalRequests = dbContext.RepairRequests.Count();
            int completedRequests = dbContext.RepairRequests.Count(r => r.Status == "Выполнено");
            double averageCompletionTime = dbContext.RepairRequests
                .Where(r => r.Status == "Выполнено")
                .Select(r => DbFunctions.DiffDays(r.RequestDate, DateTime.Now))
                .Average() ?? 0;

            // Формируем текст отчета
            string reportMessage = $"Общее количество заявок: {totalRequests}\n" +
                                   $"Выполненных заявок: {completedRequests}\n" +
                                   $"Среднее время выполнения заявки: {averageCompletionTime:F2} дней";

            // Выводим отчет
            MessageBox.Show(reportMessage, "Отчет", MessageBoxButton.OK, MessageBoxImage.Information);
        }


    }
}
