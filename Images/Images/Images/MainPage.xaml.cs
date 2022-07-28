using System;
using Xamarin.Forms;

namespace Images
{
	public partial class MainPage : ContentPage
	{
		public int _imageId = 1;

		public MainPage()
		{
			InitializeComponent();
			SetImage();
		}

		private void Previous_Clicked(object sender, EventArgs e)
		{
			if (--_imageId < 1)
			{
				_imageId = 10;
			}

			SetImage();
		}

		private void Next_Clicked(object sender, EventArgs e)
		{
			if (++_imageId > 10)
			{
				_imageId = 1;
			}

			SetImage();
		}

		private void SetImage()
		{
			image.Source = new UriImageSource()
			{
				Uri = new Uri(string.Format("https://picsum.photos/id/{0}/1920/1080", _imageId)),
				CachingEnabled = false
			};
		}
	}
}
