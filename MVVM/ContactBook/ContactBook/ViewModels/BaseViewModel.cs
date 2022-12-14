using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ContactBook.ViewModels
{
	public class BaseViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public void SetValue<T>(ref T backingField, T value, [CallerMemberName] string propertyName = null)
		{
			if (EqualityComparer<T>.Default.Equals(backingField, value))
			{
				return;
			}

			backingField = value;

			OnPropertyChanged(propertyName);
		}
	}
}
