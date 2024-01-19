using System;
using Code.Runtime.Services.Interactions.ReadBook;
using Code.Runtime.StaticData.Books;
using Code.Runtime.Ui.FlyingResources;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic.Interactables.Reading
{
    internal sealed class ReadingFlyingResourceViewer : MonoBehaviour
    {
        [SerializeField]
        private FlyingResource _flyingResource;
        
        private IReadBookService _readBookService;
        
        [Inject]
        private void Construct(IReadBookService readBookService) =>
            _readBookService = readBookService;

        private void Awake() =>
            _readBookService.BookRead += OnBookRead;

        private void OnDestroy() =>
            _readBookService.BookRead -= OnBookRead;

        private void OnBookRead(StaticBook book) =>
            _flyingResource
                .FlyResource(book.StaticBookType.Icon, 1)
                .Forget();
    }
}