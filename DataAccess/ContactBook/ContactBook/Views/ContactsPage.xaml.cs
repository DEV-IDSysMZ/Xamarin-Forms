using ContactBook.Models;
using ContactBook.Persistence;
using ContactBook.Views;
using SQLite;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace ContactBook
{
	public partial class ContactsPage : ContentPage
	{
		private ObservableCollection<Contact> _contacts;
		private readonly SQLiteAsyncConnection _connection;
		private bool _isLoaded;

		public ContactsPage()
		{
			InitializeComponent();

			_connection = DependencyService.Get<ISQLiteDb>().GetConnection();
		}

		protected override async void OnAppearing()
		{
			if (!_isLoaded)
			{
				await _connection.CreateTableAsync<Contact>();
				var contacts = await _connection.Table<Contact>().ToListAsync();

				_contacts = new ObservableCollection<Contact>(contacts);

				contactList.ItemsSource = _contacts;

				_isLoaded = true;
			}

			base.OnAppearing();
		}

		private async void OnAddContact(object sender, EventArgs e)
		{
			var page = new ContactDetailPage(new Contact());

			page.ContactAdded += async (source, contact) =>
			{
				_contacts.Add(contact);
				await _connection.InsertAsync(contact);
			};

			await Navigation.PushAsync(page);
		}

		private async void OnContactSelected(object sender, SelectedItemChangedEventArgs e)
		{
			if (contactList.SelectedItem == null)
			{
				return;
			}

			var selectedContact = e.SelectedItem as Contact;
			var page = new ContactDetailPage(selectedContact);

			contactList.SelectedItem = null;

			page.ContactUpdated += async (source, contact) =>
			{
				selectedContact = contact;
				await _connection.UpdateAsync(contact);
			};

			await Navigation.PushAsync(page);
		}

		private async void DeleteContact(object sender, EventArgs e)
		{
			var contact = (sender as MenuItem).CommandParameter as Contact;

			if (await DisplayAlert("Warning", $"Are you sure you want to delete {contact.FullName}?", "Yes", "No"))
			{
				_contacts.Remove(contact);
				await _connection.DeleteAsync(contact);
			}
		}
	}
}
