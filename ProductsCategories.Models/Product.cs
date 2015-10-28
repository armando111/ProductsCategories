namespace ProductsCategories.Models
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class Product
    {
        [BsonId]
        public ObjectId _id { get; set; }

        [BsonElement("id")]
        [BsonRequired]
        public int Id { get; set; }

        [BsonRequired]
        [BsonElement("name")]
        public string Name  { get; set; }

        [BsonRequired]
        [BsonElement("categoryId")]
        public int CategoryId { get; set; }

        [BsonElement("imageUrl")]
        public string ImageUrl { get; set; }
    }
}
