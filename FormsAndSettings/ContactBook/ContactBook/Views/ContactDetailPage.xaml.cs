using ContactBook.Models;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ContactBook.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ContactDetailPage : ContentPage
	{
		public event EventHandler<Contact> ContactAdded;
		public event EventHandler<Contact> ContactUpdated;

		public ContactDetailPage(Contact contact)
		{
			//if (contact == null)
			//{
			//	throw new ArgumentNullException(nameof(contact));
			//}

			InitializeComponent();

			BindingContext = contact ?? throw new ArgumentNullException(nameof(contact));

			//BindingContext = new Contact()
			//{
			//	Id = contact.Id,
			//	FirstName = contact.FirstName,
			//	LastName = contact.LastName,
			//	Phone = contact.Phone,
			//	Email = contact.Email,
			//	IsBlocked = contact.IsBlocked
			//};
		}

		private async void OnSave(object sender, EventArgs e)
		{
			var contact = (Contact)BindingContext;

			if (string.IsNullOrWhiteSpace(contact.FullName))
			{
				await DisplayAlert("Error", "Please enter the name.", "OK");
				return;
			}

			if (contact.Id == 0)
			{
				contact.Id = 1;
				ContactAdded?.Invoke(this, contact);
			}
			else
			{
				ContactUpdated?.Invoke(this, contact);
			}

			await Navigation.PopAsync();
		}
	}
}