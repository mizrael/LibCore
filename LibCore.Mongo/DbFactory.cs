using System;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;

namespace LibCore.Mongo
{
	public class DbFactory : IDbFactory
	{
		public IMongoDatabase GetDatabase(string connectionString)
		{
			if (string.IsNullOrWhiteSpace(connectionString))
				throw new ArgumentNullException(nameof(connectionString));

			// http://www.nguyenquyhy.com/2016/02/migrating-legacy-uuid-of-mongodb-to-standard-uuid/
			MongoDefaults.GuidRepresentation = MongoDB.Bson.GuidRepresentation.Standard;

			var dbClient = new MongoClient(connectionString);

			var connStr = new ConnectionString(connectionString);
			var db = dbClient.GetDatabase(connStr.DatabaseName);

			return db;
		}}
}
