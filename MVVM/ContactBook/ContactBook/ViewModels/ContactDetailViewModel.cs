using ContactBook.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ContactBook.ViewModels
{
	public class ContactDetailViewModel
	{
		private readonly IContactStore _contactStore;
		private readonly IPageService _pageService;

		public event EventHandler<Contact> ContactAdded;
		public event EventHandler<Contact> ContactUpdated;

		public Contact Contact { get; private set; }
		public ICommand SaveCommand { get; private set; }

		public ContactDetailViewModel(ContactViewModel viewModel, IContactStore contactStore, IPageService pageService)
		{
			if (viewModel == null)
				throw new ArgumentNullException(nameof(viewModel));

			_contactStore = contactStore;
			_pageService = pageService;

			SaveCommand = new Command(async () => await SaveContact());

			Contact = new Contact()
			{
				Id = viewModel.Id,
				FirstName = viewModel.FirstName,
				LastName = viewModel.LastName,
				Email = viewModel.Email,
				Phone = viewModel.Phone,
				IsBlocked = viewModel.IsBlocked,
			};
		}

		private async Task SaveContact()
		{
			if (string.IsNullOrWhiteSpace(Contact.FirstName) && string.IsNullOrWhiteSpace(Contact.LastName))
			{
				await _pageService.DisplayAlert("Error", "Please enter the name.", "OK");
				return;
			}

			if (Contact.Id == 0)
			{
				await _contactStore.AddContact(Contact);

				ContactAdded?.Invoke(this, Contact);
			}
			else
			{
				await _contactStore.UpdateContact(Contact);

				ContactUpdated?.Invoke(this, Contact);
			}

			await _pageService.PopAsync();
		}
	}
}
