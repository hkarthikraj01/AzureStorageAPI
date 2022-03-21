using AzureStorage.Business.IBusiness;
using AzureStorage.Model;
using AzureStroage.Repo.IRepo;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AzureStorage.Business
{
    public class AzureBusiness<TEntity> : IAzureBusiness<TEntity>
    where TEntity : TableEntity, new()
    {
        AzureBusiness<Customers> _tableStorageRepository;
        public AzureBusiness(IAzureRepo<Customers> tableStorageRepository)
        {
            _tableStorageRepository = (AzureBusiness<Customers>)tableStorageRepository;
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            try
            {  
                var execute = await _tableStorageRepository.AddAsync(entity as Customers);
                return execute as TEntity;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public async Task<TEntity> DeleteAsync(string rowKey, string partitionKey)
        {
            try
            {
                var execute = await _tableStorageRepository.DeleteAsync(rowKey,partitionKey);
                return execute as TEntity;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public async Task<TEntity> GetAsync(string rowKey, string partitionKey)
        {
            try
            {
                var execute = await _tableStorageRepository.GetAsync(rowKey, partitionKey);
                return execute as TEntity;
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
                var execute = await _tableStorageRepository.UpdateAsync(entity as Customers);
                return execute as TEntity;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}
