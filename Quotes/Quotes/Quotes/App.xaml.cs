using Xamarin.Forms;

namespace Quotes
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			MainPage = new QuotesPage();
		}

		protected override void OnStart()
		{
		}

		protected override void OnSleep()
		{
		}

		protected override void OnResume()
		{
		}
	}
}
