using System;
using UnityEngine;

namespace Code.Runtime.Data
{
    /// <summary>
    /// Used to mark data as read-only to prevent modifying from the inspector.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
    public sealed class ReadOnlyAttribute : PropertyAttribute { }
}