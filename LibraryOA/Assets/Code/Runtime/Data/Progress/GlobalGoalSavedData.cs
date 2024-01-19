using System;

namespace Code.Runtime.Data.Progress
{
    [Serializable]
    public class GlobalGoalSavedData
    {
        public string GoalId;
        public int GoalStepIndex;
        public bool PayedForStep;
    }
}