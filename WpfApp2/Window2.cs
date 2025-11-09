using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp2
{
    public partial class Window2 : Window
    {
        // Свойство для хранения данных, которые вернем в главное окно
        public string ReturnedData { get; set; }
        

        public Window2(string userName)
        {
            InitializeComponent();
            // Показываем полученное имя пользователя
            UserNameTextBlock.Text = $"Привет, {userName}!";
        }
    
     private void ReturnWithDataBtn_Click(object sender, RoutedEventArgs e)
     {
            // Сохраняем данные из текстового поля
            ReturnedData = DataTextBox.Text;

            // Закрываем окно
            this.Close();
     }
        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            // Просто закрываем окно без данных
            this.Close();
        }
    }
}
