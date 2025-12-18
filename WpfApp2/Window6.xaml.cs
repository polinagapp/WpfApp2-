using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для Window6.xaml
    /// </summary>
    public partial class Window6 : Window
    {
        //объект, который хранит ссылку на метод (или несколько методов)
        //Объявление делегата
        public delegate void TaskDelegate(string taskName);

        //Использование встроенного делегата Action
        private Action<string> showMessage;

        //Использование делегата
        private TaskDelegate taskAdded;

        //Коллекция задач
        private ObservableCollection<string> tasks = new ObservableCollection<string>();

        public Window6()
        {
            InitializeComponent();
            showMessage = msg => MessageBox.Show(msg);
            taskAdded = name => Console.WriteLine($"Задача добавлена: {name}");
            // Настройка ListBox
            TasksListBox.ItemsSource = tasks;
            AddButton.Click += AddButton_Click;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string taskName = TaskTextBox.Text;

            if (!string.IsNullOrWhiteSpace(taskName))
            {
                //Вызов делегата Action
                showMessage?.Invoke($"Добавлено: {taskName}");

                //Вызов делегата
                taskAdded?.Invoke(taskName);

                //Добавление задачи
                tasks.Add(taskName);
                TaskTextBox.Text = "";
            }
        }
    }
}