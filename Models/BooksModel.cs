namespace Models
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class BooksModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string BookID { get; set; }
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public decimal Rating { get; set; }
        public decimal TotalRating { get; set; }
        public int DiscountPrice { get; set; }
        public int OriginalPrice { get; set; }
        public string Description { get; set; }
        public string BookImage { get; set; }
    }
}
