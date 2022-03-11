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
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        maximEntities3 entities = new maximEntities3();
        public Authorization()
        {
            InitializeComponent();
        }
        private void entrance_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                var password = Password_Box.Text.Trim();
                var login = Login_Box.Text.Trim();

                int n = 1, key = 48;
                string s = Password_Box.Text;

                string s1 = "";
                string alf = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя., АБВГДЕЁЖЗИЙКЛМНОПРСТУФЧЦЧШЩЪЫЬЭБЯ0123456789abcdefghiklmnopqrstvwxyzABCDEFGHIKLMNOPRSTVWXYZ";
                int q_1 = alf.Length;
                for (int i = 0; i < s.Length; i++)
                {
                    for (int j = 0; j < alf.Length; j++)
                    {
                        if (s[i] == alf[j])
                        {
                            int name = j * n + key;

                            while (name >= q_1)
                                name -= q_1;

                            s1 = s1 + alf[name];
                        }
                    }
                }

                var logins = (from user in entities.Users where user.Login == login select user).First();
                var passwords = (from user in entities.Users where user.Password == s1 select user).First();

                if (logins != null)
                {
                    Otdel_Window.Users = logins;
                    this.DialogResult = true;
                }
            }
            catch
            {
                MessageBox.Show("Ошибка", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Кнопка для перехода к окно регистрации
            var window = new Registratoin();
            window.ShowDialog();
        }
    }
}
