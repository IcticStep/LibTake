using System;
using UnityEngine;

namespace Code.Runtime.Logic.Animations
{
    internal abstract class CoddedAnimation : MonoBehaviour
    {
        public event Action Finished;
        public abstract bool Playing { get; }
        public abstract void StartAnimation();

        protected void NotifyFinished() =>
            Finished?.Invoke();
    }
}