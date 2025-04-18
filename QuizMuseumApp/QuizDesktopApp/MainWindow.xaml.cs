using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using QuizLibrary.Models;

namespace QuizDesktopApp
{
    public partial class MainWindow : Window
    {
        private List<QuizQuestion> _questions = new();
        private QuizQuestion _currentQuestion;
        private int _currentIndex = 0;
        private int _score = 0;

        private DispatcherTimer _timer;
        private int _timeLeft = 15;

        private const string ApiUrl = "https://localhost:7268/api/quiz";

        public MainWindow()
        {
            InitializeComponent();
            InitTimer();
            LoadQuestionsFromApi();
        }

        private void InitTimer()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            _timeLeft--;
            TimerTextBlock.Text = $"Осталось: {_timeLeft} сек.";

            if (_timeLeft <= 0)
            {
                _timer.Stop();
                LoadNextQuestion();
            }
        }

        private async void LoadQuestionsFromApi()
        {
            try
            {
                using var client = new HttpClient();
                var result = await client.GetFromJsonAsync<List<QuizQuestion>>(ApiUrl);
                _questions = result ?? new List<QuizQuestion>();
                _currentIndex = 0;
                _score = 0;

                LoadNextQuestion();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при получении вопросов: {ex.Message}");
                Application.Current.Shutdown();
            }
        }

        private void LoadNextQuestion()
        {
            if (_currentIndex >= _questions.Count)
            {
                MessageBox.Show($"Викторина завершена! Ваш результат: {_score}/{_questions.Count}", "Результат");
                Application.Current.Shutdown();
                return;
            }

            _currentQuestion = _questions[_currentIndex];
            _currentIndex++;

            _timeLeft = 15;
            TimerTextBlock.Text = $"Осталось: {_timeLeft} сек.";
            _timer.Start();

            LoadQuestionContent();
        }

        private void LoadQuestionContent()
        {
            QuestionTextBlock.Text = _currentQuestion.QuestionText;

            try
            {
                var imageUrl = $"https://localhost:7268/{_currentQuestion.ImagePath.Replace("\\", "/")}";
                QuestionImage.Source = new BitmapImage(new Uri(imageUrl));
            }
            catch
            {
                QuestionImage.Source = null;
            }

            OptionButton0.Content = _currentQuestion.Options[0];
            OptionButton1.Content = _currentQuestion.Options[1];
            OptionButton2.Content = _currentQuestion.Options[2];
            OptionButton3.Content = _currentQuestion.Options[3];
        }

        private void OptionButton_Click(object sender, RoutedEventArgs e)
        {
            _timer.Stop();

            int selectedIndex = int.Parse((sender as FrameworkElement)?.Name.Replace("OptionButton", "") ?? "-1");
            if (selectedIndex == _currentQuestion.CorrectOptionIndex)
            {
                _score++;
            }

            LoadNextQuestion();
        }

        private void SkipButton_Click(object sender, RoutedEventArgs e)
        {
            _timer.Stop();
            LoadNextQuestion();
        }
    }
}
