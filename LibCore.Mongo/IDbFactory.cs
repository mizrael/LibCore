using MongoDB.Driver;

namespace LibCore.Mongo
{
    public interface IDbFactory
	{
		IMongoDatabase GetDatabase(string connectionString);
	}
}
