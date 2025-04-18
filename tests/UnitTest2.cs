using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuizLibrary.Models;
using System.Collections.Generic;

namespace QuizLibrary.Tests
{
    [TestClass]
    public class QuizQuestionTests
    {
        [TestMethod]
        public void QuizQuestion_CorrectAnswer_ShouldMatch()
        {
            var question = new QuizQuestion
            {
                QuestionText = "Как зовут автора «Звёздной ночи»?",
                Options = new List<string> { "Дали", "Пикассо", "Ван Гог", "Моне" },
                CorrectOptionIndex = 2
            };

            Assert.AreEqual("Ван Гог", question.Options[question.CorrectOptionIndex]);
        }

        [TestMethod]
        public void QuizQuestion_OptionsCount_ShouldBeFour()
        {
            var question = new QuizQuestion
            {
                Options = new List<string> { "Один", "Два", "Три", "Четыре" }
            };

            Assert.AreEqual(4, question.Options.Count);
        }

        [TestMethod]
        public void QuizQuestion_Text_ShouldNotBeNull()
        {
            var question = new QuizQuestion
            {
                QuestionText = "Тест вопрос"
            };

            Assert.IsNotNull(question.QuestionText);
        }

        [TestMethod]
        public void QuizQuestion_CorrectIndex_ShouldBeWithinBounds()
        {
            var question = new QuizQuestion
            {
                Options = new List<string> { "А", "Б", "В", "Г" },
                CorrectOptionIndex = 3
            };

            Assert.IsTrue(question.CorrectOptionIndex >= 0 && question.CorrectOptionIndex < question.Options.Count);
        }

        [TestMethod]
        public void QuizQuestion_IncorrectIndex_ShouldFail()
        {
            var question = new QuizQuestion
            {
                Options = new List<string> { "А", "Б", "В" },
                CorrectOptionIndex = 4
            };

            Assert.IsTrue(question.CorrectOptionIndex < question.Options.Count);
        }
    }
}
