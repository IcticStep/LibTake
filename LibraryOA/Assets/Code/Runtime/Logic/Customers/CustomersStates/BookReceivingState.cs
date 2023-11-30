using Code.Runtime.Services.BooksReceiving;
using Code.Runtime.Services.CustomersQueue;

namespace Code.Runtime.Logic.Customers.CustomersStates
{
    internal class BookReceivingState : ICustomerState
    {
        private readonly CustomerStateMachine _customerStateMachine;
        private readonly ICustomersQueueService _customersQueueService;
        private readonly IBooksReceivingService _booksReceivingService;

        public BookReceivingState(CustomerStateMachine customerStateMachine, ICustomersQueueService customersQueueService, IBooksReceivingService booksReceivingService)
        {
            _customerStateMachine = customerStateMachine;
            _customersQueueService = customersQueueService;
            _booksReceivingService = booksReceivingService;
        }

        public void Start()
        {
            
        }

        public void Exit() =>
            _customersQueueService.Dequeue();
    }
}