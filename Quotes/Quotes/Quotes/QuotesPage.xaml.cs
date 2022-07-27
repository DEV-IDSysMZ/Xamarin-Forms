using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Quotes
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class QuotesPage : ContentPage
	{
		private int _index = 0;
		private readonly string[] _quotes = new string[]
		{
			"First Quote",
			"Second Quote",
			"Third Quote",
			"4. Quote",
			"5. Quote"
		};

		public QuotesPage()
		{
			InitializeComponent();
			quote.Text = _quotes[_index];
		}

		private void Button_Clicked(object sender, EventArgs e)
		{
			if (++_index >= _quotes.Length)
				_index = 0;

			quote.Text = _quotes[_index];
		}
	}
}