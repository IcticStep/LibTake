using System.Collections;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using UnityEngine.TestTools;
using Progress = Code.Runtime.Logic.Progress;

namespace Code.Tests.Logic
{
    public class ProgressTests
    {
        [Test]
        public void WhenCreated_And_ThenProgressEmpty()
        {
            // Arrange.
            Progress progress = Create.Progress();

            // Act.

            // Assert.
            Assert.That(progress.Value, Is.LessThanOrEqualTo(float.Epsilon));
            Assert.True(progress.Empty);
        }

        [UnityTest]
        public IEnumerator WhenStartFilling_AndFilled_ThenFull() =>
            UniTask.ToCoroutine(async () =>
            {
                // Arrange.
                Progress progress = SetUp.ProgressWithSecondsToFinish(float.Epsilon);
                
                // Act.
                progress.StartFilling();
                await UniTask.DelayFrame(2);

                // Assert.
                Assert.True(progress.Full);
            });

        [Test]
        public void WhenStartFilling_And_ThenStartEventFired()
        {
            // Arrange.
            Progress progress = SetUp.ProgressWithSecondsToFinish(1f);
            bool wasCalled = false;
            progress.Started += () => wasCalled = true;

            // Act.
            progress.StartFilling();

            // Assert.
            Assert.True(wasCalled, message: "Event fired");
        }

        [UnityTest]
        public IEnumerator WhenStartFilling_AndWaited_ThenValueWentUp() =>
            UniTask.ToCoroutine(async () =>
            {
                // Arrange.
                Progress progress = SetUp.ProgressWithSecondsToFinish(0.1f);

                // Act.
                progress.StartFilling();
                await UniTask.WaitForSeconds(0.01f);
                progress.StopFilling();

                // Assert.
                Assert.That(progress.Value, Is.GreaterThan(float.Epsilon),
                    message: $"{nameof(progress)}.{nameof(progress.Value)}");
            });

        [UnityTest]
        public IEnumerator WhenReset_AndHasProgress_ThenProgressErased() =>
            UniTask.ToCoroutine(async () =>
            {
                // Arrange.
                Progress progress = SetUp.ProgressWithSecondsToFinish(1f);
                progress.StartFilling();
                await UniTask.DelayFrame(5);
                progress.StopFilling();
                
                // Act.
                progress.Reset();

                // Assert.
                Assert.That(progress.Value, Is.LessThanOrEqualTo(float.Epsilon),
                    message: $"{nameof(progress)}.{nameof(progress.Value)}");
            });
    }
}