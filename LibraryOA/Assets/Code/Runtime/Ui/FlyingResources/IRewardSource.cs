using System;

namespace Code.Runtime.Ui.FlyingResources
{
    public interface IRewardSource
    {
        public event Action<int> Rewarded;
    }
}