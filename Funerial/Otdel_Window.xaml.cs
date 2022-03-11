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
    /// Логика взаимодействия для Otdel_Window.xaml
    /// </summary>
    public partial class Otdel_Window : Window
    {
        maximEntities3 entity = new maximEntities3();
        public static Users Users { get; set; }
        public Otdel_Window()
        {
            InitializeComponent();
            var curr = entity.Otdel.ToList();
            Otdel_ListBox.ItemsSource = curr;
            
            if (new Authorization().ShowDialog() == false)
            {
                this.Close();
            }
            Check((bool)Users.Role);

            Update();
        }
        public void Check(bool Role)
        {
            if (Role)
            {
                Button_Student.Visibility = Visibility.Visible;
                New_Otdel.Visibility = Visibility.Visible;
                Button_Special.Visibility = Visibility.Visible;
            }
            else
            {
                Button_Student.Visibility = Visibility.Hidden;
                New_Otdel.Visibility = Visibility.Hidden;
                Button_Special.Visibility = Visibility.Hidden;
            }
        }
        private void Update()
        {
            var current = entity.Otdel.ToList();
            current = current.Where(p => p.Name.ToLower().Contains(TextBox_Search.Text.ToLower())).ToList();
            Otdel_ListBox.ItemsSource = current.OrderBy(p => p.Name).ToList();
        }
        private void Otdel_ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected_Car = Otdel_ListBox.SelectedItem as Otdel;
            if (selected_Car != null)
            {
                TextBlock_Information.Text= selected_Car.Information;
               
            }
            else
            {
                
                Otdel_ListBox.SelectedIndex = -1;
                TextBlock_Information.Text = " ";
            }
        }

        private void Special_Button_Click(object sender, RoutedEventArgs e)
        {

            var window = new Specials();
            window.ShowDialog();
        }

        private void Button_Student_Click(object sender, RoutedEventArgs e)
        {
          
            var window = new Submission_documents();
            window.ShowDialog();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var window = new NewOtdel();
            window.ShowDialog();
        }

        private void Button_Special_Click(object sender, RoutedEventArgs e)
        {
            var window = new Add_Special();
            window.ShowDialog();
        }
    }
}
