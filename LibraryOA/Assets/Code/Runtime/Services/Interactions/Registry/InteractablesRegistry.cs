using System.Collections.Generic;
using Code.Runtime.Logic.Interactions;
using JetBrains.Annotations;
using UnityEngine;

namespace Code.Runtime.Services.Interactions.Registry
{
    [UsedImplicitly]
    public sealed class InteractablesRegistry : IInteractablesRegistry
    {
        private readonly Dictionary<Collider, Interactable> _interactablesByCollider = new();

        public void Register<T>(T interactable, Collider collider)
            where T : Interactable =>
            _interactablesByCollider[collider] = interactable;

        public void CleanUp() =>
            _interactablesByCollider.Clear();

        public Interactable GetInteractableByCollider(Collider found) =>
            _interactablesByCollider.TryGetValue(found, out Interactable interactable)
                ? interactable
                : default(Interactable);
    }
}