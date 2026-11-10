using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DriverExam.QuestionDal
{
	public class Option : INotifyPropertyChanged
	{
		private string content; 
		private bool isCorrect;
		private bool isSelected;

		public string Content
		{
			get => content;
			set => SetProperty(ref content, value);
		}

		public bool IsCorrect
		{
			get => isCorrect;
			set => SetProperty(ref isCorrect, value);
		}
		public bool IsSelected
		{
			get => isSelected;
			set => SetProperty(ref isSelected, value);
		}

		public Option(string content, bool isCorrect) 
		{
			this.content = content ?? throw new ArgumentNullException(nameof(content)); 
			this.isCorrect = isCorrect; 
		}

		public event PropertyChangedEventHandler? PropertyChanged;

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
