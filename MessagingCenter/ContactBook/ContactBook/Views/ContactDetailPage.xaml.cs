using ContactBook.Persistence;
using Xamarin.Forms;

namespace ContactBook
{
	public partial class ContactDetailPage : ContentPage
	{
		public ContactDetailPage(ContactViewModel viewModel)
		{
			InitializeComponent();

			var contactStore = new SQLiteContactStore(DependencyService.Get<ISQLiteDb>());
			var pageService = new PageService();

			BindingContext = new ContactDetailViewModel(
				viewModel ?? new ContactViewModel(), contactStore, pageService);
		}
	}
}