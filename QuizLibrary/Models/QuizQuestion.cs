using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizLibrary.Models
{
    public class QuizQuestion
    {
        public string Id { get; set; }                     // Уникальный идентификатор
        public string ImagePath { get; set; }              // Путь к изображению (локальный или URL)
        public string QuestionText { get; set; }           // Текст вопроса
        public List<string> Options { get; set; }          // Варианты ответов (4)
        public int CorrectOptionIndex { get; set; }        // Индекс правильного ответа

        public QuizQuestion()
        {
            Options = new List<string>();
        }
    }
}

