using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace AzureStorage.Model
{
    public class Customers : TableEntity
    {
        public string IdentityNumber { get; set; }
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string CustomerCode { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
