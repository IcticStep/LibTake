using System.Collections.Generic;
using System.Linq;
using Code.Runtime.Infrastructure.Services.Factories;
using Code.Runtime.Logic.Customers;
using NUnit.Framework;
using UnityEngine;

namespace Code.Tests.Factories
{
    [TestFixture]
    public class InteractablesTests
    {
        [Test]
        public void WhenCreated5Customers_And_ThenIdsAreUnique()
        {
            // Arrange. 
            IInteractablesFactory customerFactory = SetUp.InteractablesFactoryForCustomers();
            List<CustomerStateMachine> customers = new();
            
            // Act.
            for(int i = 0; i < 3; i++)
                customers.Add(customerFactory.CreateCustomer(Vector3.zero));
            IEnumerable<string> ids = customers.Select(x => x.GetComponentInChildren<BookReceiver>().Id);
            
            // Assert.
            IEnumerable<IGrouping<string, string>> duplicates = ids
                .GroupBy(x => x)
                .Where(group => group.Count() > 1);
            
            Assert.IsEmpty(duplicates, "Duplicated ids");
        }
    }
}