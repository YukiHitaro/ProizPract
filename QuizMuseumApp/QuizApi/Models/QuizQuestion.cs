namespace QuizApi.Models;

public class QuizQuestion
{
    public string QuestionText { get; set; }
    public string ImagePath { get; set; }
    public List<string> Options { get; set; }
    public int CorrectOptionIndex { get; set; }
}
