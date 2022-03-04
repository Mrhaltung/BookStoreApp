namespace RepositoryLayer.Repository
{
    using Models;
    using MongoDB.Driver;
    using RepositoryLayer.Interface;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public class AddressRepository : IAddressRepository
    {
        private readonly IMongoCollection<AddressModel> Address;

        public AddressRepository(IDatabaseSetting DB)
        {
            var client = new MongoClient(DB.ConnectionString);
            var Db = client.GetDatabase(DB.DatabaseName);
            Address = Db.GetCollection<AddressModel>("Address");
        }

        public async Task<AddressModel> AddAddress(AddressModel addAddress)
        {
            try
            {
                var check = await this.Address.Find(x => x.AddressID == addAddress.AddressID).FirstOrDefaultAsync();
                if(check == null)
                {
                    await this.Address.InsertOneAsync(addAddress);
                    return addAddress;
                }
                return null;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }                
        }

        public async Task<AddressModel> UpdateAddress(AddressModel editAddress)
        {
            try
            {
                var check = await this.Address.Find(x => x.AddressID == editAddress.AddressID).FirstOrDefaultAsync();
                if(check != null)
                {
                    await this.Address.UpdateOneAsync(x => x.AddressID == editAddress.AddressID,
                        Builders<AddressModel>.Update.Set(x => x.FullAddress, editAddress.FullAddress)
                        .Set(x => x.City, editAddress.City)
                        .Set(x => x.State, editAddress.State)
                        .Set(x => x.Pincode, editAddress.Pincode));
                    return check;
                }
                return null;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }                
        }

        public async Task<bool> DeleteAddress(AddressModel delete)
        {
            try
            {
                var check = await this.Address.FindOneAndDeleteAsync(x => x.AddressID == delete.AddressID);
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<AddressModel> GetAllAddress()
        {
            try
            {
                return this.Address.Find(FilterDefinition<AddressModel>.Empty).ToList();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<AddressModel> GetByAddressType(string addTypeId)
        {
            try
            {
                return await this.Address.Find(x => x.AddressID == addTypeId).FirstOrDefaultAsync();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
