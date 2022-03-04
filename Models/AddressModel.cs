namespace Models
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    public class AddressModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string AddressID { get; set; }

        [ForeignKey("RegisterModel")]
        public string UserID { get; set; }
        public virtual RegisterModel RegisterModel { get; set; }

        [ForeignKey("AddressType")]
        public string AddTypeID { get; set; }
        public string FullAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Pincode { get; set; }
    }
}
