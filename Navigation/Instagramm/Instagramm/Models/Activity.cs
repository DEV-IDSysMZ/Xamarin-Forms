namespace Instagramm.Models
{
	public class Activity
	{
		public int UserId { get; set; }
		public string Description { get; set; }
		public string ImageUrl => $"https://placeimg.com/100/100/people/{UserId}";
	}
}
