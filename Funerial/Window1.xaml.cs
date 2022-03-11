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
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        maximEntities3 entity = new maximEntities3();
        public Window1()
        {
            InitializeComponent();
            var sale = entity.Svaz.ToList();
            Datagrid_Sale.ItemsSource = sale;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AddSales_Butoon_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
