using System;

namespace LibCore.Mongo
{

    public class RepositoryOptions
    {
        public RepositoryOptions(string connectionString, string collectionName)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException(nameof(connectionString));
            
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentNullException(nameof(collectionName));

            CollectionName = collectionName;
            
            ConnectionString = connectionString;
        }

        public readonly string ConnectionString;
        
        public readonly string CollectionName;
    }

}