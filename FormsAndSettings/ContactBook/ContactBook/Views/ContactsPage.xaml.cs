using ContactBook.Models;
using ContactBook.Views;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace ContactBook
{
	public partial class ContactsPage : ContentPage
	{
		private readonly ObservableCollection<Contact> _contacts;

		public ContactsPage()
		{
			InitializeComponent();

			_contacts = new ObservableCollection<Contact>
			{
				new Contact { Id = 1, FirstName = "John", LastName = "Smith", Email = "john@smith.com", Phone = "1111" },
				new Contact { Id = 2, FirstName = "Mary", LastName = "Johnson", Email = "mary@johnson.com", Phone = "2222" }
			};

			contacts.ItemsSource = _contacts;
		}

		private async void OnAddContact(object sender, EventArgs e)
		{
			var page = new ContactDetailPage(new Contact());

			page.ContactAdded += (source, contact) =>
			{
				_contacts.Add(contact);
			};

			await Navigation.PushAsync(page);
		}

		private async void OnContactSelected(object sender, SelectedItemChangedEventArgs e)
		{
			if (contacts.SelectedItem == null)
			{
				return;
			}

			var selectedContact = e.SelectedItem as Contact;
			var page = new ContactDetailPage(selectedContact);

			contacts.SelectedItem = null;

			page.ContactUpdated += (source, contact) =>
			{
				selectedContact.Id = contact.Id;
				selectedContact.FirstName = contact.FirstName;
				selectedContact.LastName = contact.LastName;
				selectedContact.Phone = contact.Phone;
				selectedContact.Email = contact.Email;
				selectedContact.IsBlocked = contact.IsBlocked;
			};

			await Navigation.PushAsync(page);
		}

		private async void DeleteContact(object sender, EventArgs e)
		{
			var contact = (sender as MenuItem).CommandParameter as Contact;

			if (await DisplayAlert("Warning", $"Are you sure you want to delete {contact.FullName}?", "Yes", "No"))
				_contacts.Remove(contact);
		}
	}
}
