using ContactBook.iOS.Persistence;
using ContactBook.Persistence;
using SQLite;
using System;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteDb))]
namespace ContactBook.iOS.Persistence
{
	internal class SQLiteDb : ISQLiteDb
	{
		public SQLiteAsyncConnection GetConnection()
		{
			var documentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			var path = Path.Combine(documentPath, "MySQLite.db3");

			return new SQLiteAsyncConnection(path);
		}
	}
}