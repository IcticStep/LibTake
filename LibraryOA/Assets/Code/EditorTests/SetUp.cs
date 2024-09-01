using Code.Runtime.Infrastructure.AssetManagement;
using Code.Runtime.Infrastructure.Services.Factories;
using Code.Runtime.Infrastructure.Services.SaveLoad;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Logic;
using Code.Runtime.Services.Customers.Registry;
using Code.Runtime.Services.Interactions.Registry;
using Code.Runtime.Services.TruckDriving;
using NSubstitute;
using UnityEngine;

namespace Code.EditorTests
{
    public static class SetUp
    {
        public static Progress ProgressWithSecondsToFinish(float timeToFinish)
        {
            Progress progress = Create.LogicProgress();
            progress.Initialize(timeToFinish);
            return progress;
        }

        public static IInteractablesFactory InteractablesFactoryForCustomers()
        {
            IAssetProvider assetProvider = Substitute.For<IAssetProvider>();
            assetProvider
                .Instantiate(Arg.Any<string>(), Arg.Any<Vector3>())
                .Returns(x =>
                {
                    string path = (string)x[0];
                    Vector3 position = (Vector3)x[1];
                    
                    GameObject gameObject = Resources.Load<GameObject>(path);
                    return Object.Instantiate(gameObject, position, Quaternion.identity);
                });
            IInteractablesFactory customerFactory = new InteractablesFactory(
                assetProvider,
                Substitute.For<ISaveLoadRegistry>(),
                Substitute.For<IInteractablesRegistry>(),
                Substitute.For<IStaticDataService>(),
                Substitute.For<ITruckProvider>(),
                Substitute.For<ICustomersRegistryService>());

            return customerFactory;
        }
    }
}