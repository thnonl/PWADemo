using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace React_Redux.Models
{
    public class Product
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
        
        [JsonProperty(PropertyName = "price")]
        public double Price { get; set; }

        [JsonProperty(PropertyName = "createdOn")]
        public DateTime CreatedOn { get; set; }
        
        [JsonProperty(PropertyName = "updatedOn")]
        public DateTime? UpdatedOn { get; set; }
    }
}