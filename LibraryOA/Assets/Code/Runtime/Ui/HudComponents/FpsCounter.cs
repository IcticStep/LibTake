using System.Collections;
using TMPro;
using UnityEngine;

namespace Code.Runtime.Ui.HudComponents
{
    internal sealed class FpsCounter : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _counterText;

        [SerializeField]
        private float _secondsUpdateInterval = 0.1f;

        private float _fpsValue;
        private WaitForSeconds _cachedWait;
        private Coroutine _coroutine;

        private void Awake() =>
            _cachedWait = new WaitForSeconds(_secondsUpdateInterval);

        private void Start() =>
            _coroutine = StartCoroutine(UpdateFps());

        private void OnDestroy() =>
            StopCoroutine(_coroutine);

        private IEnumerator UpdateFps()
        {
            while(true)
            {
                _fpsValue = 1f / Time.unscaledDeltaTime;
                yield return _cachedWait;
                _counterText.text = "FPS: " + _fpsValue.ToString("0.00");
            }
        }
    }
} 