using Grpc.Core;
using GrpcServiceTutorial;

namespace GrpcServiceTutorial.Services
{
    public class CustomersService : Customer.CustomerBase
    {
        private readonly ILogger _logger;
        public CustomersService(ILogger<CustomersService> logger)
        {
            _logger = logger;
        }

        public override Task<CustomerModel> GetCustomerInfo(CustomerLookupModel request, ServerCallContext context)
        {
            CustomerModel output = new CustomerModel();

            if(request.UserId == 1)
            {
                output.FirstName = "Jane";
                output.LastName = "Amazing";
            }

            return Task.FromResult(output);

        }

    }
}
