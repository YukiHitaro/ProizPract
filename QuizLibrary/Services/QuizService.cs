using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizLibrary.Models;

namespace QuizLibrary.Services
{
    public class QuizService
    {
        private readonly List<QuizQuestion> _questions;
        private int _currentIndex;
        public int Score { get; private set; }

        public QuizService(List<QuizQuestion> questions)
        {
            _questions = questions ?? throw new ArgumentNullException(nameof(questions));
            _currentIndex = 0;
            Score = 0;
        }

        public QuizQuestion GetNextQuestion()
        {
            if (_currentIndex < _questions.Count)
                return _questions[_currentIndex++];
            return null;
        }

        public void SubmitAnswer(int selectedIndex)
        {
            if (_currentIndex == 0 || _currentIndex > _questions.Count)
                return;

            var lastQuestion = _questions[_currentIndex - 1];

            if (selectedIndex == lastQuestion.CorrectOptionIndex)
                Score++;
        }

        public bool IsFinished => _currentIndex >= _questions.Count;

        public int TotalQuestions => _questions.Count;
    }
}
