using System;
using System.Collections.Generic;

namespace ViktorynaApp.Services
{
    public interface IQuizSessionBuilder
    {
        IQuizSessionBuilder WithId(string id);
        IQuizSessionBuilder WithUserId(string userId);
        IQuizSessionBuilder WithCategory(string category);
        IQuizSessionBuilder WithQuestions(List<Models.Pytannia> questions);
        IQuizSessionBuilder WithStartTime(DateTime startTime);
        IQuizSessionBuilder WithInitialScore(int score);
        IQuizSessionBuilder WithCurrentQuestionIndex(int index);
        QuizSession Build();
    }

    public class QuizSessionBuilder : IQuizSessionBuilder
    {
        private readonly QuizSession _session = new QuizSession();

        public IQuizSessionBuilder WithId(string id)
        {
            _session.Id = id;
            return this;
        }

        public IQuizSessionBuilder WithUserId(string userId)
        {
            _session.UserId = userId;
            return this;
        }

        public IQuizSessionBuilder WithCategory(string category)
        {
            _session.Category = category;
            return this;
        }

        public IQuizSessionBuilder WithQuestions(List<Models.Pytannia> questions)
        {
            _session.Questions = questions;
            return this;
        }

        public IQuizSessionBuilder WithStartTime(DateTime startTime)
        {
            _session.StartTime = startTime;
            return this;
        }

        public IQuizSessionBuilder WithInitialScore(int score)
        {
            _session.Score = score;
            return this;
        }

        public IQuizSessionBuilder WithCurrentQuestionIndex(int index)
        {
            _session.CurrentQuestionIndex = index;
            return this;
        }

        public QuizSession Build()
        {
            // Проста перевірка, щоб не повертати "порожню" сесію.
            if (string.IsNullOrWhiteSpace(_session.UserId) || _session.Questions == null)
                throw new InvalidOperationException("Неповна сесія вікторини");

            _session.Answers ??= new List<QuestionResult>();
            return _session;
        }
    }
}
