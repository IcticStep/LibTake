using System;
using UnityEngine;

namespace Code.Runtime.StaticData.Balance
{
    [Serializable]
    public sealed class BookRewardStatement
    {
        [SerializeField]
        private float _percentsLowerBound;
        [SerializeField]
        private int _reward;
        public float PercentsLowerBound => _percentsLowerBound;
        public int Reward => _reward;

        public bool IsTrueFor(float percents) =>
            percents >= PercentsLowerBound;
    }
}