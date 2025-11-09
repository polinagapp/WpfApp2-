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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Button1_Click(Object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hello world");
            Window1 window1 = new Window1();
            window1.Show();
            this.Close();
            MessageBox.Show("Hello world");
            MainWindow MainWindow = new MainWindow();
            MainWindow.Show();
            this.Close();
        }
        private void OpenSecondWindowBtn_Click(object sender, RoutedEventArgs e)
        {
            // 1. Создаем второе окно и передаем туда имя пользователя
            Window2 Window2 = new Window2(UserNameTextBox.Text);

            // 2. Открываем второе окно как модальное (оно заблокирует главное окно)
            Window2.ShowDialog();

            // 3. Этот код выполнится только когда второе окно закроется
            if (Window2.ReturnedData != null)
            {
                // 4. Получаем данные из второго окна
                ResultTextBlock.Text = Window2.ReturnedData;
            }
        }
    }
}
