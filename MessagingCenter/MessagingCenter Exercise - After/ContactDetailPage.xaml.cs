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

			// Note that I've pushed the responsibility of creating a 
			// ContactDetailViewModel into this page. This simplifies the code 
			// in the consumer of this page (ContactsPageViewModel). So, every time
			// we create a new ContactDetailPage, we don't have to worry about 
			// instantiating the underlying view model. 

			BindingContext = new ContactDetailViewModel(
				viewModel ?? new ContactViewModel(), contactStore, pageService);
		}
	}
}