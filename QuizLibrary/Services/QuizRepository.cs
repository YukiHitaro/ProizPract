using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizLibrary.Models;
using System.Text.Json;

namespace QuizLibrary.Services
{
    public static class QuizRepository
    {
        public static List<QuizQuestion> LoadFromJson(string path)
        {
            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<List<QuizQuestion>>(json);
        }
    }
}
