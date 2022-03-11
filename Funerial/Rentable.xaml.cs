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
    /// Логика взаимодействия для Rentable.xaml
    /// </summary>
    public partial class Rentable : Window
    {
        maximEntities3 entity = new maximEntities3();
        public Rentable()
        {
            InitializeComponent();
            var curr = entity.Introductory.ToList();
            ListBox_predmet.ItemsSource = curr;
            Update();
        }

        private void TextBox_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }
        private void Update()
        {
            var current = entity.Introductory.ToList();

            current = current.Where(p => p.Name.ToLower().Contains(TextBox_Search.Text.ToLower())).ToList();

            ListBox_predmet.ItemsSource = current.OrderBy(p => p.Name).ToList();

        }
        private void ListBox_Service_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected_Car = ListBox_predmet.SelectedItem as Introductory;
            if (selected_Car != null)
            {
                TextBox_Name.Text = selected_Car.Name;

            }
            else
            {
                TextBox_Name.Text = "";

                ListBox_predmet.SelectedIndex = -1;
            }
        }

        private void New_Button_Click(object sender, RoutedEventArgs e)
        {
            TextBox_Name.Text = "";

            ListBox_predmet.SelectedIndex = -1;
        
        }

        private void Dell_Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы действительно хотите удалить запись?", "Сообщение", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (result == MessageBoxResult.Yes)
            {
                var deleted = ListBox_predmet.SelectedItem as Introductory;
                if (deleted != null)
                {
                    try
                    {
                        var exits = (from exit in entity.Svaz where exit.Id_Predmet == deleted.Id select exit).First();
                        MessageBox.Show("Запись удалить нельзя! Зарегестрирована запись данного бренда", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    catch
                    {
                        entity.Introductory.Remove(deleted);
                        entity.SaveChanges();
                        ListBox_predmet.Items.Refresh();

                        MessageBox.Show("Запись удалена", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                        
                        TextBox_Name.Text = "";
                       
                        ListBox_predmet.SelectedIndex = -1;
                    }

                }
            }
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            var player = ListBox_predmet.SelectedItem as Introductory;
            // Проверка а ввод пустых полей
            if (TextBox_Name.Text == "")
            {
                MessageBox.Show("Заполни все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                //Если значение player == null, данные добавляются в БД
                if (player == null)
                {

                    player = new Introductory();
                    entity.Introductory.Add(player);
                    ListBox_predmet.Items.Refresh();

                }
                player.Name = TextBox_Name.Text.Trim();

                entity.SaveChanges();
                ListBox_predmet.Items.Refresh();

                MessageBox.Show("Данные сохранены!", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
