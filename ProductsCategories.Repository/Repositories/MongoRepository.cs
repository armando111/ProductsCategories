namespace ProductsCategories.Repository.Repositories
{
    using ProductsCategories.Repository.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq.Expressions;
    using MongoDB.Driver;
    using MongoDB.Bson;

    public class MongoRepository<T> : IRepository<T> where T : class
    {
        private IMongoCollection<T> collection;

        public MongoRepository()
        {
            collection = MongoConfig<T>.collection;
        }

        public async void Delete(int id)
        {
            var filter = Builders<T>.Filter.Eq("id", id);
            var result = await collection.DeleteOneAsync(filter);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            var filter = Builders<T>.Filter.Empty;
            return await collection.Find<T>(filter).ToListAsync<T>();
        }

        public async Task<IEnumerable<T>> GetByFilter(Expression<Func<T, bool>> expression)
        {
            return await collection.Find<T>(expression).ToListAsync<T>();
        }

        public async Task<T> GetOne(Expression<Func<T, bool>> expression)
        {
            return await collection.Find<T>(expression).FirstOrDefaultAsync();
        }

        public async void Insert(T entity)
        {
            await collection.InsertOneAsync(entity);
        }

        public async void Update(Expression<Func<T, bool>> filterExtention, T entity)
        {
            var filter = Builders<T>.Filter.Where(filterExtention);
            await collection.ReplaceOneAsync(filter, entity);

        }

        public async Task<int> GetNextId()
        {
            Sequence sequenceColl = new Sequence();
            IMongoCollection<Sequence> collectionSequence = MongoConfig<Sequence>.collection;

            var SequencInDB = await collectionSequence
                .Find<Sequence>(x => x.CollName == typeof(T).Name)
                .FirstOrDefaultAsync();


            if (SequencInDB == null)
            {
                await collectionSequence.InsertOneAsync(new Sequence { CollName = typeof(T).Name, Counter = 1 });
                return 1;
            }

            SequencInDB.Counter++;

            var update = Builders<Sequence>.Update.Set("Counter", SequencInDB.Counter);
            await collectionSequence.UpdateOneAsync<Sequence>(x => x.CollName == typeof(T).Name, update);

            return SequencInDB.Counter;
        }

        internal class Sequence
        {
            public ObjectId _id { get; set; }
            public string CollName { get; set; }
            public int Counter { get; set; }
        }
    }
}
