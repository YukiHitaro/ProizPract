using QuizApi.Models;
using System.Text.Json;

namespace QuizApi.Services;

public static class QuizRepository
{
    public static List<QuizQuestion> LoadFromJson(string path)
    {
        if (!File.Exists(path))
            return new List<QuizQuestion>();

        var json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<QuizQuestion>>(json) ?? new List<QuizQuestion>();
    }
}