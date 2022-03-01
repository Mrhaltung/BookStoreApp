namespace Models
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class RegisterModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        public string UserID { get; set; }

        public string FullName { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9]+([.#_$+-][a-zA-Z0-9]+)*[@][a-zA-Z0-9]+[.][a-zA-Z]{2,3}([.][a-zA-Z]{2})?$", ErrorMessage = "Email is not valid. Please Enter valid email")]
        public string EmailID { get; set; }

        [Required]
        public string Password { get; set; }

        public string Mobile { get; set; }
    }
}
