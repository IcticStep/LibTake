using System;

namespace Code.Runtime.Ui.Coin
{
    public interface ICoinRewardSource
    {
        public event Action<int> Rewarded;
    }
}