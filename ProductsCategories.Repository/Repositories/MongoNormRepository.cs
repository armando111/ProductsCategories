namespace ProductsCategories.Repository.Repositories
{
    using Norm;
    using Norm.Collections;
    using Norm.Linq;
    using Norm.Responses;
    using Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Linq.Expressions;
    /*
    public class MongoNormRepository<T> : IRepository<T> where T : class
    {
        private IMongo provider;
        private IMongoDatabase db;
         
        public MongoNormRepository()
        {
            //this looks for a connection string in your Web.config
            provider = Mongo.Create(ConfigurationManager.ConnectionStrings["mongoDB"].ConnectionString);
            db = provider.Database;
        }

        public void Insert(T item)
        {
            db.GetCollection<T>().Insert(item);
        }

        public T GetById(Expression<Func<T, bool>> expression)
        {
            return GetAll().Where(expression).SingleOrDefault();
        }

        public IQueryable<T> GetAll()
        {
            return provider.GetCollection<T>().AsQueryable();
        }

        public void Update(object OldEntity, T entity)
        {
            db.GetCollection<T>().UpdateOne(OldEntity, entity);
        }
        
        public void Delete(T entity)
        {
            db.GetCollection<T>().Delete(entity);
        }

        public int GetNextId(T entity)
        {
             Sequence seq = provider
                .GetCollection<Sequence>()
                .AsQueryable()
                .Where(s => s.Name == typeof(T).Name)
                .SingleOrDefault();

            Sequence sequenceTmp = new Sequence();
            if(seq == null)
            {
                Sequence seqNew = new Sequence() { Name = typeof(T).Name, Seq = 0 };
                db.GetCollection<Sequence>().Insert(seqNew);

                seqNew.Seq++;
                sequenceTmp = seqNew;
            }
            else
            {
                seq.Seq++;
                sequenceTmp = seq;
            }
            
            db.GetCollection<Sequence>().UpdateOne(new { Name = sequenceTmp.Name}, sequenceTmp);

            return sequenceTmp.Seq;
        }

        private class Sequence
        {
            public ObjectId _id { get; set; }
            public string Name { get; set; }
            public int Seq { get; set; }
        }

    }
    */
}







