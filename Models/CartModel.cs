namespace Models
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    public class CartModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string CartID { get; set; }

        [ForeignKey("RegisterModel")]
        public string UserID { get; set; }
        public virtual RegisterModel RegisterModel { get; set; }

        [ForeignKey("BooksModel")]
        public string BookID { get; set; }
        public virtual BooksModel BooksModel { get; set; }
        public int Quantity { get; set; }
    }
}
