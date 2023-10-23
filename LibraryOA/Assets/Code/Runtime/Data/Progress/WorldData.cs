using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Runtime.Data.Progress
{
    [Serializable]
    public sealed class WorldData
    {
        public BookHoldersState BookHoldersState = new();
        public ProgressesStates ProgressesStates;
    }

    [Serializable]
    public class ProgressesStates
    {
    }
}