using Code.Runtime.Logic.Interactables;
using UnityEngine;

namespace Code.Runtime.Services.Interactions.Registry
{
    public interface IInteractablesRegistry
    {
        void Register<T>(T interactable, Collider collider)
            where T : Interactable;

        void CleanUp();
        Interactable GetInteractableByCollider(Collider found);
    }
}