using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactBook.Services
{
	public interface IContactStore
	{
		Task<IEnumerable<Contact>> GetContactsAsync();
		Task<Contact> GetContact(int id);
		Task AddContact(Contact contact);
		Task UpdateContact(Contact contact);
		Task DeleteContact(Contact contact);
	}
}
