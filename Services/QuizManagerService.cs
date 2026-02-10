// Services/QuizManagerService.cs
using System;
using System.Collections.Generic;
using System.Linq;
using ViktorynaApp.Models;

namespace ViktorynaApp.Services
{
    public class QuizManagerService : IQuizManager
    {
        private readonly IViktorynaService _viktorynaService;
        private readonly Dictionary<string, QuizSession> _activeSessions = new();

        public QuizManagerService(IViktorynaService viktorynaService)
        {
            _viktorynaService = viktorynaService;
        }

        public QuizSession StartNewQuiz(string userId, string category)
        {
            var questions = _viktorynaService.OtrymatyPytannia(category);

            // Використовуємо будівельник для покрокового створення сесії.
            var session = new QuizSessionBuilder()
                .WithId(Guid.NewGuid().ToString())
                .WithUserId(userId)
                .WithCategory(category)
                .WithQuestions(questions)
                .WithStartTime(DateTime.Now)
                .WithInitialScore(0)
                .WithCurrentQuestionIndex(0)
                .Build();

            _activeSessions[session.Id] = session;
            return session;
        }

        public QuestionResult SubmitAnswer(QuizSession session, List<int> selectedAnswers)
        {
            if (session.Answers.Count >= session.Questions.Count)
                throw new InvalidOperationException("Усі питання вже відповіли");

            var currentQuestion = session.Questions[session.Answers.Count];
            var isCorrect = IsAnswerCorrect(currentQuestion, selectedAnswers);

            var result = new QuestionResult
            {
                Question = currentQuestion,
                SelectedAnswers = selectedAnswers,
                IsCorrect = isCorrect
            };

            session.Answers.Add(result);
            return result;
        }

        public QuizResult FinishQuiz(QuizSession session)
        {
            int correctAnswers = session.Answers.Count(a => a.IsCorrect);
            int totalQuestions = session.Questions.Count;

            var result = new QuizResult
            {
                QuizId = session.Id,
                CorrectAnswers = correctAnswers,
                TotalQuestions = totalQuestions,
                ScorePercentage = (int)Math.Round((double)correctAnswers / totalQuestions * 100)
            };

            // Зберігаємо результат
            SaveQuizResult(session.UserId, session.Category, correctAnswers, totalQuestions);

            // Видаляємо сесію
            _activeSessions.Remove(session.Id);

            return result;
        }

        private bool IsAnswerCorrect(Pytannia question, List<int> selectedAnswers)
        {
            // Перевіряємо чи всі правильні відповіді обрані
            var correctAnswers = question.PravylnyIndex;

            if (selectedAnswers.Count != correctAnswers.Count)
                return false;

            return selectedAnswers.All(correctAnswers.Contains) &&
                   correctAnswers.All(selectedAnswers.Contains);
        }

        private void SaveQuizResult(string userId, string category, int correctAnswers, int totalQuestions)
        {
            var result = new Models.Rezultat
            {
                Login = userId,
                Rozdil = category,
                KilkistPravylnyh = correctAnswers,
                TotalQuestions = totalQuestions
            };

            _viktorynaService.DodatyRezultat(result);
        }
    }
}
