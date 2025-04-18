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
                QuestionText = "��� ����� ������ �������� ����?",
                Options = new List<string> { "����", "�������", "��� ���", "����" },
                CorrectOptionIndex = 2
            };

            Assert.AreEqual("��� ���", question.Options[question.CorrectOptionIndex]);
        }

        [TestMethod]
        public void QuizQuestion_OptionsCount_ShouldBeFour()
        {
            var question = new QuizQuestion
            {
                Options = new List<string> { "����", "���", "���", "������" }
            };

            Assert.AreEqual(4, question.Options.Count);
        }

        [TestMethod]
        public void QuizQuestion_Text_ShouldNotBeNull()
        {
            var question = new QuizQuestion
            {
                QuestionText = "���� ������"
            };

            Assert.IsNotNull(question.QuestionText);
        }

        [TestMethod]
        public void QuizQuestion_CorrectIndex_ShouldBeWithinBounds()
        {
            var question = new QuizQuestion
            {
                Options = new List<string> { "�", "�", "�", "�" },
                CorrectOptionIndex = 3
            };

            Assert.IsTrue(question.CorrectOptionIndex >= 0 && question.CorrectOptionIndex < question.Options.Count);
        }

        [TestMethod]
        public void QuizQuestion_IncorrectIndex_ShouldFail()
        {
            var question = new QuizQuestion
            {
                Options = new List<string> { "�", "�", "�" },
                CorrectOptionIndex = 4
            };

            Assert.IsTrue(question.CorrectOptionIndex < question.Options.Count);
        }
    }
}
