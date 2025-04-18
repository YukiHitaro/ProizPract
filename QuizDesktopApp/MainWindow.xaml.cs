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
using QuizLibrary.Models;
using QuizLibrary.Services;
using System;
using System.IO;
using System.Windows.Threading;
namespace QuizDesktopApp
{
    public partial class MainWindow : Window
    {
        private QuizService _quizService;
        private QuizQuestion _currentQuestion;
        private DispatcherTimer _timer;
        private int _timeLeft = 15;

        public MainWindow()
        {
            InitializeComponent();

            var questions = QuizRepository.LoadFromJson("quiz_data.json");
            _quizService = new QuizService(questions);

            InitTimer();
            LoadNextQuestion();
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

        private void LoadNextQuestion()
        {
            _timeLeft = 15;
            TimerTextBlock.Text = $"Осталось: {_timeLeft} сек.";
            _timer.Start();

            _currentQuestion = _quizService.GetNextQuestion();

            if (_currentQuestion == null)
            {
                MessageBox.Show($"Викторина завершена! Ваш результат: {_quizService.Score}/{_quizService.TotalQuestions}", "Результат");
                Application.Current.Shutdown();
                return;
            }

            try
            {
                QuestionImage.Source = new BitmapImage(new Uri(System.IO.Path.GetFullPath(_currentQuestion.ImagePath)));
            }
            catch
            {
                QuestionImage.Source = null;
            }

            QuestionTextBlock.Text = _currentQuestion.QuestionText;

            OptionButton0.Content = _currentQuestion.Options[0];
            OptionButton1.Content = _currentQuestion.Options[1];
            OptionButton2.Content = _currentQuestion.Options[2];
            OptionButton3.Content = _currentQuestion.Options[3];
        }

        private void OptionButton_Click(object sender, RoutedEventArgs e)
        {
            _timer.Stop();
            int selectedIndex = int.Parse((sender as FrameworkElement).Name.Replace("OptionButton", ""));
            _quizService.SubmitAnswer(selectedIndex);
            LoadNextQuestion();
        }

        private void SkipButton_Click(object sender, RoutedEventArgs e)
        {
            _timer.Stop();
            LoadNextQuestion();
        }
    }
}
