namespace Instagramm.Models
{
	public class User
	{
		public int Id { get; set; }
		public string Description { get; set; }
		public string Name { get; set; }
		public string ImageUrl => $"https://placeimg.com/100/100/people/{Id}";
	}
}