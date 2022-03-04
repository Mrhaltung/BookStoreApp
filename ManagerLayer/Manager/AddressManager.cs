namespace ManagerLayer.Manager
{
    using ManagerLayer.Interface;
    using Models;
    using RepositoryLayer.Interface;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public class AddressManager : IAddressManager
    {
        private readonly IAddressRepository repo;
        public AddressManager(IAddressRepository repo)
        {
            this.repo = repo;
        }

        public async Task<AddressModel> AddAddress(AddressModel addAddress)
        {
            try
            {
                return await this.repo.AddAddress(addAddress);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<AddressModel> UpdateAddress(AddressModel editAddress)
        {
            try
            {
                return await this.repo.UpdateAddress(editAddress);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> DeleteAddress(AddressModel delete)
        {
            try
            {
                return await this.repo.DeleteAddress(delete);
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
                return this.repo.GetAllAddress();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<AddressModel> GetByAddressType(string addTypeId)
        {
            try
            {
                return await this.repo.GetByAddressType(addTypeId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
