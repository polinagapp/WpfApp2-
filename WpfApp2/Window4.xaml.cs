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

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для Window4.xaml
    /// </summary>
    public partial class Window4 : Window
    {
        // Поле для хранения списка вопросов теста
        private List<Question> questions;
        // Поле для хранения индекса текущего вопроса (начинается с 0)
        private int currentQuestionIndex = 0;
        //для подсчета правильных ответов
        private int score = 0;
        public Window4()
        {
            InitializeComponent();
            InitializeQuestions();
            SetupInitialVisibility();
        }
        private void SetupInitialVisibility()
        {
            // Изначально скрываем элементы теста
            ProgressBar.Visibility = Visibility.Collapsed;
            NextButton.Visibility = Visibility.Collapsed;
            SubmitButton.Visibility = Visibility.Collapsed;

            // Border с вопросами тоже скрываем
            var border = FindVisualChild<Border>(this, "TestBorder");
            if (border != null)
                border.Visibility = Visibility.Collapsed;
        }

        private void InitializeQuestions()
        {
            //новый список для хранения вопросов
            questions = new List<Question>
            {
                // Вопросы с одним вариантом ответа (RadioButton)
                new Question(
                    "Кто из студентов группы ПР1 - настоящий каспер?",
                    new List<string> { "Филиппенко Мирослава", "Артамкин Никита ", "Шутков Никита", "Картешкин Михаил" },
                    2, // Шутков
                    QuestionType.SingleChoice
                ),
                new Question(
                    "Сколько хромосом у человека?",
                    new List<string> {
                        "46",
                        "17",
                        "65",
                        "23"
                    },
                    0,
                    QuestionType.SingleChoice
                ),
                new Question(
                    "А у мамы?",
                    new List<string> { "0", "она лучшая", "-0", "нет" },
                    1, // "она лучшая"
                    QuestionType.SingleChoice
                ),
                new Question(
                    "Что такое Git?",
                    new List<string> {
                        "Система контроля версий",
                        "Язык программирования",
                        "База данных",
                        "Фреймворк"
                    },
                    0, // Система контроля версий
                    QuestionType.SingleChoice
                ),
                new Question(
                    "Какой модификатор доступа является самым таким ограничивающим в C#?",
                    new List<string> { "public", "private", "protected", "internal" },
                    1, // private
                    QuestionType.SingleChoice
                ),

                // Вопросы с несколькими вариантами ответа (CheckBox)
                new Question(
                    "Какой зз у Мишы?",
                    new List<string> { "колесница", "лев", "дева(ужас)", "C#" },
                    new List<int> { 0, 2}, // колесница, дева, сишаррп
                    QuestionType.MultipleChoice
                ),
                new Question(
                    "Самые худшие дисциплины...",
                    new List<string> { "проекты", "англ", "кураторский", "моделирование данных", "физра" },
                    new List<int> { 0, 2, 3 }, // проекты, кураторский, моделирование
                    QuestionType.MultipleChoice
                ),
                new Question(
                    "Миша заболел, придет ли он на пары?",
                    new List<string> {
                        "да",
                        "однозначно",
                        "да, но непонятно зачем",
                        "он сказал, что придет!"
                    },
                    new List<int> { 0, 1, 2, 3 }, // Все варианты
                    QuestionType.MultipleChoice
                ),
                new Question(
                    "Какие из следующих технологий используются в веб-разработке?",
                    new List<string> { "HTML", "SQL", "CSS", "XML" },
                    new List<int> { 0, 2 }, // HTML, CSS
                    QuestionType.MultipleChoice
                ),
                new Question(
                    "Что из перечисленного является типами данных в C#?",
                    new List<string> { "int", "string", "C++"},
                    new List<int> { 0, 1 }, // int, string
                    QuestionType.MultipleChoice
                ),
                new Question(
                    "Получил ли Некит статую от Мистера Биста?",
                    new List<string> { "нет", "нет, но близко", "даааа", "не понравилась, продал(" },
                    new List<int> { 0, 1}, // нет, нет, но близко
                    QuestionType.MultipleChoice
                ),
                new Question(
                    "Самые кайфовые пары...",
                    new List<string> {
                        "ООП C#",
                        "физра",
                        "моделирование",
                        "Алекс"
                    },
                    new List<int> { 0, 3 }, // с#, алекс
                    QuestionType.MultipleChoice
                ),
                new Question(
                    "Какой Миша самый крутой?",
                    new List<string> { "Стекляшка", "Сонный", "Майкл", "когда не постирал вещи.." },
                    new List<int> { 0, 1, 3 }, // стекляшка, сонный, не постирал вещи
                    QuestionType.MultipleChoice
                ),
                new Question(
                    "Кока кола может быть...",
                    new List<string> {
                        "доброй",
                        "китайской",
                        "из сша(запрещено в рф)",
                        "белой"
                    },
                    new List<int> { 0, 1 }, //доброй, китайской
                    QuestionType.MultipleChoice
                )
            };
        }

        // Метод для поиска визуальных элементов по имени
        private T FindVisualChild<T>(DependencyObject parent, string childName) where T : FrameworkElement
        {
            if (parent == null) return null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i) as FrameworkElement;
                if (child != null)
                {
                    if (child is T && child.Name == childName)
                        return (T)child;

                    var result = FindVisualChild<T>(child, childName);
                    if (result != null)
                        return result;
                }
            }
            return null;
        }

        private void ShowQuestion(int index)
        {
            QuestionPanel.Children.Clear();

            if (index < 0 || index >= questions.Count)
                return;

            var question = questions[index];

            // Отображение номера вопроса
            var questionNumber = new TextBlock
            {
                Text = $"Вопрос {index + 1} из {questions.Count}",
                FontWeight = FontWeights.Bold,
                FontSize = 14,
                Margin = new Thickness(0, 0, 0, 15),
                Foreground = Brushes.Gray
            };
            QuestionPanel.Children.Add(questionNumber);

            // Текст вопроса
            var questionText = new TextBlock
            {
                Text = question.Text,
                FontSize = 16,
                FontWeight = FontWeights.SemiBold,
                Margin = new Thickness(0, 0, 0, 25),
                TextWrapping = TextWrapping.Wrap,
                Foreground = Brushes.Black
            };
            QuestionPanel.Children.Add(questionText);

            // Создание вариантов ответов
            if (question.Type == QuestionType.SingleChoice)
            {
                CreateRadioButtons(question, index);
            }
            else
            {
                CreateCheckBoxes(question, index);
            }

            // Обновление состояния кнопок
            NextButton.Content = index < questions.Count - 1 ? "Следующий вопрос" : "Последний вопрос";
            SubmitButton.Visibility = index == questions.Count - 1 ? Visibility.Visible : Visibility.Collapsed;

            // Обновление прогресс-бара
            ProgressBar.Value = index;
            ProgressBar.Maximum = questions.Count - 1;
            currentQuestionIndex = index;
        }

        private void CreateRadioButtons(Question question, int questionIndex)
        {
            var stackPanel = new StackPanel();

            for (int i = 0; i < question.Options.Count; i++)
            {
                var radioButton = new RadioButton
                {
                    Content = question.Options[i],
                    FontSize = 14,
                    Margin = new Thickness(0, 8, 0, 8),
                    Tag = new Tuple<int, int>(questionIndex, i),
                    Foreground = Brushes.Black
                };
                stackPanel.Children.Add(radioButton);
            }

            QuestionPanel.Children.Add(stackPanel);
        }

        private void CreateCheckBoxes(Question question, int questionIndex)
        {
            var stackPanel = new StackPanel();

            for (int i = 0; i < question.Options.Count; i++)
            {
                var checkBox = new CheckBox
                {
                    Content = question.Options[i],
                    FontSize = 14,
                    Margin = new Thickness(0, 8, 0, 8),
                    Tag = new Tuple<int, int>(questionIndex, i),
                    Foreground = Brushes.Black
                };
                stackPanel.Children.Add(checkBox);
            }

            QuestionPanel.Children.Add(stackPanel);
        }

        private void SaveCurrentAnswer()
        {
            if (currentQuestionIndex < 0 || currentQuestionIndex >= questions.Count)
                return;

            var question = questions[currentQuestionIndex];

            if (question.Type == QuestionType.SingleChoice)
            {
                question.UserSingleAnswer = -1; // Сбрасываем ответ
                foreach (var child in QuestionPanel.Children)
                {
                    if (child is StackPanel stackPanel)
                    {
                        foreach (var item in stackPanel.Children)
                        {
                            if (item is RadioButton radioButton && radioButton.IsChecked == true)
                            {
                                question.UserSingleAnswer = ((Tuple<int, int>)radioButton.Tag).Item2;
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                question.UserMultipleAnswers.Clear();
                foreach (var child in QuestionPanel.Children)
                {
                    if (child is StackPanel stackPanel)
                    {
                        foreach (var item in stackPanel.Children)
                        {
                            if (item is CheckBox checkBox && checkBox.IsChecked == true)
                            {
                                question.UserMultipleAnswers.Add(((Tuple<int, int>)checkBox.Tag).Item2);
                            }
                        }
                    }
                }
            }
        }

        private void CalculateScore()
        {
            score = 0;
            foreach (var question in questions)
            {
                if (question.Type == QuestionType.SingleChoice)
                {
                    if (question.UserSingleAnswer == question.CorrectSingleAnswer)
                    {
                        score++;
                    }
                }
                else
                {
                    if (question.UserMultipleAnswers.Count == question.CorrectMultipleAnswers.Count &&
                        new HashSet<int>(question.UserMultipleAnswers).SetEquals(question.CorrectMultipleAnswers))
                    {
                        score++;
                    }
                }
            }
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            // Показываем элементы теста
            ProgressBar.Visibility = Visibility.Visible;
            NextButton.Visibility = Visibility.Visible;
            SubmitButton.Visibility = Visibility.Collapsed; // Пока скрываем

            // Показываем первый вопрос
            ShowQuestion(0);

            // Скрываем кнопку "Начать тест"
            StartButton.Visibility = Visibility.Collapsed;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SaveCurrentAnswer();

            if (currentQuestionIndex < questions.Count - 1)
            {
                ShowQuestion(currentQuestionIndex + 1);
            }
            else
            {
                // Если это был последний вопрос, показываем кнопку "Завершить тест"
                SubmitButton.Visibility = Visibility.Visible;
            }
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            SaveCurrentAnswer();
            CalculateScore();
            ShowResults();
        }

        private void ShowResults()
        {
            double scaledScore = (double)score / questions.Count * 10;

            string resultMessage = $"Тест наконец завершен!\n\n" +
                                 $"Результат: {scaledScore:0.0}/10\n" +
                                 $"Правильных ответов: {score} из {questions.Count}\n";

            MessageBox.Show(resultMessage, "Сами результаты теста");

            // Сохраняем в файл
            SaveResultsToFile(scaledScore);

            ResetTest();
        }

        private void SaveResultsToFile(double scaledScore)
        {
            try
            {
                string fileName = $"Тест_{DateTime.Now:yyyy-MM-dd_HH-mm}.txt";
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string filePath = System.IO.Path.Combine(desktopPath, fileName);

                string content = $"Дата: {DateTime.Now}\n" +
                                $"Результат: {scaledScore:0.0}/10\n" +
                                $"Правильных ответов: {score} из {questions.Count}\n";

                File.WriteAllText(filePath, content);

                MessageBox.Show($"Сохранено в файл:\n{filePath}", "Успешно!!!11)");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void ResetTest()
        {
            // Сбрасываем все ответы
            foreach (var question in questions)
            {
                question.Reset();
            }

            // Сбрасываем индексы и счет
            currentQuestionIndex = 0;
            score = 0;

            // Очищаем панель вопросов
            QuestionPanel.Children.Clear();

            // Показываем начальный экран
            StartButton.Visibility = Visibility.Visible;
            ProgressBar.Visibility = Visibility.Collapsed;
            NextButton.Visibility = Visibility.Collapsed;
            SubmitButton.Visibility = Visibility.Collapsed;
        }

        private void ProgressBar_ValueChanged(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }

    public enum QuestionType
    {
        SingleChoice,
        MultipleChoice
    }

    public class Question
    {
        public string Text { get; set; }
        public List<string> Options { get; set; }
        public QuestionType Type { get; set; }
        public int CorrectSingleAnswer { get; set; }
        public List<int> CorrectMultipleAnswers { get; set; }
        public int UserSingleAnswer { get; set; } = -1;
        public List<int> UserMultipleAnswers { get; set; } = new List<int>();

        public Question(string text, List<string> options, int correctAnswer, QuestionType type)
        {
            Text = text;
            Options = options;
            CorrectSingleAnswer = correctAnswer;
            Type = type;
        }

        public Question(string text, List<string> options, List<int> correctAnswers, QuestionType type)
        {
            Text = text;
            Options = options;
            CorrectMultipleAnswers = correctAnswers;
            Type = type;
        }

        public void Reset()
        {
            UserSingleAnswer = -1;
            UserMultipleAnswers.Clear();
        }
    }
}