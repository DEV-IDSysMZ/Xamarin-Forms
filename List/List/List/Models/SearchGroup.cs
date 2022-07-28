using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace List.Models
{
	internal class SearchGroup : ObservableCollection<Search>
	{
		public string Title { get; set; }

		public SearchGroup(string title, IEnumerable<Search> seaches = null) : base(seaches)
		{
			Title = title;
		}
	}
}
