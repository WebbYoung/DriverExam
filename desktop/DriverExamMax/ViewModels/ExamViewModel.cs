using DriverExam.QuestionDal;
using DriverExamMax.QuestionDal;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DriverExam.ViewModels
{
    class ExamViewModel:INotifyPropertyChanged
    {
        private Question? currentQuestion;
		public QuestionService _questionService { get; private set; }
        private int currentIndex = -1; 
		public event PropertyChangedEventHandler? PropertyChanged;
		public string CurrentIndex
		{
			get => currentIndex.ToString();
			set => SetProperty(ref currentIndex, Convert.ToInt32(value)).ToString();
		}
		public ExamViewModel(string path,QuestionService.Mode mode)
		{
			QuestionManager questionManager = new(path);
			_questionService = new QuestionService(questionManager,mode);
			GoToFirstQuestion();
		}
		public Question? CurrentQuestion
        {
			get => currentQuestion;
			set=>SetProperty(ref currentQuestion, value);
        }
		public bool CanGoPrevious => currentIndex > 0;
		public bool CanGoNext=>currentIndex <_questionService.GetLength();
		public void GoToFirstQuestion()
		{
			SetCurrentQuestion(0);
		}
		public void GoToNextQuestion()
		{
			if (CanGoNext)
			{
				SetCurrentQuestion(currentIndex+1);
			}
		}
		public void GoToPreviousQuestion()
		{
			if (CanGoPrevious)
			{
				SetCurrentQuestion(currentIndex - 1);
			}
		}
		private void SetCurrentQuestion(int index)
		{
			currentIndex = index;
			CurrentQuestion=_questionService.GetQuestion(index);
			OnPropertyChanged(nameof(CanGoPrevious));
			OnPropertyChanged(nameof(CanGoNext));
			OnPropertyChanged(nameof(CurrentIndex));
		}
		public bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
		{
			if (EqualityComparer<T>.Default.Equals(field, value)) return false;
			field = value;
			OnPropertyChanged(propertyName);
			return true;
		}
		public void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
