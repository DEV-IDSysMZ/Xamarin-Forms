using Instagramm.Models;
using Instagramm.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Instagramm
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ActivityFeed : ContentPage
	{
		public ActivityService ActivityList { get; set; }

		public ActivityFeed()
		{
			ActivityList = new ActivityService();
			InitializeComponent();

			activityFeed.ItemsSource = ActivityList.GetActivities();
		}

		private void OnActivitySelected(object sender, SelectedItemChangedEventArgs e)
		{
			if (!(e.SelectedItem is Activity activity))
				return;

			activityFeed.SelectedItem = null;

			Navigation.PushAsync(new UserProfile(activity.UserId));
		}
	}
}
