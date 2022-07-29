using Instagramm.Services;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Instagramm
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserProfile : ContentPage
	{
		private UserService _userService = new UserService();

		public UserProfile(int userId)
		{
			BindingContext = _userService.GetUser(userId);

			InitializeComponent();
		}
	}
}