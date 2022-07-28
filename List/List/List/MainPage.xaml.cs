using List.Models;
using List.Service;
using System.Collections.Generic;
using Xamarin.Forms;

namespace List
{
	public partial class MainPage : ContentPage
	{
		private SearchService _searchService;
		private List<SearchGroup> _searchGroup;

		public MainPage()
		{
			_searchService = new SearchService();

			InitializeComponent();

			PopulateListView(_searchService.GetSearches());
		}

		private void PopulateListView(IEnumerable<Search> searches)
		{
			_searchGroup = new List<SearchGroup>
			{
				new SearchGroup("Recent Searches", searches)
			};

			listView.ItemsSource = _searchGroup;
		}

		private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
		{
			PopulateListView(_searchService.GetSearches(e.NewTextValue));
		}

		private void OnDeleteClicked(object sender, System.EventArgs e)
		{
			var search = (sender as MenuItem).CommandParameter as Search;

			_searchGroup[0].Remove(search);
			_searchService.DeleteSearch(search.Id);
		}

		private void OnSearchSelected(object sender, SelectedItemChangedEventArgs e)
		{
			DisplayAlert("Selected Element", (e.SelectedItem as Search).Location, "Ok");
		}

		private void OnRefreshing(object sender, System.EventArgs e)
		{
			PopulateListView(_searchService.GetSearches());

			listView.EndRefresh();
		}
	}
}
