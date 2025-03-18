using IndividualTask3.Models;

namespace IndividualTask3.Interfaces
{
    public interface ICustomerFieldService
    {
        Task<CustomerField> GetCustomerFieldsAsync(string accountNumber);

    }
}
