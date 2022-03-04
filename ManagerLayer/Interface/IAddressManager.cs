namespace ManagerLayer.Interface
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IAddressManager
    {
        Task<AddressModel> AddAddress(AddressModel addAddress);
        Task<AddressModel> UpdateAddress(AddressModel editAddress);
        Task<bool> DeleteAddress(AddressModel delete);
        IEnumerable<AddressModel> GetAllAddress();
        Task<AddressModel> GetByAddressType(string addTypeId);
    }
}
