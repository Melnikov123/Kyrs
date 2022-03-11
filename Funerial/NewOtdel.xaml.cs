using Microsoft.Win32;
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

using System.IO;

namespace Funerial
{
    /// <summary>
    /// Логика взаимодействия для NewOtdel.xaml
    /// </summary>
    public partial class NewOtdel : Window
    {
        maximEntities3 entity = new maximEntities3();
        public NewOtdel()
        {
         
            InitializeComponent();
            var curr = entity.Otdel.ToList();
            ListBox_Otdel.ItemsSource = curr;

            Update();
        }

        private void New_Opisanie_Button_Click(object sender, RoutedEventArgs e)
        {         
            OpenFileDialog ofd = new OpenFileDialog(); // создаём процесс  
            ofd.ShowDialog(); // открываем проводник    
            if (ofd.FileName != "") // проверка на выбор файла  
            {
                StreamReader sr = new StreamReader(ofd.FileName);
                while (!sr.EndOfStream) // перебираем строки, пока они не закончены       
                {
                    
                    TextBox_Img.Text= sr.ReadLine();
                }
            }
            else MessageBox.Show("Файл не выбран");
        }

        private void Clear_Button_Click(object sender, RoutedEventArgs e)
        {
            TextBox_Img.Text = "";
            TextBox_name.Text = "";

            ListBox_Otdel.SelectedIndex = -1;
            ListBox_Otdel.Items.Refresh();
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            var save = ListBox_Otdel.SelectedItem as Otdel;
            if (TextBox_name.Text == "" || TextBox_Img.Text == "" )
            {
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else
            {
                if (save == null)
                {
                    save = new Otdel();
                    entity.Otdel.Add(save);
                   
                }
                save.Name = TextBox_name.Text.Trim();
               
                save.Information = TextBox_Img.Text.Trim();

                entity.SaveChanges();
                ListBox_Otdel.Items.Refresh();
                MessageBox.Show("Данные успешно сохранены!", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Delet_Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы действительно хотите удалить запись?", "Сообщение", MessageBoxButton.YesNoCancel, MessageBoxImage.Information);
            if (result == MessageBoxResult.Yes)
            {
              
                var delete_ = ListBox_Otdel.SelectedItem as Otdel;
                if (delete_ != null)
                {
                    try
                    {
                        var exits = (from exit in entity.Special where exit.Id_Otdel == delete_.Id select exit).First();
                        MessageBox.Show("Запись удалить нельзя! Зарегестрирована запись данного объекта", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    catch
                    {
                        entity.Otdel.Remove(delete_);
                        entity.SaveChanges();
                        ListBox_Otdel.SelectedIndex = -1;
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
            var selected_service = ListBox_Otdel.SelectedItem as Otdel;
            // Если значение не равно null, выводим информацию
            if (selected_service != null)
            {
                TextBox_name.Text = selected_service.Name;
              
                
                TextBox_Img.Text = selected_service.Information;

            }
        }
        private void Update()
        {
            var current = entity.Otdel.ToList();
            current = current.Where(p => p.Name.ToLower().Contains(TextBox_Search.Text.ToLower())).ToList();
            ListBox_Otdel.ItemsSource = current.OrderBy(p => p.Name).ToList();
        }
        private void TextBox_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }

        private void TextBox_Img_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
