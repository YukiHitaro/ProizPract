using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using Xamarin.Forms;

namespace QuizApp
{
    public partial class MainPage : ContentPage
    {
        private List<QuizQuestion> _questions;
        private QuizQuestion _currentQuestion;
        private int _currentIndex = 0;
        private int _score = 0;

        private Timer _timer;
        private int _timeLeft = 15;

        public MainPage()
        {
            InitializeComponent();
            LoadQuestions();
            StartTimer();
            LoadNextQuestion();
        }

        private void LoadQuestions()
        {
            _questions = new List<QuizQuestion>
    {
        new QuizQuestion
        {
            QuestionText = "Кто автор картины 'Звёздная ночь'?",
            ImagePath = "starry_night",
            Options = new List<string> { "Моне", "Пикассо", "Ван Гог", "Дали" },
            CorrectOptionIndex = 2
        },
        new QuizQuestion
        {
            QuestionText = "Кто написал 'Гернику'?",
            ImagePath = "guernica",
            Options = new List<string> { "Дали", "Моне", "Пикассо", "Матисс" },
            CorrectOptionIndex = 2
        },
        new QuizQuestion
        {
            QuestionText = "Кто написал 'Мону Лизу'?",
            ImagePath = "mona_lisa",
            Options = new List<string> { "Рафаэль", "Леонардо да Винчи", "Микеланджело", "Боттичелли" },
            CorrectOptionIndex = 1
        },
        new QuizQuestion
        {
            QuestionText = "Кто автор картины 'Крик'?",
            ImagePath = "the_scream",
            Options = new List<string> { "Мунк", "Ван Гог", "Климт", "Пикассо" },
            CorrectOptionIndex = 0
        },
        new QuizQuestion
        {
            QuestionText = "Кто написал серию картин 'Водяные лилии'?",
            ImagePath = "water_lilies",
            Options = new List<string> { "Ренуар", "Дега", "Моне", "Мане" },
            CorrectOptionIndex = 2
        },
        new QuizQuestion
        {
            QuestionText = "Кто написал картину 'Постоянство памяти' (Мягкие часы)?",
            ImagePath = "persistence_of_memory",
            Options = new List<string> { "Магритт", "Миро", "Дали", "Шагал" },
            CorrectOptionIndex = 2
        },
        new QuizQuestion
        {
            QuestionText = "Кто автор картины 'Рождение Венеры'?",
            ImagePath = "birth_of_venus",
            Options = new List<string> { "Донателло", "Боттичелли", "Леонардо да Винчи", "Микеланджело" },
            CorrectOptionIndex = 1
        },
        new QuizQuestion
        {
            QuestionText = "Кто написал картину 'Поцелуй' (Der Kuss)?",
            ImagePath = "the_kiss",
            Options = new List<string> { "Шиле", "Климт", "Кокошка", "Бэкон" },
            CorrectOptionIndex = 1
        },
        new QuizQuestion
        {
            QuestionText = "Кто автор картины 'Менины'?",
            ImagePath = "las_meninas",
            Options = new List<string> { "Гойя", "Мурильо", "Веласкес", "Сурбаран" },
            CorrectOptionIndex = 2
        },
        new QuizQuestion
        {
            QuestionText = "Кто написал серию картин 'Подсолнухи'?",
            ImagePath = "flowers",
            Options = new List<string> { "Гоген", "Сезанн", "Ван Гог", "Тулуз-Лотрек" },
            CorrectOptionIndex = 2
        }
    };
        }

        private void StartTimer()
        {
            _timer = new Timer(1000);
            _timer.Elapsed += OnTimerTick;
            _timer.Start();
        }

        private void OnTimerTick(object sender, ElapsedEventArgs e)
        {
            _timeLeft--;
            Device.BeginInvokeOnMainThread(() =>
            {
                TimerLabel.Text = $"Осталось: {_timeLeft} сек.";
            });

            if (_timeLeft <= 0)
            {
                _timer.Stop();
                Device.BeginInvokeOnMainThread(() => LoadNextQuestion());
            }
        }

        private void LoadNextQuestion()
        {
            if (_currentIndex >= _questions.Count)
            {
                DisplayAlert("Викторина завершена", $"Ваш результат: {_score}/{_questions.Count}", "ОК");
                _timer?.Stop();
                return;
            }

            _currentQuestion = _questions[_currentIndex++];
            _timeLeft = 15;
            TimerLabel.Text = $"Осталось: {_timeLeft} сек.";
            _timer.Start();

            QuestionLabel.Text = _currentQuestion.QuestionText;
            QuestionImage.Source = ImageSource.FromFile($"{_currentQuestion.ImagePath}.jpg");

            OptionButton0.Text = _currentQuestion.Options[0];
            OptionButton1.Text = _currentQuestion.Options[1];
            OptionButton2.Text = _currentQuestion.Options[2];
            OptionButton3.Text = _currentQuestion.Options[3];
        }

        private void OnOptionClicked(object sender, EventArgs e)
        {
            _timer.Stop();

            if (sender is Button btn)
            {
                // Находим кнопку среди OptionButton0, OptionButton1, OptionButton2 и OptionButton3
                var buttons = new[] { OptionButton0, OptionButton1, OptionButton2, OptionButton3 };
                int selectedIndex = Array.IndexOf(buttons, btn);

                // Проверяем, правильно ли выбран индекс
                if (selectedIndex == -1)
                {
                    // Если по каким-то причинам индекс не найден, вы можете обработать ошибку
                    return;
                }

                // Сравниваем выбранный индекс с правильным
                if (selectedIndex == _currentQuestion.CorrectOptionIndex)
                    _score++;
            }

            LoadNextQuestion();
        }

        private void OnSkipClicked(object sender, EventArgs e)
        {
            _timer.Stop();
            LoadNextQuestion();
        }
    }
}
