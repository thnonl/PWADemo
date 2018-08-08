using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace React_Redux.Models
{
    public class Product
    {
        public ObjectId Id { get; set; }

        [BsonElement("Title")]
        public string Title { get; set; }

        [BsonElement("Description")]
        public string Description { get; set; }
        
        [BsonElement("Price")]
        public double Price { get; set; }

        [BsonElement("CreatedOn")]
        public DateTime CreatedOn { get; set; }
        
        [BsonElement("UpdatedOn")]
        public DateTime? UpdatedOn { get; set; }
    }
}