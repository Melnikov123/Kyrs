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
    /// Логика взаимодействия для Registratoin.xaml
    /// </summary>
    public partial class Registratoin : Window
    {
        // Подключение БД
        maximEntities3 entities = new maximEntities3();
        public Registratoin()
        {
            InitializeComponent();
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            // Запись в переменную данных с TextBox
            //Trim()-Для удаления пробелов
            string login = Login_TextBox.Text.Trim();
            string password = Password_TextBox.Text.Trim();
            string repit_password = Repit_Password_TextBox.Text.Trim();
          
            if (Login_TextBox.Text == "" || Password_TextBox.Text == "")
            {
                MessageBox.Show("Ошибка");
            }
            else
            {
                var add = new Users();

                int n = 1, key = 48;
                string s = Password_TextBox.Text;
                string s1 = "";
                string alf = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя., АБВГДЕЁЖЗИЙКЛМНОПРСТУФЧЦЧШЩЪЫЬЭБЯ0123456789abcdefghiklmnopqrstvwxyzABCDEFGHIKLMNOPRSTVWXYZ";
                int q_1 = alf.Length;
                for (int i = 0; i < s.Length; i++)//цикл перебора букв шифруемого слова
                {
                    for (int j = 0; j < alf.Length; j++)//цикл сравнения каждой бкувы с алфавитом
                    {
                        if (s[i] == alf[j]) // в случае совпадения создаем переменную, где храню номер буквы со сдвигом
                        {
                            int name = j * n + key;//номер буквы+сдвиг в переменную

                            while (name >= q_1)//чтобы переменная не уходила за рамки алфавита
                                name -= q_1;

                            s1 = s1 + alf[name];//занесли зашифрованную букву в переменную для ее хранения
                        }
                    }
                }

                
                add.Login = login;
                add.Password = s1;
                entities.Users.Add(add);
                entities.SaveChanges();

                MessageBox.Show("Регистрация прошла успешно", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
       

        private void Login_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}

