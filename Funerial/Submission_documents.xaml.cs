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
using System.Windows.Shapes;

using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;

namespace Funerial
{
    /// <summary>
    /// Логика взаимодействия для Submission_documents.xaml
    /// </summary>
    public partial class Submission_documents : System.Windows.Window
    {
        maximEntities3 entity = new maximEntities3();
        public Submission_documents()
        {
            InitializeComponent();
            var sale = entity.Documents.ToList();
            Grid_documents.ItemsSource = sale;

            var abt = entity.Abityrient.ToList();
            TextBox_surname.ItemsSource = abt;

            var spec = entity.Special.ToList();
            TextBox_spec.ItemsSource = spec;
            Update();
        }

        private void TextBox_name1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void Sum()
        {
            int sum = 0;
            foreach (var item in Grid_documents.Items)
            {
                int modelId = (int)(item as Documents).Id_Abityrient;
                int g= (int)(from model in entity.Abityrient where model.Id == modelId select model.Id).FirstOrDefault();
                sum++;
            }
            Label_Sum.Content = sum.ToString();
        }

        private void Update()
        {          
            var current = entity.Documents.ToList();
            var a = current = current.Where(p => p.Id_Spec.ToString().Contains(TextBox_Search.Text.ToLower())).ToList();
            Grid_documents.ItemsSource = current.OrderBy(p => p.Id_Spec).ToList();
            Sum();
        }

        private void Grid_documents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Получает элемент в текущем выделении
            var selected_service = Grid_documents.SelectedItem as Documents;
            // Если значение не равно null, выводим информацию
            if (selected_service != null)
            {
                TextBox_spec.SelectedItem = (from car in entity.Special where car.Id == selected_service.Id_Spec select car).Single<Special>();
                TextBox_surname.SelectedItem = (from car in entity.Abityrient where car.Id == selected_service.Id_Abityrient select car).Single<Abityrient>();
            }
        }
        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }    

        private void New_Abityrients_Click(object sender, RoutedEventArgs e)
        {
          
            var sale = new Students();
          

            sale.ShowDialog();
        }

        private void Export_Button_Click(object sender, RoutedEventArgs e)
        {
            Excel.Application excel = new Excel.Application();
            excel.Visible = true;
            Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
            Worksheet sheet1 = (Worksheet)workbook.Sheets[1];

            for (int j = 0; j < Grid_documents.Columns.Count; j++)
            {
                Range myRange = (Range)sheet1.Cells[1, j + 1];
                sheet1.Cells[1, j + 1].Font.Bold = true;
                sheet1.Columns[j + 1].ColumnWidth = 15;
                myRange.Value2 = Grid_documents.Columns[j].Header;
            }
            for (int i = 0; i < Grid_documents.Columns.Count; i++)
            {
                for (int j = 0; j < Grid_documents.Items.Count; j++)
                {
                    TextBlock b = Grid_documents.Columns[i].GetCellContent(Grid_documents.Items[j]) as TextBlock;
                    Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[j + 2, i + 1];
                    myRange.Value2 = b.Text;
                }
            }
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            var player = Grid_documents.SelectedItem as Documents;
            // Проверка а ввод пустых полей
            if (TextBox_surname.Text == "" || TextBox_spec.Text == "")
            {
                MessageBox.Show("Заполни все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                //Если значение player == null, данные добавляются в БД
                if (player == null)
                {

                    player = new Documents();
                    entity.Documents.Add(player);
                    Grid_documents.Items.Refresh();

                }
                player.Id_Abityrient = (TextBox_surname.SelectedItem as Abityrient).Id;
                player.Id_Spec = (TextBox_spec.SelectedItem as Special).Id;
                
                entity.SaveChanges();
                Grid_documents.Items.Refresh();

                MessageBox.Show("Данные сохранены!", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }
    }
}
