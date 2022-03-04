﻿namespace Models
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    public class OrderModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string OrderID { get; set; }

        [ForeignKey("BooksModel")]
        public string BookID { get; set; }
        public virtual BooksModel BooksModel { get; set; }

        [ForeignKey("RegisterModel")]
        public string UserID { get; set; }
        public virtual RegisterModel RegisterModel { get; set; }

        [ForeignKey("AddressModel")]
        public string AddressID { get; set; }
        public virtual AddressModel AddressModel { get; set; }
    }
}
