using ContactBook.Models;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ContactBook.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ContactDetails : ContentPage
	{
		public ContactDetails(Contact contact)
		{
			if (contact == null)
			{
				throw new ArgumentNullException(nameof(contact));
			}

			InitializeComponent();

			BindingContext = contact;

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

		private void OnSave(object sender, EventArgs e)
		{

		}
	}
}