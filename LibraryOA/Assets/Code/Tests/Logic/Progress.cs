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
            Assert.That(progress.Value, Is.LessThanOrEqualTo(float.Epsilon), $"{nameof(progress)}.{nameof(progress.Value)}");
            Assert.True(progress.Empty, $"{nameof(progress)}.{nameof(progress.Empty)}");
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
                Assert.True(progress.Full, $"{nameof(progress)}.{nameof(progress.Full)}");
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
            Assert.True(wasCalled, message: $"{progress.Started} event fired");
        }

        [UnityTest]
        public IEnumerator WhenStartFilling_AndWaited_ThenValueWentUp() =>
            UniTask.ToCoroutine(async () =>
            {
                // Arrange.
                Progress progress = SetUp.ProgressWithSecondsToFinish(0.1f);

                // Act.
                progress.StartFilling();
                await UniTask.WaitForSeconds(0.1f);
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
        
        [UnityTest]
        public IEnumerator WhenReset_AndWasStarted_ThenJustResetIsTrue() =>
            UniTask.ToCoroutine(async () =>
            {
                // Arrange.
                Progress progress = SetUp.ProgressWithSecondsToFinish(1f);
                progress.StartFilling();
                
                // Act.
                progress.Reset();

                // Assert.
                Assert.IsTrue(progress.JustReset, $"{nameof(progress)}.{nameof(progress.JustReset)}");
            });

        [Test]
        public void WhenCreated_And_ThenCanBeStarted()
        {
            // Arrange.
            Progress progress = SetUp.ProgressWithSecondsToFinish(1f);
            
            // Act.

            // Assert.
            Assert.True(progress.CanBeStarted, $"{nameof(progress)}.{nameof(progress.CanBeStarted)}");
        }

        [Test]
        public void WhenStartFilling_And_ThenCanNotBeStarted()
        {
            // Arrange.
            Progress progress = SetUp.ProgressWithSecondsToFinish(1f);
            
            // Act.
            progress.StartFilling();
            
            // Assert.
            Assert.False(progress.CanBeStarted, $"{nameof(progress)}.{nameof(progress.CanBeStarted)}");
        }

        [UnityTest]
        public IEnumerator WhenStartFilling_AndAwaited_ThenFilled() =>
            UniTask.ToCoroutine(async () =>
            {
                // Arrange.
                Progress progress = SetUp.ProgressWithSecondsToFinish(0.01f);
                
                // Act.
                progress.StartFilling();
                int finishedTask = await UniTask.WhenAny(progress.Task, UniTask.WaitForSeconds(0.5f));
                if(finishedTask == 1)
                    Assert.Fail($"Timeout on trying to await the progress task.");

                // Assert.
                Assert.True(progress.Full);
            });
    }
}