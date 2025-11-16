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

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {
        public Window3()
        {
            InitializeComponent();
            FirstNumberBox.GotFocus += TextBox_GotFocus;
            SecondNumberBox.GotFocus += TextBox_GotFocus;
            FirstNumberBox.Focus();
            lastFocusedTextBox = FirstNumberBox;
        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            lastFocusedTextBox = (TextBox)sender;
        }
        private string currentOperation = ""; // Храним выбранную операцию
        private TextBox lastFocusedTextBox; // Храним последнее активное поле

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (lastFocusedTextBox != null)
            {
                lastFocusedTextBox.Text += "1";
                // фокус курсора 
                lastFocusedTextBox.Focus();
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            currentOperation = "+";
            SecondNumberBox.Focus();
            lastFocusedTextBox = SecondNumberBox;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            currentOperation = "-";
            SecondNumberBox.Focus();
            lastFocusedTextBox = SecondNumberBox;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            currentOperation = "*";
            SecondNumberBox.Focus();
            lastFocusedTextBox = SecondNumberBox;
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            currentOperation = "/";
            SecondNumberBox.Focus();
            lastFocusedTextBox = SecondNumberBox;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(FirstNumberBox.Text) || string.IsNullOrEmpty(SecondNumberBox.Text))
            {
                ResultBox.Text = "Введите числа!";
                return;
            }

            try
            {
                double num1 = double.Parse(FirstNumberBox.Text);
                double num2 = double.Parse(SecondNumberBox.Text);
                double result = 0;
                switch (currentOperation)
                {
                    case "+":
                        result = num1 + num2;
                        break;
                    case "-":
                        result = num1 - num2;
                        break;
                    case "*":
                        result = num1 * num2;
                        break;
                    case "/":
                        result = num1 / num2;
                        break;
                    default:
                        ResultBox.Text = "Выберите операцию!";
                        return;
                }

                ResultBox.Text = result.ToString();
            }
            catch
            {
                ResultBox.Text = "Ошибка!";
            }
        }
        private void FirstNumberBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void SecondNumberBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ResultBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}