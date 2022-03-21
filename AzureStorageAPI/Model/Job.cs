using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureStorageAPI.Model
{
    public class Job : TableEntity
    {
        public string JobID { get; set; }
        public string JobName { get; set; }
        public string JobStatus { get; set; }
        public string LogInfo { get; set; }
        public string MailId { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
