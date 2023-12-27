using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Logic;
using Code.Runtime.Logic.Customers;
using Code.Runtime.Logic.Customers.CustomersStates;
using Code.Runtime.Services.Player.Inventory;
using Code.Runtime.StaticData.Balance;
using NSubstitute;
using NUnit.Framework;

namespace Code.Tests.Logic
{
    [TestFixture]
    public class CustomerRewardStateTests
    {
        [Test]
        public void WhenStartedReward_And_ThenPlayerReceiveCoins()
        {
            // Arrange.
            IPlayerInventoryService playerInventoryService = Substitute.For<IPlayerInventoryService>();
            IStaticDataService staticDataService = Substitute.For<IStaticDataService>();
            staticDataService.BookReceiving.Returns(Substitute.For<StaticBookReceiving>());
            
            RewardState rewardState = new RewardState(
                Substitute.For<ICustomerStateMachine>(),
                Substitute.For<IProgress>(),
                playerInventoryService,
                staticDataService);
            
            // Act.
            rewardState.Start();

            // Assert.
            playerInventoryService.Received().AddCoins(Arg.Any<int>());
        }
    }
}