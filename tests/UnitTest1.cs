using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuizLibrary.Models;
using System.Collections.Generic;

namespace tests
{
    [TestClass]
    public class UnitTests2
    {
        [TestMethod]
        public void CorrectAnswer_ShouldIncreaseScore()
        {
            var question = new QuizQuestion
            {
                QuestionText = "2 + 2?",
                Options = new List<string> { "3", "4", "5", "6" },
                CorrectOptionIndex = 1
            };

            Assert.AreEqual("4", question.Options[question.CorrectOptionIndex]);
        }

        [TestMethod]
        public void Question_ShouldContainFourOptions()
        {
            var question = new QuizQuestion
            {
                Options = new List<string> { "A", "B", "C", "D" }
            };

            Assert.AreEqual(4, question.Options.Count);
        }

        [TestMethod]
        public void Question_ShouldHaveValidText()
        {
            var question = new QuizQuestion { QuestionText = "What is art?" };
            Assert.IsFalse(string.IsNullOrWhiteSpace(question.QuestionText));
        }

        [TestMethod]
        public void CorrectOptionIndex_ShouldBeInRange()
        {
            var question = new QuizQuestion
            {
                Options = new List<string> { "A", "B", "C", "D" },
                CorrectOptionIndex = 2
            };

            Assert.IsTrue(question.CorrectOptionIndex >= 0 && question.CorrectOptionIndex < 4);
        }

        [TestMethod]
        public void Fail_WhenTooManyOptions()
        {
            var question = new QuizQuestion
            {
                Options = new List<string> { "A", "B", "C", "D", "E" }
            };

            Assert.AreEqual(4, question.Options.Count); // FAIL Ч специально
        }
    }
}
