using AzureStorageAPI.Model;
using AzureStorageAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AzureStorageAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AzureController : ControllerBase
    {
       private readonly IAzureRepository<Job> _tableStorageRepository;
        public AzureController(IAzureRepository<Job> tableStorageRepository)
        {
            _tableStorageRepository = tableStorageRepository;
        }

        // GET: api/<AzureController>
        [Route("job-list")]
        [HttpGet]
        public async Task<IEnumerable<Job>> GetAllJobAsync()
        {
            var response =  _tableStorageRepository.QueryAsync().ToList();
            return response;
        }

        // GET api/<AzureController>/5
        [Route("Get-job/{rowKey},{partitionKey}")]
        [HttpGet]
        public async Task<Job> GetJobAsync(string rowKey, string partitionKey)
        {
            var response = await _tableStorageRepository.GetAsync(partitionKey, rowKey);
            return response;
        }

        // POST api/<AzureController>
        [Route("CreateJob")]
        [HttpPost]
        public async Task<object> PostJob(Job job)
        {
            job.JobID = Guid.NewGuid().ToString();
            job.RowKey = Guid.NewGuid().ToString();
            job.PartitionKey = "CustomerType";
            var response = await _tableStorageRepository.AddAsync(job);
            return response;
        }

        // PUT api/<AzureController>/5
        [Route("Update-job")]
        [HttpPut]
        public async Task<object> PutAsync(Job job)
        {
            var response = await _tableStorageRepository.UpdateAsync(job);
            return response;
        }

        // DELETE api/<AzureController>/5
        [Route("Delete-job/{rowKey},{partitionKey}")]
        [HttpDelete]
        public async Task<object> DeleteAsync(string rowKey, string partitionKey)
        {
            var response = await _tableStorageRepository.DeleteAsync(partitionKey, rowKey);
            return response;
        }
    }
}
