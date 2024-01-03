using Code.Runtime.Logic;
using Code.Runtime.Logic.Customers;
using Code.Runtime.Logic.Customers.CustomersStates;
using Code.Runtime.Services.Player.Lives;
using NSubstitute;
using NUnit.Framework;

namespace Code.Tests.Logic
{
    [TestFixture]
    public class CustomerPunishStateTests
    {
        [Test]
        public void WhenStartedPunish_And_ThenPlayerLiveServiceWasteLifeInvoked()
        {
            // Arrange.
            IPlayerLivesService playerLivesService = Substitute.For<IPlayerLivesService>();
            PunishState punishState = new PunishState(
                Substitute.For<ICustomerStateMachine>(),
                Substitute.For<IBookReceiver>(),
                Substitute.For<IProgress>(),
                playerLivesService);
            
            // Act.
            punishState.Start();

            // Assert.
            playerLivesService.Received().WasteLife();
        }
    }
}