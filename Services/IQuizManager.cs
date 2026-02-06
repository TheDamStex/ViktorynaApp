// Services/IQuizManager.cs
using ViktorynaApp.Models;
using System.Collections.Generic;

namespace ViktorynaApp.Services
{
    public interface IQuizManager
    {
        QuizSession StartNewQuiz(string userId, string category);
        QuestionResult SubmitAnswer(QuizSession session, List<int> selectedAnswers);
        QuizResult FinishQuiz(QuizSession session);
    }

    public class QuizSession
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Category { get; set; }
        public List<Pytannia> Questions { get; set; }
        public List<QuestionResult> Answers { get; set; }
        public DateTime StartTime { get; set; }
    }

    public class QuestionResult
    {
        public Pytannia Question { get; set; }
        public List<int> SelectedAnswers { get; set; }
        public bool IsCorrect { get; set; }
    }

    public class QuizResult
    {
        public string QuizId { get; set; }
        public int CorrectAnswers { get; set; }
        public int TotalQuestions { get; set; }
        public int ScorePercentage { get; set; }
        public int Rank { get; set; }
    }
}