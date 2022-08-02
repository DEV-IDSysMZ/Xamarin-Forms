using SQLite;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ContactBook.Models
{
	public class Contact : INotifyPropertyChanged
	{
		private string _firstName;
		private string _lastName;

		public event PropertyChangedEventHandler PropertyChanged;

		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }

		[MaxLength(255)]
		public string FirstName
		{
			get => _firstName;
			set
			{
				if (_firstName == value)
				{
					return;
				}

				_firstName = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(FullName));
			}
		}

		[MaxLength(255)]
		public string LastName
		{
			get => _lastName;
			set
			{
				if (_lastName == value)
				{
					return;
				}

				_lastName = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(FullName));
			}
		}

		[MaxLength(20)]
		public string Phone { get; set; }

		[MaxLength(255)]
		public string Email { get; set; }

		public bool IsBlocked { get; set; }

		[Ignore]
		public string FullName
		{
			get { return $"{FirstName} {LastName}"; }
		}

		private void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
