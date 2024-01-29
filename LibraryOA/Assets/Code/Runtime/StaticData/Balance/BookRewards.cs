using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code.Runtime.StaticData.Balance
{
    [Serializable]
    public sealed class BookRewards
    {
        [SerializeField]
        private List<BookRewardStatement> _bookRewardStatements = new();

        public int GetRewardSize(float percentsWaiting)
        {
            BookRewardStatement statement = _bookRewardStatements
                .OrderByDescending(statement => statement.PercentsLowerBound)
                .FirstOrDefault(statement => statement.IsTrueFor(percentsWaiting));

            return statement?.Reward ?? 1;
        }
    }
}