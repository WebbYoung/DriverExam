using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DriverExam.QuestionDal
{
    public class Question: INotifyPropertyChanged
	{
		private int questionId;
		private string? title;
		private Image? image;
		private Option? selectedOption;
		private List<Option> options;
		private string answer;
		private string? analysis;
		public int QuestionId 
		{
			get => questionId;
			set => SetProperty(ref questionId, value);
		}
        public string Title
		{
			get => title??throw new ArgumentNullException(nameof(title));
			set => SetProperty(ref title, value);
		}
        public Image Image
		{
			get => image??throw new ArgumentNullException(nameof(image));
			set => SetProperty(ref image, value);
		}
		public List<Option> Options
		{
			get => options;
			set => SetProperty(ref options, value);
		} 
		public Option SelectedOption
		{
			get => selectedOption ?? throw new ArgumentNullException(nameof(selectedOption));
			set => SetProperty(ref selectedOption, value);
		}
		public string Analysis
		{
			get => analysis ?? throw new ArgumentNullException(nameof(analysis));
			set => SetProperty(ref analysis, value);
		}
		public string Answer
		{
			get => answer??throw new ArgumentNullException(nameof(answer));
			set => SetProperty(ref answer, value);
		}
		public event PropertyChangedEventHandler? PropertyChanged;
		public bool SetProperty<T>(ref T field,T value, [CallerMemberName]string propertyName="")
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
