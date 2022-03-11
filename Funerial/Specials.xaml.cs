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
    /// Логика взаимодействия для Specials.xaml
    /// </summary>
    public partial class Specials : Window
    {
        maximEntities3 entity = new maximEntities3();
        public Specials()
        {
            InitializeComponent();
            var sale = entity.Special.ToList();
            Datagrid_Sale.ItemsSource = sale;

            Update();
        }
        private void Update()
        {
            var current = entity.Special.ToList();
            current = current.Where(p => p.Name.ToLower().Contains(TextBox_Search.Text.ToLower())).ToList();
            Datagrid_Sale.ItemsSource = current.OrderBy(p => p.Name).ToList();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AddSales_Butoon_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }
    }
}
