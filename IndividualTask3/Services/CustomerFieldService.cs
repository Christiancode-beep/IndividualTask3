using IndividualTask3.Interfaces;
using IndividualTask3.Models;
using Microsoft.EntityFrameworkCore;

namespace IndividualTask3.Services
{
    public class CustomerFieldService : ICustomerFieldService
    {
        private readonly CustomerFieldDbContext _dbContext;
        private readonly ILogger<CustomerFieldService> _logger;

        public CustomerFieldService(CustomerFieldDbContext dbContext, ILogger<CustomerFieldService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<CustomerField> GetCustomerFieldsAsync(string accountNumber)
        {
            try
            {
                var response = await _dbContext.CustomerFields.FirstOrDefaultAsync(c => c.AccountNumber == accountNumber);
                if (response == null)
                {
                    _logger.LogInformation("Fields empty or null");
                    return response;
                }
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occured");
                return null;
            }

        }
    }
}
