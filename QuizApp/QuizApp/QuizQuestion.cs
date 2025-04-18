using System;
using System.Collections.Generic;
using System.Text;

namespace QuizApp
{
    public class QuizQuestion
    {
        public string QuestionText { get; set; }
        public string ImagePath { get; set; } // Название изображения без расширения
        public List<string> Options { get; set; }
        public int CorrectOptionIndex { get; set; }
    }
}
