using Code.Runtime.Logic;

namespace Code.Tests.Logic
{
    public static class SetUp
    {
        public static Progress ProgressWithSecondsToFinish(float timeToFinish)
        {
            Progress progress = Create.Progress();
            progress.Initialize(timeToFinish);
            return progress;
        }
    }
}