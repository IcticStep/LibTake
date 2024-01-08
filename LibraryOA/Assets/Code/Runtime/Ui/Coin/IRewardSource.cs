using System;

namespace Code.Runtime.Ui.Coin
{
    public interface IRewardSource
    {
        public event Action<int> Rewarded;
    }
}