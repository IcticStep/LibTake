using Code.Runtime.Ui.Common;
using UnityEngine;

namespace Code.Runtime.Ui
{
    public sealed class LoadingCurtain : MonoBehaviour
    {
        [SerializeField]
        private SmoothFader _smoothFader;
        [SerializeField]
        private Canvas _canvas;

        private void Start() =>
            DontDestroyOnLoad(_canvas);

        public void Show() =>
            _smoothFader.UnFade();

        public void Hide() =>
            _smoothFader.Fade();
    }
}