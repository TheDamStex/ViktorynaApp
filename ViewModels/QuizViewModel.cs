// ViewModels/QuizViewModel.cs
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ViktorynaApp.Models;
using ViktorynaApp.Services;

namespace ViktorynaApp.ViewModels
{
    public class QuizViewModel : BaseViewModel
    {
        private readonly IViktorynaService _viktorynaService;
        private string _category;
        private string _login;

        private ObservableCollection<Pytannia> _questions = new ObservableCollection<Pytannia>();
        public ObservableCollection<Pytannia> Questions
        {
            get => _questions;
            set => SetProperty(ref _questions, value);
        }

        private Pytannia _currentQuestion;
        public Pytannia CurrentQuestion
        {
            get => _currentQuestion;
            set => SetProperty(ref _currentQuestion, value);
        }

        private int _currentQuestionIndex;
        public int CurrentQuestionIndex
        {
            get => _currentQuestionIndex;
            set => SetProperty(ref _currentQuestionIndex, value);
        }

        private string _questionCounter;
        public string QuestionCounter
        {
            get => _questionCounter;
            set => SetProperty(ref _questionCounter, value);
        }

        public ICommand NextCommand { get; }
        public ICommand PreviousCommand { get; }
        public ICommand FinishCommand { get; }

        public QuizViewModel(IViktorynaService viktorynaService, string login, string category)
        {
            _viktorynaService = viktorynaService;
            _login = login;
            _category = category;

            NextCommand = new RelayCommand(NextQuestion);
            PreviousCommand = new RelayCommand(PreviousQuestion);
            FinishCommand = new RelayCommand(FinishQuiz);

            LoadQuestions();
        }

        private void LoadQuestions()
        {
            var questions = _viktorynaService.OtrymatyPytannia(_category);
            Questions = new ObservableCollection<Pytannia>(questions);

            if (Questions.Count > 0)
            {
                CurrentQuestionIndex = 0;
                CurrentQuestion = Questions[0];
                UpdateQuestionCounter();
            }
        }

        private void NextQuestion()
        {
            if (CurrentQuestionIndex < Questions.Count - 1)
            {
                CurrentQuestionIndex++;
                CurrentQuestion = Questions[CurrentQuestionIndex];
                UpdateQuestionCounter();
            }
        }

        private void PreviousQuestion()
        {
            if (CurrentQuestionIndex > 0)
            {
                CurrentQuestionIndex--;
                CurrentQuestion = Questions[CurrentQuestionIndex];
                UpdateQuestionCounter();
            }
        }

        private void FinishQuiz()
        {
            // Логіка завершення вікторини
            int correctAnswers = CalculateCorrectAnswers();

            var result = new Rezultat
            {
                Login = _login,
                Rozdil = _category,
                KilkistPravylnyh = correctAnswers
            };

            _viktorynaService.DodatyRezultat(result);
        }

        private int CalculateCorrectAnswers()
        {
            // Тимчасова реалізація
            return 0;
        }

        private void UpdateQuestionCounter()
        {
            QuestionCounter = $"Питання {CurrentQuestionIndex + 1} з {Questions.Count}";
        }
    }
}