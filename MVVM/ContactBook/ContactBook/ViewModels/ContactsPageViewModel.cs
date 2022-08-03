using ContactBook.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ContactBook.ViewModels
{
	public class ContactsPageViewModel : BaseViewModel
	{
		private readonly IContactStore _contactStore;
		private readonly IPageService _pageService;
		private ContactViewModel _selectedContact;
		private bool _isDataLoaded;

		public ICommand LoadDataCommand { get; private set; }
		public ICommand AddContactCommand { get; private set; }
		public ICommand SelectContactCommand { get; private set; }
		public ICommand DeleteContactCommand { get; private set; }
		public ObservableCollection<ContactViewModel> Contacts { get; private set; } = new ObservableCollection<ContactViewModel>();
		public ContactViewModel SelectedContact { get => _selectedContact; set => SetValue(ref _selectedContact, value); }

		public ContactsPageViewModel(IContactStore contactStore, IPageService pageService)
		{
			_contactStore = contactStore;
			_pageService = pageService;

			LoadDataCommand = new Command(async () => await LoadData());
			AddContactCommand = new Command(async () => await AddContact());
			SelectContactCommand = new Command<ContactViewModel>(async c => await SelectContact(c));
			DeleteContactCommand = new Command<ContactViewModel>(async c => await DeleteContact(c));
		}

		private async Task LoadData()
		{
			if (_isDataLoaded)
			{
				return;
			}

			var contacts = await _contactStore.GetContactsAsync();

			foreach (var contact in contacts)
				Contacts.Add(new ContactViewModel(contact));

			_isDataLoaded = true;
		}

		private async Task SelectContact(ContactViewModel contact)
		{
			if (contact == null)
				return;

			SelectedContact = null;

			var viewModel = new ContactDetailViewModel(contact, _contactStore, _pageService);

			viewModel.ContactUpdated += (source, updatedContact) =>
			{
				contact.Id = updatedContact.Id;
				contact.FirstName = updatedContact.FirstName;
				contact.LastName = updatedContact.LastName;
				contact.Phone = updatedContact.Phone;
				contact.Email = updatedContact.Email;
				contact.IsBlocked = updatedContact.IsBlocked;
			};

			await _pageService.PushAsync(new ContactDetailPage(viewModel));
		}

		private async Task AddContact()
		{
			var viewModel = new ContactDetailViewModel(new ContactViewModel(), _contactStore, _pageService);

			viewModel.ContactAdded += (source, contact) =>
			{
				Contacts.Add(new ContactViewModel(contact));
			};

			await _pageService.PushAsync(new ContactDetailPage(viewModel));
		}

		private async Task DeleteContact(ContactViewModel contactViewModel)
		{
			if (await _pageService.DisplayAlert("Warning", $"Are you sure you want to delete {contactViewModel.FullName}?", "Yes", "No"))
			{
				Contacts.Remove(contactViewModel);

				var contact = await _contactStore.GetContact(contactViewModel.Id);
				await _contactStore.DeleteContact(contact);
			}
		}
	}
}
