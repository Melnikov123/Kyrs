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

namespace Funerial
{
    /// <summary>
    /// Логика взаимодействия для Students.xaml
    /// </summary>
    public partial class Students : Window
    {
        maximEntities3 entity =new maximEntities3();
        public Students()
        {
            InitializeComponent();
            var curr = entity.Abityrient.ToList();
            GridStudent.ItemsSource = curr;
        }

        private void GridStudent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Получает элемент в текущем выделении
            var selected_service = GridStudent.SelectedItem as Abityrient ;
            // Если значение не равно null, выводим информацию
            if (selected_service != null)
            {
                TextBox_name.Text = selected_service.Name;


                TextBox_Patronymic.Text = selected_service.Patronymic;
                TextBox_Surname.Text = selected_service.Surname;
                Data_Box.Text = selected_service.Data_spach;

            }
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            var save = GridStudent.SelectedItem as Abityrient;
            if (TextBox_name.Text == "" || TextBox_Patronymic.Text == ""|| TextBox_Surname.Text=="")
            {
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else
            {
                if (save == null)
                {
                    save = new Abityrient();
                    entity.Abityrient.Add(save);

                }
                save.Name = TextBox_name.Text.Trim();

                save.Patronymic = TextBox_Patronymic.Text.Trim();
                save.Surname = TextBox_Surname.Text.Trim();
                save.Data_spach = Data_Box.Text.Trim();
                entity.SaveChanges();
                GridStudent.Items.Refresh();
                MessageBox.Show("Данные успешно сохранены!", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Delet_Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы действительно хотите удалить запись?", "Сообщение", MessageBoxButton.YesNoCancel, MessageBoxImage.Information);
            if (result == MessageBoxResult.Yes)
            {

                var delete_services = GridStudent.SelectedItem as Abityrient;
                if (delete_services != null)
                {
                    entity.Abityrient.Remove(delete_services);
                    entity.SaveChanges();
                    GridStudent.SelectedIndex = -1;
                    MessageBox.Show("Данные удалены!", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Выберите данные!", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void Clear_Button_Click(object sender, RoutedEventArgs e)
        {
            TextBox_Surname.Text = "";
            TextBox_Patronymic.Text = "";
            TextBox_name.Text = "";
            GridStudent.SelectedIndex = -1;
        }

        private void TextBox_Search_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
