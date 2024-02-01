using Code.Runtime.Ui.Common;
using UnityEngine;

namespace Code.Runtime.Ui.Menu
{
    internal sealed class SettingsGroup : MonoBehaviour
    {
        [SerializeField]
        private FastFlyingFading _fader;

        private void Start() =>
            _fader.SetInOffScreenPosition();
    }
}