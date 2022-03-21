using AzureStorageAPI.Connection;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AzureStorageAPI.Repository
{
    public class AzureRepository<TEntity> : IAzureRepository<TEntity>
        where TEntity : TableEntity, new()
    {
        private readonly CloudTableClient _cloudTableClient;
        private readonly CloudTable _cloudTable;
        public AzureRepository()
        {
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(AStorageConnection.ConnectionString);
            _cloudTableClient = cloudStorageAccount.CreateCloudTableClient();
            _cloudTable = _cloudTableClient.GetTableReference(typeof(TEntity).Name);
            _cloudTable.CreateIfNotExists();
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            try
            {
                var operation = TableOperation.InsertOrMerge(entity);
                var execute = await _cloudTable.ExecuteAsync(operation);
                return execute.Result as TEntity;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        public async Task<TEntity> DeleteAsync(string partitionKey, string rowKey)
        {
            try
            {
                var entity = await GetAsync(partitionKey, rowKey);
                var operation = TableOperation.Delete(entity);
                var execute = await _cloudTable.ExecuteAsync(operation);
                return execute.Result as TEntity;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        public async Task<TEntity> GetAsync(string partitionKey, string rowKey)
        {
            try
            {
                var operation = TableOperation.Retrieve<TEntity>(partitionKey, rowKey);
                var execute = await _cloudTable.ExecuteAsync(operation);
                return execute.Result as TEntity;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        public IQueryable<TEntity> QueryAsync(Expression<Func<TEntity, bool>> expression)
        {
            try
            {
                return _cloudTable.CreateQuery<TEntity>().Where(expression).AsQueryable();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        public IQueryable<TEntity> QueryAsync()
        {
            try
            {
                return _cloudTable.CreateQuery<TEntity>().AsQueryable();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }


        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            try
            {
                var operation = TableOperation.Replace(entity);
                var execute = await _cloudTable.ExecuteAsync(operation);
                return execute.Result as TEntity;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}
