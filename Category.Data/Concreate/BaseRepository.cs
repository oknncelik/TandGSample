using Category.Common.Configs.Models;
using Category.Data.Abstruct;
using Category.Entities.Abstruct;
using MongoDB.Driver;
using System.Linq.Expressions;

public enum OrderBy
{
    ASC,
    DESC,
}

namespace Category.Data.MongoDb.Concreate
{
    /// <summary>
    /// Docker Mongo Install...
    /// docker run -d --name mongossrv -p 27017:27017 -v etc/mongod.conf -e MONGO_INITDB_ROOT_USERNAME=ping -e MONGO_INITDB_ROOT_PASSWORD=pong mongo
    /// Connection
    /// mongodb://ping:pong@localhost:27017/TandGSampleCategory?authSource=admin
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseRepository<T> : IBaseRepository<T> where T : class, IEntity, new()
    {
        public readonly IMongoCollection<T> collection;
        public IMongoDatabase database { get; set; }

        public BaseRepository(MongoDbSettings setting)
        {
            var connectionString = $"mongodb://{setting.UserName}:{setting.Password}@" +
                                   $"{setting.Server}/{setting.DataBase}" +
                                   $"?authSource=admin";
            var client = new MongoClient(connectionString);
            database = client.GetDatabase(setting.DataBase);
            this.collection = database.GetCollection<T>(typeof(T).Name);
        }

        public IMongoCollection<T> GetCollection<T>()
        {
            return database.GetCollection<T>(typeof(T).Name);
        }

        public async Task<T?> AddAsync(T value)
        {
            value.ActiveFlag = true;
            value.CreatedAt = DateTime.Now;

            await collection.InsertOneAsync(value);
            return value;
        }

        public async Task<IList<T>?> AddRangeAsync(IList<T> values)
        {
            foreach (var value in values)
            {
                var now = DateTime.Now;
                value.ActiveFlag = true;
                value.CreatedAt = DateTime.Now;
            }
            await collection.InsertManyAsync(values);
            return values;
        }

        public async Task<bool> DeleteAsync(T value)
        {
            var filter = Builders<T>.Filter.Eq(x => x.Id, value.Id);
            value.ActiveFlag = false;
            var result = await collection.ReplaceOneAsync(filter, value);
            return true;
        }

        public async Task<bool> DeleteRangeAsync(IList<T> values)
        {
            foreach (var value in values)
            {
                var filter = Builders<T>.Filter.Eq(x => x.Id, value.Id);
                value.ActiveFlag = false;
                var result = await collection.ReplaceOneAsync(filter, value);
            }
            return true;
        }

        public async Task<bool> ForceDeleteAsync(T value)
        {
            var filter = Builders<T>.Filter.Eq(x => x.Id, value.Id);
            var result = await collection.DeleteOneAsync(filter);
            return result.DeletedCount > 0;
        }

        public async Task<bool> ForceDeleteRangeAsync(IList<T> values)
        {
            var filter = Builders<T>.Filter.In(x => x.Id, values.Select(p => p.Id));
            var result = await collection.DeleteManyAsync(filter);
            return result.DeletedCount > 0;
        }

        public async Task<T?> GetByIdAsync(string id)
        {
            var result = await collection.FindAsync(x => x.Id == id && x.ActiveFlag == true);
            return await result.FirstOrDefaultAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>>? filter)
        {
            var filters = Builders<T>.Filter.And(Builders<T>.Filter.Where(x => x.ActiveFlag == true),
                                                 Builders<T>.Filter.Where(filter));
            var result = await collection
                .FindAsync(filters);
            return await result.AnyAsync();
        }

        public async Task<T?> GetByFilterAsync(Expression<Func<T, bool>>? filter)
        {
            var filters = Builders<T>.Filter.And(Builders<T>.Filter.Where(x => x.ActiveFlag == true),
                                                 Builders<T>.Filter.Where(filter));
            var result = await collection
                .FindAsync(filters);
            return await result.FirstOrDefaultAsync();
        }

        public async Task<IList<T>?> GetAllAsync(Expression<Func<T, bool>>? filter = null, 
                                                 OrderBy orderDir = OrderBy.ASC, 
                                                 Expression<Func<T, object>>? orderBy = null, 
                                                 int? take = null)
        {
            if (orderBy == null)
                orderBy = x => x.CreatedAt;

            FilterDefinition<T>? filters;
            if (filter == null)
                filters = Builders<T>.Filter.And(Builders<T>.Filter.Where(x => x.ActiveFlag == true));
            else
                filters = Builders<T>.Filter.And(Builders<T>.Filter.Where(x => x.ActiveFlag == true),
                                                 Builders<T>.Filter.Where(filter));

            var query = collection.Find(filter, options: new FindOptions { AllowDiskUse = true });

            if (take?.ToInt() > 0)
                query = query.Limit(take.ToInt());

            if (orderDir == OrderBy.ASC)
                query = query.SortBy(orderBy);
            else
                query = query.SortByDescending(orderBy);

            return await query.ToListAsync();
        }

        public async Task<T?> UpdateAsync(T value)
        {
            var filter = Builders<T>.Filter.Eq(x => x.Id, value.Id);
            var result = await collection.ReplaceOneAsync(filter, value);
            return value;
        }
    }
}
