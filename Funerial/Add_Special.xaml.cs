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
    /// Логика взаимодействия для Add_Special.xaml
    /// </summary>
    public partial class Add_Special : Window
    {
        maximEntities3 entity = new maximEntities3();
        public Add_Special()
        {
            InitializeComponent();
            var sale = entity.Special.ToList();
            ListBox_Spec.ItemsSource = sale;

            var currs = entity.Otdel.ToList();
            ComboBox_Otdel.ItemsSource = currs;

            Update();
        }
        private void Update()
        {
            var current = entity.Special.ToList();
            current = current.Where(p => p.Name.ToLower().Contains(TextBox_Search.Text.ToLower())).ToList();
            ListBox_Spec.ItemsSource = current.OrderBy(p => p.Name).ToList();
        }
        private void TextBox_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }

        private void Clear_Button_Click(object sender, RoutedEventArgs e)
        {
            ComboBox_Otdel.SelectedIndex = -1;
            TextBox_name.Text = "";

            ListBox_Spec.SelectedIndex = -1;
            ListBox_Spec.Items.Refresh();
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            var save = ListBox_Spec.SelectedItem as Special;
            if (TextBox_name.Text == "")
            {
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else
            {
                if (save == null)
                {
                    save = new Special();
                    entity.Special.Add(save);

                }
                save.Name = TextBox_name.Text.Trim();

                save.Id_Otdel = (ComboBox_Otdel.SelectedItem as Special).Id;


                entity.SaveChanges();
                ListBox_Spec.Items.Refresh();
                MessageBox.Show("Данные успешно сохранены!", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Delet_Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы действительно хотите удалить запись?", "Сообщение", MessageBoxButton.YesNoCancel, MessageBoxImage.Information);
            if (result == MessageBoxResult.Yes)
            {

                var delete_ = ListBox_Spec.SelectedItem as Special;
                if (delete_ != null)
                {
                    try
                    {
                        var exits = (from exit in entity.Documents where exit.Id_Spec == delete_.Id select exit).First();
                        MessageBox.Show("Запись удалить нельзя! Зарегестрирована запись данного объекта", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    catch
                    {
                        entity.Special.Remove(delete_);
                        entity.SaveChanges();
                        ListBox_Spec.SelectedIndex = -1;
                        MessageBox.Show("Данные удалены!", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                    }


                }
                else
                {
                    MessageBox.Show("Выберитед данные!", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void ListBox_Otdel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Получает элемент в текущем выделении
            var selected_ = ListBox_Spec.SelectedItem as Special;
            // Если значение не равно null, выводим информацию
            if (selected_ != null)
            {
                TextBox_name.Text = selected_.Name;

                ComboBox_Otdel.SelectedItem = (from car in entity.Otdel where car.Id == selected_.Id_Otdel select car).Single<Otdel>();


            }
        }

        private void ComboBox_Otdel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
