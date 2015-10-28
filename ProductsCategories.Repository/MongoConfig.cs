namespace ProductsCategories.Repository
{
    using MongoDB.Driver;
    using System.Configuration;

    public static class MongoConfig<T> where T : class
    {
        private static IMongoClient client;
        private static IMongoDatabase db;
        private static readonly string mongoServer = ConfigurationManager.ConnectionStrings["mongoDBServer"].ConnectionString;
        private static readonly string mongoDb = ConfigurationManager.ConnectionStrings["mongoDB"].ConnectionString;

        public static  IMongoCollection<T> collection;
        
        static MongoConfig()
        {
            client = new MongoClient(mongoServer);
            db = client.GetDatabase(mongoDb);
            collection = db.GetCollection<T>(typeof(T).Name);
        }
    }
}
